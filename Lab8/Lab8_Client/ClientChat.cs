using System;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Lab8
{
    public partial class ChatForm : Form
    {
        private readonly HttpClient client = new HttpClient();
        private bool isConnected = false;
        private string username = "anon";
        private string serverUrl = null;
        private int lastMessageCount = 0;

        public ChatForm()
        {
            InitializeComponent();
            dataGridViewChat.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewChat.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        string InsertLineBreaks(string text, int maxLength)
        {
            var sb = new StringBuilder();
            var words = text.Split(' ');
            var currentLine = new StringBuilder();

            foreach (var word in words)
            {
                if (currentLine.Length + word.Length + (currentLine.Length > 0 ? 1 : 0) <= maxLength)
                {
                    if (currentLine.Length > 0)
                        currentLine.Append(' ');
                    currentLine.Append(word);
                }
                else
                {
                    if (currentLine.Length > 0)
                    {
                        sb.AppendLine(currentLine.ToString());
                        currentLine.Clear();
                    }
                    currentLine.Append(word);
                }
            }

            if (currentLine.Length > 0)
                sb.Append(currentLine.ToString());

            return sb.ToString();
        }


        private string GetLocalIPAddress()
        {
            return Dns.GetHostEntry(Dns.GetHostName())
                      .AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                      ?.ToString() ?? "unknown";
        }

        private async void TimerReceive_Tick(object sender, EventArgs e)
        {
            buttonConnect.Visible = !isConnected;
            buttonDisconnect.Visible = isConnected;
            buttonSend.Enabled = isConnected;
            textBoxMessage.Enabled = isConnected;

            if (!isConnected || string.IsNullOrEmpty(serverUrl)) return;

            try
            {
                var response = await client.GetAsync($"{serverUrl}/receive");
                string json = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(json);

                var messages = data["messages"];
                string title = $"{data["chatName"]} {data["users"]}/{data["maxUsers"]}";
                groupBoxChat.Text = title;

                if (messages.Count() != lastMessageCount)
                {
                    int firstRow = dataGridViewChat.FirstDisplayedScrollingRowIndex;
                    bool atBottom = dataGridViewChat.DisplayedRowCount(false) + firstRow >= dataGridViewChat.RowCount - 1;

                    dataGridViewChat.Rows.Clear();
                    foreach (var msg in messages)
                    {
                        string formatted = $"[{msg["date"]}] {msg["username"]}: {msg["message"]}";
                        dataGridViewChat.Rows.Add(InsertLineBreaks(formatted, 50));
                    }

                    lastMessageCount = messages.Count();

                    if (atBottom && dataGridViewChat.RowCount > 0)
                        dataGridViewChat.FirstDisplayedScrollingRowIndex = dataGridViewChat.RowCount - 1;
                    else if (!atBottom && firstRow >= 0 && dataGridViewChat.RowCount > firstRow)
                        dataGridViewChat.FirstDisplayedScrollingRowIndex = firstRow;
                }
            }
            catch (Exception ex)
            {
                dataGridViewChat.Rows.Clear();
                dataGridViewChat.Rows.Add($"Ошибка получения: {ex.Message}");
            }
        }

        private async void buttonSend_Click(object sender, EventArgs e)
        {
            string msgText = textBoxMessage.Text.Trim();
            if (msgText.Length == 0 || !isConnected) return;

            var message = new
            {
                username,
                senderIP = GetLocalIPAddress(),
                date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                message = msgText
            };

            try
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(message);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync($"{serverUrl}/send", content);
                textBoxMessage.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка отправки: {ex.Message}");
            }
        }

        private async void buttonJoin_Click(object sender, EventArgs e)
        {
            string ip = comboBoxServerIP.Text.Trim();

            if (string.IsNullOrEmpty(ip))
            {
                MessageBox.Show("Введите IP сервера.");
                return;
            }

            serverUrl = $"http://{ip}/chat";
            Text = serverUrl;
            username = "anon";

            using (var dialog = new ConnectionDialogue(username))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    username = dialog.Nickname;
                }
                else return;
            }

            try
            {
                var joinData = new
                {
                    username,
                    senderIP = GetLocalIPAddress()
                };
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(joinData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{serverUrl}/join", content);
                if (response.IsSuccessStatusCode)
                {
                    isConnected = true;
                    if (!comboBoxServerIP.Items.Contains(ip))
                        comboBoxServerIP.Items.Add(ip);

                    comboBoxServerIP.SelectedItem = ip;
                    timerUpdate.Enabled = true;
                }
                else
                {
                    MessageBox.Show($"Ошибка подключения: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
            }
        }

        private async void buttonDisconnect_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                try
                {
                    var leaveData = new
                    {
                        username,
                        senderIP = GetLocalIPAddress()
                    };
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(leaveData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    await client.PostAsync($"{serverUrl}/leave", content);
                }
                catch { }
            }

            isConnected = false;
            serverUrl = "";
            timerUpdate.Enabled = false;
            dataGridViewChat.Rows.Clear();
            groupBoxChat.Text = "Вы не подключены к чату";
            buttonDisconnect.Visible = false;
            buttonConnect.Visible = true;
        }

        private async void buttonExit_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                try
                {
                    var leaveData = new
                    {
                        username,
                        senderIP = GetLocalIPAddress()
                    };
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(leaveData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    await client.PostAsync($"{serverUrl}/leave", content);
                }
                catch { }
            }
            Close();
        }

        private async void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isConnected)
            {
                try
                {
                    var leaveData = new
                    {
                        username,
                        senderIP = GetLocalIPAddress()
                    };
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(leaveData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    await client.PostAsync($"{serverUrl}/leave", content);
                }
                catch { }
            }
        }

    }
}
