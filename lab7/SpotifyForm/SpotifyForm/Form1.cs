using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpotifyForm
{
    public partial class SpotifyForm: Form
    {
        private dynamic _spotifyHook;

        public SpotifyForm()
        {
            InitializeComponent();
        }

        private void LoadHook()
        {
            try
            {
                var assembly = Assembly.LoadFrom("D:\\holn\\Desktop\\Study\\ОС\\lab7\\SpotifyNya\\SpotifyNya\\bin\\Debug\\SpotifyNya.dll");
                var type = assembly.GetType("SpotifyNya.SpotifyControl");

                _spotifyHook = Activator.CreateInstance(type, Keys.F12);
                type.GetMethod("Start").Invoke(_spotifyHook, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке dll Spotify: {ex.Message}");
            }
            
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var disposeMethod = _spotifyHook?.GetType().GetMethod("Dispose");
            disposeMethod?.Invoke(_spotifyHook, null);

            base.OnFormClosing(e);
        }

        private void SpotifyForm_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            LoadHook();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            var disposeMethod = _spotifyHook?.GetType().GetMethod("Dispose");
            disposeMethod?.Invoke(_spotifyHook, null);
            //Process.Start("C:\\Program Files\\WindowsApps\\32669SamG.ModernFlyouts_0.9.3.0_x64__pcy8vm99wrpcg\\ModernFlyoutsHost.exe");
        }
    }
}
