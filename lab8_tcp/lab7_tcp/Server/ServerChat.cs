using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab7_tcp
{
    public partial class ServerChat: Form
    {
        private Server server;
        private FileTransferServer _fileServer;

        public ServerChat()
        {
            InitializeComponent();
            server = new Server();
            _fileServer = new FileTransferServer();

            server.MessageReceived += OnMessageReceived;
            _fileServer.FileReceived += OnFileReceived;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {   
                server.Start(txtServer.Text, 8888);
                _fileServer.Start(txtServer.Text, 8889);
                richTextBox2.AppendText("Сервер запущен...\n"); 
                label1.Text = $"IP сервера: {txtServer.Text}";
            }
            catch (Exception ex)
            {
                richTextBox2.AppendText($"Ошибка запуска: {ex.Message}\n");
            }
        }

        private void OnFileReceived(string fileName, long fileSize)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, long>(OnFileReceived), fileName, fileSize);
                return;
            }

            richTextBox2.AppendText($"Получен файл: {fileName} ({fileSize} bytes)\n");
        }

        private void OnMessageReceived(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(OnMessageReceived), message);
                return;
            }
            richTextBox2.AppendText($"{message}\n");
        }

        private void Chat_Load(object sender, EventArgs e)
        {

        }

        private void ServerChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Stop();
        }
    }
}
