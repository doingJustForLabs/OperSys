using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace _2
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string logPath = "D:\\Study\\Subjects\\Sem4\\OS\\Test\\2\\log.txt";
            StreamWriter logFile = new StreamWriter(logPath);

            DriveInfo[] drives = DriveInfo.GetDrives();
            string logEntry = $"Доступные диски в системе:\n";

            foreach (DriveInfo drive in drives)
            {

                string driveInfo = $"Диск: {drive.Name}\n  Тип: {drive.DriveType}\n";
                if (drive.IsReady)
                {
                    long totalSizeGb = drive.TotalSize / (1024 * 1024 * 1024);
                    long freeSpaceGb = drive.AvailableFreeSpace / (1024 * 1024 * 1024);

                    driveInfo += $"  Общий размер: {totalSizeGb} ГБ\n";
                    driveInfo += $"  Занятое место: {totalSizeGb - freeSpaceGb} ГБ\n";
                    driveInfo += $"  Свободное место: {freeSpaceGb} ГБ\n";
                    
                }
                logEntry += driveInfo;
            }
            logRichTextBox.Text += logEntry;
            logFile.WriteLine(logEntry);
            logFile.Close();
        }
    }
}
