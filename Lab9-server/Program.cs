using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Server
{
    static TcpListener listener;
    static List<ClientHandler> clients = new List<ClientHandler>();
    static object lockObj = new object();

    static void Main()
    {
        Console.Write("Введите IP-адрес для запуска сервера: ");
        string ipInput = Console.ReadLine();

        if (!IPAddress.TryParse(ipInput, out IPAddress ipAddress))
        {
            Console.WriteLine("Некорректный IP-адрес. Используется 127.0.0.1 по умолчанию.");
            ipAddress = IPAddress.Loopback;
        }

        listener = new TcpListener(ipAddress, 5000);
        listener.Start();
        Console.WriteLine($"Сервер запущен на {ipAddress}:5000 и ждет подключений...");

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            ClientHandler handler = new ClientHandler(client);
            lock (lockObj)
            {
                clients.Add(handler);
            }
            new Thread(() => handler.HandleClient(clients, lockObj)).Start();
        }
    }
}

class ClientHandler
{
    TcpClient client;
    NetworkStream stream;
    string clientName;

    public ClientHandler(TcpClient client)
    {
        this.client = client;
        this.stream = client.GetStream();
    }

    public void HandleClient(List<ClientHandler> clients, object lockObj)
    {
        try
        {
            // Сначала получаем имя клиента
            byte[] buffer = new byte[1024];
            int byteCount = stream.Read(buffer, 0, buffer.Length);
            clientName = Encoding.UTF8.GetString(buffer, 0, byteCount);

            Broadcast($"{clientName} подключился к чату.", clients, lockObj);

            while (true)
            {
                byteCount = stream.Read(buffer, 0, buffer.Length);
                if (byteCount == 0) break;

                string message = Encoding.UTF8.GetString(buffer, 0, byteCount);
                Broadcast($"{clientName}: {message}", clients, lockObj);
            }
        }
        catch { }

        finally
        {
            lock (lockObj)
            {
                clients.Remove(this);
            }
            Broadcast($"{clientName} покинул чат.", clients, lockObj);
            client.Close();
        }
    }

    void Broadcast(string message, List<ClientHandler> clients, object lockObj)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        lock (lockObj)
        {
            foreach (var client in clients)
            {
                try
                {
                    client.stream.Write(data, 0, data.Length);
                }
                catch { }
            }
        }
        Console.WriteLine(message);
    }
}
