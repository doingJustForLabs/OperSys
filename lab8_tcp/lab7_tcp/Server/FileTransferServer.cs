using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace lab7_tcp
{
    public class FileTransferServer
    {
        private TcpListener _listener;
        public event Action<string, long> FileReceived;
        private List<TcpClient> _clients = new List<TcpClient>();

        public void Start(string ipAddress, int port)
        {
            IPAddress ip;
            if (!IPAddress.TryParse(ipAddress, out ip))
            {
                throw new ArgumentException("Неверный формат IP-адреса");
            }

            _listener = new TcpListener(ip, port);
            _listener.Start();
            Task.Run(AcceptClients);
        }

        private async Task AcceptClients()
        {
            while (true)
            {
                var client = await _listener.AcceptTcpClientAsync();
                _clients.Add(client);
                _ = HandleClient(client);
            }
        }

        private async Task HandleClient(TcpClient client)
        {
            try
            {
                using (var stream = client.GetStream())
                {
                    // Читаем информацию о файле
                    byte[] fileNameLengthBytes = new byte[4];
                    await stream.ReadAsync(fileNameLengthBytes, 0, 4);
                    int fileNameLength = BitConverter.ToInt32(fileNameLengthBytes, 0);

                    byte[] fileNameBytes = new byte[fileNameLength];
                    await stream.ReadAsync(fileNameBytes, 0, fileNameLength);
                    string fileName = Encoding.UTF8.GetString(fileNameBytes);

                    byte[] fileSizeBytes = new byte[8];
                    await stream.ReadAsync(fileSizeBytes, 0, 8);
                    long fileSize = BitConverter.ToInt64(fileSizeBytes, 0);

                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string saveDirectory = Path.Combine(desktopPath, "ReceivedFiles");

                    Directory.CreateDirectory(saveDirectory);

                    // Сохраняем файл
                    string savePath = Path.Combine(saveDirectory, fileName);

                    //using (FileStream fs = File.Create(savePath))
                    //{
                    //    byte[] buffer = new byte[8192];
                    //    int bytesRead;
                    //    long totalRead = 0;

                    //    while (totalRead < fileSize)
                    //    {
                    //        int toRead = (int)Math.Min(buffer.Length, fileSize - totalRead);
                    //        bytesRead = await stream.ReadAsync(buffer, 0, toRead);
                    //        await fs.WriteAsync(buffer, 0, bytesRead);
                    //        totalRead += bytesRead;
                    //    }
                    //}

                    await BroadcastFile(stream, fileName, fileSize, savePath);

                    FileReceived?.Invoke(fileName, fileSize);
                }
            }
            catch { }
            finally
            {
                client.Close();
            }
        }

        private async Task BroadcastFile(NetworkStream sourceStream, string fileName, long fileSize, string serverSavePath)
        {
            // Сохраняем на сервере
            using (var fs = File.Create(serverSavePath))
            {
                await sourceStream.CopyToAsync(fs);
            }

            // Рассылаем другим клиентам
            foreach (var client in _clients.ToList()) // ToList для копирования на случай изменений
            {
                try
                {
                    if (client.Connected)
                    {
                        await SendFileToClient(client, fileName, serverSavePath);
                    }
                }
                catch { /* Игнорируем отключившихся клиентов */ }
            }
        }

        private async Task SendFileToClient(TcpClient client, string fileName, string filePath)
        {
            using (var stream = client.GetStream())
            using (var fileStream = File.OpenRead(filePath))
            {
                // Отправляем метаданные
                byte[] fileNameBytes = Encoding.UTF8.GetBytes(fileName);
                byte[] fileNameLengthBytes = BitConverter.GetBytes(fileNameBytes.Length);
                byte[] fileSizeBytes = BitConverter.GetBytes(new FileInfo(filePath).Length);

                await stream.WriteAsync(fileNameLengthBytes, 0, 4);
                await stream.WriteAsync(fileNameBytes, 0, fileNameBytes.Length);
                await stream.WriteAsync(fileSizeBytes, 0, 8);

                // Отправляем содержимое файла
                await fileStream.CopyToAsync(stream);
            }
        }

        public void Stop()
        {
            _listener?.Stop();
        }
    }
}
