using System;

namespace Lab8_Server
{
    public static class ChatCommands
    {
        public static void Run(ChatServer server)
        {
            bool serverRunning = false;

            while (true)
            {
                string input = Console.ReadLine()?.Trim().ToLower();

                if (input == null) continue;

                string[] parts = input.Split(' ');

                if (parts.Length == 0) continue;

                switch (parts[0])
                {
                    case "help":
                        Console.WriteLine("help — список команд");
                        Console.WriteLine("clear — очистить чат");
                        Console.WriteLine("info — показать инфо о чате");
                        Console.WriteLine("name <имя> — изменить имя чата");
                        Console.WriteLine("max <число> — изменить макс. число участников");
                        Console.WriteLine("run — запустить сервер");
                        Console.WriteLine("stop — остановить сервер");
                        Console.WriteLine();
                        break;

                    case "clear":
                        server.ClearMessages();
                        Console.WriteLine("Чат очищен.\n");
                        break;

                    case "info":
                        Console.WriteLine($"Название: {server.GetChatName()}");
                        Console.WriteLine($"Создан: {server.GetStartTime()}");
                        Console.WriteLine($"IP: {server.GetIP()}");
                        Console.WriteLine($"Порт: {server.GetPort()}");
                        Console.WriteLine($"Пользователи: {server.GetUserCount()}/{server.GetMaxUsers()}");
                        int index = 1;
                        foreach (var kv in server.GetClients())
                        {
                            Console.WriteLine($"{index++}. {kv.Value} ({kv.Key})");
                        }
                        Console.WriteLine();
                        break;

                    case "name":
                        if (parts.Length < 2) Console.WriteLine("Используйте: name <новое имя>\n");
                        else
                        {
                            server.SetChatName(parts[1]);
                            Console.WriteLine("Имя чата обновлено.\n");
                        }
                        break;

                    case "max":
                        if (parts.Length < 2 || !int.TryParse(parts[1], out int max) || max <= 0)
                            Console.WriteLine("Используйте: max <число> (больше 0)\n");
                        else
                        {
                            server.SetMaxUsers(max);
                            Console.WriteLine("Максимальное число обновлено.\n");
                        }
                        break;

                    case "run":
                        if (serverRunning)
                        {
                            Console.WriteLine("Сервер уже запущен.\n");
                        }
                        else
                        {
                            server.Start();
                            serverRunning = true;
                            Console.WriteLine("Сервер запущен.\n");
                        }
                        break;

                    case "stop":
                        if (!serverRunning)
                        {
                            Console.WriteLine("Сервер не запущен.\n");
                        }
                        else
                        {
                            server.Stop();
                            serverRunning = false;
                            Console.WriteLine("Сервер остановлен.\n");
                        }
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда. Введите 'help' для справки.\n");
                        break;
                }
            }
        }
    }
}
