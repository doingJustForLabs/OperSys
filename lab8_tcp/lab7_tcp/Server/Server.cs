using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace lab7_tcp
{
    public class Server
    {
        private TcpListener _listener;
        private List<TcpClient> _clients = new List<TcpClient>();
        public event Action<string> MessageReceived;
        private CancellationTokenSource _cts = new CancellationTokenSource();

        public void Start(string ipAddress, int port)
        {
            IPAddress ip;
            if (!IPAddress.TryParse(ipAddress, out ip))
            {
                throw new ArgumentException("Неверный формат IP-адреса");
            }

            _listener = new TcpListener(ip, port);
            _listener.Start();
            Task.Run(() => AcceptClients(_cts.Token), _cts.Token);
        }

        private async Task AcceptClients(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    TcpClient client = await _listener.AcceptTcpClientAsync().ConfigureAwait(false);

                    _clients.Add(client);
                    _ = HandleClient(client, token);
                }
                catch (OperationCanceledException) { break; }
                catch (ObjectDisposedException) { break; } // listener остановлен
                catch (Exception ex)
                {
                    if (!token.IsCancellationRequested)
                        MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }

        private async Task HandleClient(TcpClient client, CancellationToken token)
        {
            try
            {
                using (var stream = client.GetStream())
                using (var reader = new StreamReader(stream))
                {
                    while (!token.IsCancellationRequested)
                    {
                        string message = await reader.ReadLineAsync().ConfigureAwait(false);
                        if (message == null) break; // Клиент отключился
                        MessageReceived?.Invoke($"[Клиент]: {message}");
                        BroadcastMessage(message, client);
                    }
                }
            }
            catch (Exception ex) when (!token.IsCancellationRequested)
            {
                MessageReceived?.Invoke($"Ошибка: {ex.Message}");
            }
            finally
            {
                _clients.Remove(client);
                client.Dispose();
            }
        }

        private void BroadcastMessage(string message, TcpClient sender)
        {
            foreach (var client in _clients.Where(c => !c.Equals(sender)))
            {
                try
                {
                    var writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
                    writer.WriteLine(message);
                }
                catch { }
            }
        }

        public void Stop()
        {
            _cts.Cancel();
            _listener?.Stop();

            foreach (var client in _clients)
            {
                client.Close();
            }
            _clients.Clear();
        }

        public string GetServerIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }
    }
}
