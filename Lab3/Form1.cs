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
using System.Xml.Linq;

namespace Lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadDrives();
        }

        private void LoadDrives()
        {
            treeView.Nodes.Clear();
            foreach (var drive in DriveInfo.GetDrives())
            {
                TreeNode node = new TreeNode(drive.Name) { Tag = drive.Name};
                node.Nodes.Add("Загрузка...");
                treeView.Nodes.Add(node);
            }
        }

        private void LoadDirectories(TreeNode parentNode)
        {
            try
            {
                string path = parentNode.Tag.ToString();
                foreach (var dir in Directory.EnumerateDirectories(path))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);

                    if ((dirInfo.Attributes & FileAttributes.Hidden) == 0 &&
                        (dirInfo.Attributes & FileAttributes.System) == 0)
                    {
                        TreeNode node = new TreeNode(Path.GetFileName(dir)) { Tag = dir };
                        node.Nodes.Add("Загрузка...");
                        parentNode.Nodes.Add(node);
                    }
                }
                foreach (var file in Directory.EnumerateFiles(path))
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if ((fileInfo.Attributes & FileAttributes.Hidden) == 0)
                    {
                        TreeNode node = new TreeNode(fileInfo.Name) { Tag = file };
                        parentNode.Nodes.Add(node);
                    }
                }
            }
            catch (Exception) {  }
        }

        private void TreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Nodes.Count == 1 && node.Nodes[0].Text == "Загрузка...")
            {
                node.Nodes.Clear(); 
                LoadDirectories(node); 
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            pathTextBox.Text = node.FullPath;
        }


    }
}
