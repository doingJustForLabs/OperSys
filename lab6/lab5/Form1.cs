using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab5
{
    public partial class Form1: Form
    {
        private readonly Dictionary<IntPtr, WindowInfo> _itemsCache = new Dictionary<IntPtr, WindowInfo>();

        public Form1()
        {
            InitializeComponent();
            RefreshWindowsList();
            timer1.Start();
            WindowsAPI.OnTitleChanged += new Action<IntPtr, string>(HandleTitleChanged);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        public void RefreshWindowsList()
        {
            listWindows.BeginUpdate();
            listWindows.Items.Clear();

            var windows = WindowsAPI.GetAllWindows();
            foreach (var window in windows)
            {
                listWindows.Items.Add(window);
            }

            listWindows.EndUpdate();
        }

        private void RenameWindow(object sender, EventArgs e)
        {
            if (listWindows.SelectedItem is WindowInfo window && !string.IsNullOrEmpty(textBoxTitle.Text))
            {
                WindowsAPI.SetWindowTitle(window.Handle, textBoxTitle.Text);
            }
        }

        private void ReplaceWindow(object sender, EventArgs e)
        {
            if (listWindows.SelectedItem is WindowInfo window)
            {
                int x = (int)numericPosX.Value;
                int y = (int)numericPosY.Value;
                const uint SWP_NOSIZE = 0x0001;
                const uint SWP_NOZORDER = 0x0004;

                WindowsAPI.SetWindowPositionOrSize(window.Handle, IntPtr.Zero, x, y, 0, 0, SWP_NOSIZE | SWP_NOZORDER);
            }
        }

        private void ResizeWindow(object sender, EventArgs e)
        {
            if (listWindows.SelectedItem is WindowInfo window)
            {
                int width = (int)numericWidth.Value;
                int height = (int)numericHeight.Value;
                const uint NO_MOVE = 0x0002;
                const uint SWP_NOZORDER = 0x0004;

                WindowsAPI.SetWindowPositionOrSize(window.Handle, IntPtr.Zero, 0, 0, width, height, NO_MOVE | SWP_NOZORDER);
            }
        }

        private void HandleTitleChanged(IntPtr hWnd, string newTitle)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<IntPtr, string>(HandleTitleChanged), hWnd, newTitle);
                return;
            }

            for (int i = 0; i < listWindows.Items.Count; i++)
            {
                if (listWindows.Items[i] is WindowInfo info && info.Handle == hWnd)
                {
                    info.Title = newTitle;
                    listWindows.Items[i] = info;
                    listWindows.Invalidate();
                    break;
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            WindowsAPI.OnTitleChanged -= HandleTitleChanged;
            base.OnFormClosing(e);
        }

        private void listWindows_SelectedIndexChanged()
        {
            if (listWindows.SelectedItem is WindowInfo window)
            {
                var info = window.GetFullInfo();

                listInfo.BeginUpdate();

                listInfo.Items.Clear();
                listInfo.Items.AddRange(info.Split('\n'));

                listInfo.EndUpdate();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            listWindows_SelectedIndexChanged();
        }
    }
}
