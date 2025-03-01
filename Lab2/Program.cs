using System;
using System.Timers;
using Timer = System.Timers.Timer;

internal class Program
{
    private static void Main(string[] args)
    {
        bool displayInGB = true;
        string logPath = "D:\\Study\\Subjects\\Sem4\\OS\\Lab2\\log.txt";

        StreamWriter logFile = new StreamWriter(logPath);

        Timer timer = new Timer(1500);
        timer.Elapsed += CheckDrives;
        timer.Start();
        

        while (true)
        { 
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Enter)
            {
                logFile.Close();
                break;
            }
            if (key == ConsoleKey.G)
            {
                displayInGB = true;
            }
            if (key == ConsoleKey.M)
            {
                displayInGB = false;
            }
        }

        void CheckDrives(object sender, ElapsedEventArgs e)
        {
            Console.Clear();
            Console.WriteLine("Нажмите G для отображения в ГБ, M для МБ, Enter для выхода\n");

            DriveInfo[] drives = DriveInfo.GetDrives();

            string logEntry = $"[{DateTime.Now}] Доступные диски в системе:\n";
            Console.WriteLine("Доступные диски в системе:");

            foreach (DriveInfo drive in drives)
            {

                string driveInfo = $"Диск: {drive.Name}\n  Тип: {drive.DriveType}\n";
                if (drive.IsReady)
                {
                    if (displayInGB)
                    {
                        long totalSizeGb = drive.TotalSize / (1024 * 1024 * 1024);
                        long freeSpaceGb = drive.AvailableFreeSpace / (1024 * 1024 * 1024);

                        driveInfo += $"  Общий размер: {totalSizeGb} ГБ\n";
                        driveInfo += $"  Занятое место: {totalSizeGb - freeSpaceGb} ГБ\n";
                        driveInfo += $"  Свободное место: {freeSpaceGb} ГБ\n";
                    }
                    else
                    {
                        long totalSizeMb = drive.TotalSize / (1024 * 1024);
                        long freeSpaceMb = drive.AvailableFreeSpace / (1024 * 1024);

                        driveInfo += $"  Общий размер: {totalSizeMb} МБ\n";
                        driveInfo += $"  Занятое место: {totalSizeMb - freeSpaceMb} МБ\n";
                        driveInfo += $"  Свободное место: {freeSpaceMb} МБ\n";
                    }
                }
                Console.WriteLine(driveInfo);
                logEntry += driveInfo;
            }
            logFile.WriteLine(logEntry);
        }
    }
}