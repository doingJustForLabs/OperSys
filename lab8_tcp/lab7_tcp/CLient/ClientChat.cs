using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace lab7_tcp
{
    public partial class ClientChat: Form
    {
        private Client client;
        private FileTransferClient _fileClient;
        private string localIP;
        private int _chatPort = 8888;
        private int _filePort = 8889;
        private bool isTransferring = false;
        private XPProgressBar xpProgress;

        public ClientChat()
        {
            InitializeComponent();
            client = new Client();
            _fileClient = new FileTransferClient();
            client.MessageReceived += OnMessageReceived;

            localIP = GetLocalIPAddress();
            _fileClient.ProgressChanged += UpdateProgress;


            //gifBox.Image = Properties.Resources.orig;

            //using (var ms = new MemoryStream(Properties.Resources.orig))
            //{
            //    gifBox.Image = Image.FromStream(ms);
            //}

            xpProgress = new XPProgressBar()
            {
                Location = new Point(966, 538),
                Size = new Size(336, 23),
                Minimum = 0,
                Maximum = 100,
                Value = 0
            };
            this.Controls.Add(xpProgress);

            _fileClient.TransferStatusChanged += ShowTransferUI;
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

        private async void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string serverIP = txtServerIP.Text.Trim();

                if (string.IsNullOrWhiteSpace(serverIP))
                {
                    MessageBox.Show("Введите IP сервера");
                    return;
                }

                // Написать потом настойщий айпу, а не локал --- done!
                await client.Connect(serverIP, _chatPort, CancellationToken.None);
                await _fileClient.Connect(serverIP, _filePort);

                richTextBox1.AppendText($"Подключено к чату и файловому серверу на {serverIP}\n");


                buttonSend.Enabled = true;
                btnSendFile.Enabled = true;
            }
            catch (Exception ex)
            {
                //buttonSend.Enabled = false;
                richTextBox1.AppendText($"Ошибка подключения: {ex.Message}\n");
            }
        }

        private async void btnSendFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    await _fileClient.SendFile(openFileDialog.FileName);
                    //txtLog.AppendText($"Файл {Path.GetFileName(openFileDialog.FileName)} отправлен\n");
                    ShowTransferUI(false, $"Файлик {openFileDialog.FileName} успешно отправлен!");
                }
                catch (Exception ex)
                {
                    txtLog.AppendText($"Ошибка отправки файла: {ex.Message}\n");
                }

                //string filePath = openFileDialog.FileName;
                //string receiverIP = txtReceiverIP.Text;

                //if (string.IsNullOrWhiteSpace(receiverIP))
                //{
                //    MessageBox.Show("Пожалуйста, введите IP-адрес получателя");
                //    return;
                //}

                //ShowTransferUI(true, $"Отправляем файл {receiverIP}...");
            }
        }

        private void UpdateProgress(int progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(UpdateProgress), progress);
                return;
            }
            xpProgress.Value = progress;
        }

        private async void buttonSend_Click(object sender, EventArgs e)
        {
            string message = richTextBox2.Text.Trim();
            if (string.IsNullOrEmpty(message))
                return;

            await client.SendMessage(message);
            richTextBox1.AppendText($"Вы: {message}\n");
            richTextBox2.Clear();
            richTextBox2.Focus();
        }

        public void ShowTransferUI(bool show, string message)
        {
            this.Invoke((MethodInvoker)delegate {
                isTransferring = show;
                gifBox.Visible = show;
                if (show) gifBox.BringToFront();
                txtLog.AppendText(message + "\r\n");
                if (!show) xpProgress.Value = 0;
            });
        }

        private void OnMessageReceived(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(OnMessageReceived), message);
                return;
            }
            richTextBox1.AppendText($"Ваша шиза: {message}\n");
        }

        private void ClientChat_Load(object sender, EventArgs e)
        {

        }

        private void ClientChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Disconnect();
        }

        private void TxtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                buttonSend.PerformClick();
            }
        }
    }
}
