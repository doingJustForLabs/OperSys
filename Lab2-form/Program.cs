using System;
using System.IO;
using System.Windows.Forms;

namespace Lab2_form
{
    public class DiskMonitorForm : Form
    {
        private ListView listView;
        private Timer timer;

        public DiskMonitorForm()
        {
            this.Text = "Disk Monitor";
            this.Width = 400;
            this.Height = 300;

            listView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details
            };
            this.Controls.Add(listView);
            LoadDrives();

            timer = new Timer();
            timer.Interval = 5000; // 5 секунд
            timer.Tick += (sender, e) => LoadDrives();
            timer.Start();
        }
        private void LoadDrives()
        {
            listView.Items.Clear();
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    listView.Items.Add(new ListViewItem(new[]
                    {
                    drive.Name,
                    drive.DriveType.ToString(),
                    $"{drive.TotalFreeSpace / (1024 * 1024 * 1024)} ГБ"
                }));
                }
            }
        }


            /// <summary>
            /// Главная точка входа для приложения.
            /// </summary>
            [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
