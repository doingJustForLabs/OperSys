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

namespace _1
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            listView.Items.Clear();

            string rootPath = pathTextBox.Text;

            foreach (var dirPath in Directory.EnumerateDirectories(rootPath, "*", SearchOption.TopDirectoryOnly))
            {
                string folderName = new DirectoryInfo(dirPath).Name;
                string[] values = { folderName, "Папка",  findDirSize(dirPath).ToString() + "Kb"};

                listView.Items.Add(new ListViewItem(values));
            }

            foreach (var filePath in Directory.EnumerateFiles(rootPath, "*.*", SearchOption.TopDirectoryOnly))
            {
                string fileName = new FileInfo(filePath).Name;
                long fileSize = new FileInfo(filePath).Length / 1024;

                string[] values = { fileName, "Файл", fileSize.ToString() + " Kb"};

                listView.Items.Add(new ListViewItem(values));

            }
        }

        private long findDirSize(string rootDirPath)
        {
            long res = 0;

            foreach (var dirPath in Directory.EnumerateDirectories(rootDirPath, "*", SearchOption.TopDirectoryOnly))
            {
                res += findDirSize(dirPath);
            }

            foreach (var filePath in Directory.EnumerateFiles(rootDirPath, "*", SearchOption.TopDirectoryOnly))
            {
                
                long fileSize = new FileInfo(filePath).Length / 1024;
                res += fileSize;

            }

            return res;
        }
    }
}
