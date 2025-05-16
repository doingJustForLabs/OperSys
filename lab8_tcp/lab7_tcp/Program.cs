using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab7_tcp
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //var server = new Server();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Task.Run(() => server.Start());
            //Application.Run(new Form1());

            //var serverForm = new ServerChat();
            //new Thread(() => {
            //    Application.Run(serverForm);
            //}).Start();

            // Запускаем главную форму клиента
            Application.Run(new ClientChat());
        }
    }
}
