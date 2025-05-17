using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Lab9_client
{
    public partial class Form1 : Form
    {
        TcpClient client;
        NetworkStream stream;
        Thread receiveThread;

        public Form1()
        {
            InitializeComponent();
        }

        private void ReceiveMessages()
        {
            byte[] buffer = new byte[1024];
            while (true)
            {
                try
                {
                    int byteCount = stream.Read(buffer, 0, buffer.Length);
                    if (byteCount == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, byteCount);
                    Invoke((MethodInvoker)(() => AppendChat(message)));
                }
                catch
                {
                    break;
                }
            }
        }

        private void AppendChat(string message)
        {
            chatRichTextBox.AppendText(message + Environment.NewLine);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                stream?.Close();
                client?.Close();
                receiveThread?.Abort();
            }
            catch { }
        }

        private void connectBtn_Click(object sender, EventArgs e)   
        {
            try
            {
                client = new TcpClient();
                client.Connect(serverIpTextBox.Text, 5000);

                MessageBox.Show("Пользователь подключился");

                stream = client.GetStream();

                byte[] nameBytes = Encoding.UTF8.GetBytes(nameTextBox.Text);
                stream.Write(nameBytes, 0, nameBytes.Length);

                receiveThread = new Thread(ReceiveMessages);
                receiveThread.IsBackground = true;
                receiveThread.Start();

                connectBtn.Enabled = false;
                serverIpTextBox.Enabled = false;
                nameTextBox.Enabled = false;
                sendTextBtn.Enabled = true;

                AppendChat("Вы подключились к чату.");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения: " + ex.Message);
            }
        }

        private void sendTextBtn_Click(object sender, EventArgs e)
        {
            if (stream != null && stream.CanWrite)
            {
                SendMessage();
            }
        }

        private void SendMessage()
        {
            string message = messageTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
                messageTextBox.Clear();
            }
        }

        private void messageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; 
                SendMessage();
            }
        }
    }
}
