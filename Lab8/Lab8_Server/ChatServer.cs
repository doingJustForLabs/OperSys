using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Lab8_Server
{
    public class ChatServer
    {
        private readonly List<JObject> messages = new List<JObject>();
        private readonly Dictionary<string, string> clientUsernames = new Dictionary<string, string>();
        private readonly HashSet<string> connectedClients = new HashSet<string>();
        private readonly object lockObj = new object();
        private HttpListener listener;
        private string chatName;
        private int maxUsers;
        private int port;
        private IPAddress localIP;
        private DateTime startTime;

        public void Start()
        {
            Console.Write("Введите IP: ");
            while (!IPAddress.TryParse(Console.ReadLine(), out localIP))
            {
                Console.Write("Неверный IP. Повторите: ");
            }

            Console.Write("Введите номер порта: ");
            while (!int.TryParse(Console.ReadLine(), out port) || port < 1 || port > 65535)
            {
                Console.Write("Неверный порт. Повторите: ");
            }

            Console.Write("Введите название чата: ");
            chatName = Console.ReadLine();

            Console.Write("Введите макс. число участников: ");
            while (!int.TryParse(Console.ReadLine(), out maxUsers) || maxUsers < 1 || maxUsers > 100)
            {
                Console.Write("Недопустимое значение. Повторите: ");
            }

            listener = new HttpListener();
            listener.Prefixes.Add($"http://{localIP}:{port}/chat/");
            listener.Start();
            startTime = DateTime.Now;

            Console.WriteLine($"Сервер {chatName} доступен по адресу: http://{localIP}:{port}/chat/");
            Console.WriteLine("Введите 'help' для списка команд.");
            Console.WriteLine();

            ThreadPool.QueueUserWorkItem(Listen);

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            ChatCommands.Run(this);
        }

        private void Listen(object _)
        {
            while (true)
            {
                try
                {
                    var context = listener.GetContext();
                    string route = context.Request.RawUrl;

                    if (context.Request.HttpMethod == "POST")
                    {
                        var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding);
                        string body = reader.ReadToEnd();
                        JObject data = JObject.Parse(body);
                        string ip = context.Request.RemoteEndPoint?.Address.ToString();
                        string username = data["username"]?.ToString();

                        if (route.EndsWith("/join"))
                        {
                            lock (lockObj)
                            {
                                if (connectedClients.Count >= maxUsers)
                                {
                                    Respond(context, 403, "Чат переполнен.");
                                    return;
                                }

                                connectedClients.Add(ip);
                                clientUsernames[ip] = username;
                                Console.WriteLine($"ПОДКЛЮЧИЛСЯ: {username} ({ip})");
                                var joinMessage = new
                                {
                                    username = chatName,
                                    senderIP = ip,
                                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                    message = $"{username} подключился к чату."
                                };
                                messages.Add(JObject.FromObject(joinMessage));
                            }

                            Respond(context, 200, "OK");
                        }
                        else if (route.EndsWith("/send"))
                        {
                            lock (lockObj)
                            {
                                messages.Add(data);
                                Console.WriteLine($"СООБЩЕНИЕ от {username} ({ip}): {data["message"]}");
                            }

                            Respond(context, 200, "OK");
                        }
                        else if (route.EndsWith("/leave"))
                        {
                            lock (lockObj)
                            {
                                connectedClients.Remove(ip);
                                clientUsernames.Remove(ip);
                                Console.WriteLine($"ОТКЛЮЧИЛСЯ: {username} ({ip})");
                                var leaveMessage = new
                                {
                                    username = chatName,
                                    senderIP = ip,
                                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                    message = $"{username} отключился."
                                };
                                messages.Add(JObject.FromObject(leaveMessage));
                            }

                            Respond(context, 200, "OK");
                        }
                    }
                    else if (context.Request.HttpMethod == "GET" && route.EndsWith("/receive"))
                    {
                        string ip = context.Request.RemoteEndPoint?.Address.ToString();

                        lock (lockObj)
                        {
                            if (!connectedClients.Contains(ip))
                            {
                                Respond(context, 403, "Не подключён.");
                                return;
                            }

                            var payload = new
                            {
                                chatName,
                                users = connectedClients.Count,
                                maxUsers,
                                messages
                            };

                            string json = JsonConvert.SerializeObject(payload);
                            byte[] buffer = Encoding.UTF8.GetBytes(json);
                            context.Response.ContentType = "application/json";
                            context.Response.ContentEncoding = Encoding.UTF8;
                            context.Response.StatusCode = 200;
                            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                            context.Response.Close();
                        }
                    }
                    else
                    {
                        Respond(context, 404, "Неизвестный путь.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ОШИБКА {ex.Message}");
                }
            }
        }

        private void Respond(HttpListenerContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.Close();
        }

        public void Stop()
        {
            Console.WriteLine("Остановка сервера...");

            lock (lockObj)
            {
                if (listener != null && listener.IsListening)
                {
                    listener.Stop();
                    listener.Close();
                    Console.WriteLine("Сервер остановлен.");
                }

                connectedClients.Clear();
                clientUsernames.Clear();
                messages.Clear();
                Console.WriteLine("Данные удалены.");
            }
        }

        private void OnProcessExit(object sender, EventArgs e)
        {
            Stop();
        }

        public int GetPort() => port;
        public string GetIP() => localIP.ToString();
        public string GetChatName() => chatName;
        public void SetChatName(string name) => chatName = name;
        public int GetMaxUsers() => maxUsers;
        public void SetMaxUsers(int max) => maxUsers = max;
        public int GetUserCount() => connectedClients.Count;
        public DateTime GetStartTime() => startTime;
        public Dictionary<string, string> GetClients() => clientUsernames;
        public void ClearMessages()
        {
            lock (lockObj) messages.Clear();
        }
    }
}
