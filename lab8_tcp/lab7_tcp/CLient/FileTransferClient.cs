using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace lab7_tcp
{
    public class FileTransferClient
    {
        private TcpClient _client;
        public event Action<int> ProgressChanged;
        public event Action<bool, string> TransferStatusChanged;

        protected virtual void OnTransferStatusChanged(bool show, string message)
        {
            TransferStatusChanged?.Invoke(show, message);
        }

        private void ShowTransferStatus(bool show, string message)
        {
            OnTransferStatusChanged(show, message);
        }

        public async Task Connect(string ip, int port)
        {
            _client = new TcpClient();
            await _client.ConnectAsync(ip, port);
        }

        public async Task SendFile(string filePath)
        {
            using (var stream = _client.GetStream())
            {
                string fileName = Path.GetFileName(filePath);
                long fileSize = new FileInfo(filePath).Length;

                // Отправляем информацию о файле
                byte[] fileNameBytes = Encoding.UTF8.GetBytes(fileName);
                byte[] fileNameLength = BitConverter.GetBytes(fileNameBytes.Length);
                byte[] fileSizeBytes = BitConverter.GetBytes(fileSize);

                await stream.WriteAsync(fileNameLength, 0, fileNameLength.Length);
                await stream.WriteAsync(fileNameBytes, 0, fileNameBytes.Length);
                await stream.WriteAsync(fileSizeBytes, 0, fileSizeBytes.Length);

                ShowTransferStatus(true, $"Получение: {fileName} ({FormatFileSize(fileSize)})");

                // Отправляем содержимое файла
                using (FileStream fs = File.OpenRead(filePath))
                {
                    byte[] buffer = new byte[8192];
                    int bytesRead;
                    long totalSent = 0;

                    while ((bytesRead = await fs.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await stream.WriteAsync(buffer, 0, bytesRead);
                        totalSent += bytesRead;
                        int progress = (int)(totalSent * 100 / fileSize);
                        ProgressChanged?.Invoke(progress);
                    }
                }
            }
        }

        public void Disconnect()
        {
            _client?.Close();
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            double len = bytes;

            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return $"{len:0.##} {sizes[order]}";
        }
    }
}
