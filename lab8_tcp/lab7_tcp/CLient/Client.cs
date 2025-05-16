using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab7_tcp
{
    public class Client
    {
        private TcpClient _tcpClient;
        public event Action<string> MessageReceived;
        private CancellationTokenSource _cts;

        public async Task Connect(string ip, int port, CancellationToken token = default)
        {
            _tcpClient = new TcpClient();
            _cts = CancellationTokenSource.CreateLinkedTokenSource(token);

            await _tcpClient.ConnectAsync(ip, port);
            _ = ReceiveMessages(_cts.Token);
        }

        public async Task SendMessage(string message)
        {
            if (_tcpClient?.Connected != true || _cts.IsCancellationRequested)
                return;

            try
            {
                var writer = new StreamWriter(_tcpClient.GetStream()) { AutoFlush = true };
                await writer.WriteLineAsync(message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                MessageReceived?.Invoke($"Ошибка: {ex.Message}");
                Disconnect();
            }
        }

        private async Task ReceiveMessages(CancellationToken token)
        {
            try
            {
                var reader = new StreamReader(_tcpClient.GetStream());
                while (!token.IsCancellationRequested)
                {
                    string message = await reader.ReadLineAsync().ConfigureAwait(false);
                    if (message == null) break; // Сервер закрыл соединение
                    MessageReceived?.Invoke(message);
                }
            }
            catch (Exception ex)
            {
                if (!token.IsCancellationRequested)
                    MessageReceived?.Invoke($"Соединение потеряно. Ошибка: {ex.Message}");
            }
            finally
            {
                Disconnect();
            }
        }

        public void Disconnect()
        {
            _cts?.Cancel();
            _tcpClient?.Close();
        }
    }
}
