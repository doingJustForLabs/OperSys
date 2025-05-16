using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab7_tcp
{
    public partial class Form1: Form
    {
        private Image gifImage;

        private string localIP;
        private int port = 5000;
        private string saveDirectory = "D:\\holn\\Desktop\\recievedFiles";
        private Thread serverThread;
        private bool isRunning = true;
        private bool isTransferring = false;
        private XPProgressBar xpProgress;

        public Form1()
        {
            InitializeComponent();

            localIP = GetLocalIPAddress();
            //localIP = "192.168.1.1";
            lblStatus.Text = $"Сервер запущен на {localIP}:{port}";

            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            // Запускаем сервер в фоновом потоке
            serverThread = new Thread(StartServer);
            serverThread.IsBackground = true;
            serverThread.Start();

            gifBox.Image = Image.FromFile("D:\\holn\\Desktop\\Study\\ОС\\lab8_tcp\\lab7_tcp\\orig.gif");

            xpProgress = new XPProgressBar()
            {
                Location = new Point(404, 314),
                Size = new Size(336, 23),
                Minimum = 0,
                Maximum = 100,
                Value = 0
            };
            this.Controls.Add(xpProgress);
        }

        private void StartServer()
        {
            try
            {
                Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(new IPEndPoint(IPAddress.Parse(localIP), port));
                listener.Listen(10);

                while (isRunning)
                {
                    using (Socket handler = listener.Accept())
                    {
                        byte[] fileNameLengthBytes = new byte[4];
                        handler.Receive(fileNameLengthBytes);
                        int fileNameLength = BitConverter.ToInt32(fileNameLengthBytes, 0);

                        byte[] fileNameBytes = new byte[fileNameLength];
                        handler.Receive(fileNameBytes);
                        string fileName = Encoding.UTF8.GetString(fileNameBytes);

                        byte[] fileSizeBytes = new byte[8];
                        handler.Receive(fileSizeBytes);
                        long fileSize = BitConverter.ToInt64(fileSizeBytes, 0);

                        ShowTransferUI(true, $"Получение: {fileName} ({FormatFileSize(fileSize)})");

                        // Получаем файл
                        string savePath = Path.Combine(saveDirectory, fileName);
                        using (FileStream fs = new FileStream(savePath, FileMode.Create))
                        {
                            byte[] buffer = new byte[8192];
                            int bytesRead;
                            long totalBytes = 0;

                            while (totalBytes < fileSize)
                            {
                                bytesRead = handler.Receive(buffer,
                                    (int)Math.Min(buffer.Length, fileSize - totalBytes),
                                    SocketFlags.None);

                                fs.Write(buffer, 0, bytesRead);
                                totalBytes += bytesRead;

                                UpdateProgress((int)(totalBytes * 100 / fileSize));
                            }
                        }

                        ShowTransferUI(false, $"Файл сохранен: {savePath}");
                    }
                }
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate {
                    txtLog.AppendText($"Ошибка сервера: {ex.Message}\r\n");
                });
            }
        }

        private void ShowTransferUI(bool show, string message)
        {
            this.Invoke((MethodInvoker)delegate {
                isTransferring = show;
                gifBox.Visible = show;
                if (show) gifBox.BringToFront();
                txtLog.AppendText(message + "\r\n");
                if (!show) xpProgress.Value = 0;
            });
        }

        private void UpdateProgress(int progress)
        {
            this.Invoke((MethodInvoker)delegate {
                xpProgress.Value = progress;
            });
        }

        public void ShowAnimation(bool show)
        {
            gifBox.Visible = show;
            if (show)
            {
                gifBox.BringToFront();
                Application.DoEvents(); // Немедленное обновления интерфейсика
            }
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string receiverIP = txtReceiverIP.Text;

                if (string.IsNullOrWhiteSpace(receiverIP))
                {
                    MessageBox.Show("Пожалуйста, введите IP-адрес получателя");
                    return;
                }

                ShowTransferUI(true, $"Отправляем файл {receiverIP}...");

                // Запускаем отправку в отдельном потоке
                new Thread(() => SendFile(filePath, receiverIP)).Start();
            }
        }

        private void SendFile(string filePath, string receiverIP)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate {
                    txtLog.AppendText($"Отправляем файл {receiverIP}...\r\n");
                });

                using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    client.Connect(new IPEndPoint(IPAddress.Parse(receiverIP), port));

                    string fileName = Path.GetFileName(filePath);
                    long fileSize = new FileInfo(filePath).Length;

                    // Отправляем информацию о файле
                    byte[] fileNameBytes = Encoding.UTF8.GetBytes(fileName);
                    byte[] fileNameLengthBytes = BitConverter.GetBytes(fileNameBytes.Length);
                    byte[] fileSizeBytes = BitConverter.GetBytes(fileSize);

                    client.Send(fileNameLengthBytes);
                    client.Send(fileNameBytes);
                    client.Send(fileSizeBytes);

                    // Используем SendFile для отправки
                    client.SendFile(filePath);

                    //this.Invoke((MethodInvoker)delegate {
                    //    txtLog.AppendText($"File {fileName} sent successfully!\r\n");
                    //});
                    ShowTransferUI(false, $"Файлик {fileName} успешно отправлен!");
                }
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate {
                    txtLog.AppendText($"Ошибка при отправке файла: {ex.Message}\r\n");
                });
            }
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Нет сетевых адаптеров с IPv4-адресом");
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            isRunning = false;
            serverThread?.Abort();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonOpenChat_Click(object sender, EventArgs e)
        {
            var chatForm = new ClientChat();
            chatForm.Show();
        }
    }
}
