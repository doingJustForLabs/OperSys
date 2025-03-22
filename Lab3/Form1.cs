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
using Microsoft.VisualBasic.FileIO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Lab3
{
    public partial class Form1 : Form
    {
        private ToolStripMenuItem pasteMenuItem;
        private string copiedPath = null;
        private bool isFolder = false;

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

        private void TreeView_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            string path = e.Node.Tag.ToString();
            string info = GetItemInfo(path);
            toolTip1.SetToolTip(treeView, info);
        }

        private void TreeView_MouseUp(object sender, MouseEventArgs e)
        {
            contextMenuStrip1.Items.Clear();
            contextMenuStrip1.Items.Add("Переименовать", null, RenameFolder);
            contextMenuStrip1.Items.Add("Удалить", null, DeleteFolder);
            contextMenuStrip1.Items.Add("Копировать", null, CopyItem);

            pasteMenuItem = new ToolStripMenuItem("Вставить", null, PasteItem)
            {
                Enabled = copiedPath != null
            };
            contextMenuStrip1.Items.Add(pasteMenuItem);

            if (e.Button == MouseButtons.Right)
            {
                treeView.SelectedNode = treeView.GetNodeAt(e.X, e.Y);
                if (treeView.SelectedNode != null)
                {
                    contextMenuStrip1.Show(treeView, e.Location);
                }
            }
        }

        private void treeView_MouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            string path = e.Node.Tag?.ToString();
            if (!string.IsNullOrEmpty(path))
            {
                string info = GetItemInfo(path);
                Point cursorPos = Cursor.Position;
                int offsetX = 15, offsetY = 15;

                toolTip1.Hide(treeView);
                toolTip1.Show(info, treeView, treeView.PointToClient(new Point(cursorPos.X + offsetX, cursorPos.Y + offsetY)), 3000);
            }
        }

        private string GetItemInfo(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                int fileCount = dirInfo.GetFiles().Length;
                int folderCount = dirInfo.GetDirectories().Length;

                return $"Папка: {dirInfo.Name}\n" +
                       $"Создана: {dirInfo.CreationTime}\n" +
                       $"Файлов: {fileCount}, Подпапок: {folderCount}";
            }
            else if (File.Exists(path))
            {
                FileInfo fileInfo = new FileInfo(path);
                return $"Файл: {fileInfo.Name}\n" +
                       $"Размер: {fileInfo.Length / 1024.0:F2} KB\n" +
                       $"Тип: {fileInfo.Extension}\n" +
                       $"Изменён: {fileInfo.LastWriteTime}";
            }
            return "Неизвестный объект";
        }

        private void DeleteFolder(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                string path = treeView.SelectedNode.Tag.ToString();

                if (Path.GetPathRoot(path) == path)
                {
                    MessageBox.Show("Ага, еще чего удалять корневой диск", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var res = MessageBox.Show($"Вы уверены, что хотите удалить папку: {path}?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                {
                    FileSystem.DeleteDirectory(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                    MessageBox.Show("Папка перемещена в корзину.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    treeView.Nodes.Remove(treeView.SelectedNode);
                } 
            }
        }
        private void RenameFolder(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                string oldPath = treeView.SelectedNode.Tag.ToString();
                string parentPath = Directory.GetParent(oldPath)?.FullName;

                if (Path.GetPathRoot(oldPath) == oldPath)
                {
                    MessageBox.Show("Не надо переименовывать диск)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string newName = Microsoft.VisualBasic.Interaction.InputBox("Введите новое имя:", "Переименование", Path.GetFileName(oldPath));

                if (string.IsNullOrWhiteSpace(newName))
                {
                    MessageBox.Show("Имя не может быть пустым!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string newPath = Path.Combine(parentPath, newName);

                if (Directory.Exists(newPath))
                {
                    MessageBox.Show("Папка с таким именем уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    Directory.Move(oldPath, newPath);
                    treeView.SelectedNode.Text = newName;
                    treeView.SelectedNode.Tag = newPath;
                    MessageBox.Show("Папка переименована!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка переименования: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        

        

        private void createButton_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                string parentPath = treeView.SelectedNode.Tag.ToString();
                string newFolderPath = Path.Combine(parentPath, "Новая папка");

                int counter = 1;
                while (Directory.Exists(newFolderPath))
                {
                    newFolderPath = Path.Combine(parentPath, $"Новая папка ({counter++})");
                }

                Directory.CreateDirectory(newFolderPath);
                MessageBox.Show("Папка создана!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (treeView.SelectedNode.Nodes.Count == 1 && treeView.SelectedNode.Nodes[0].Text == "Загрузка...")
                {
                    treeView.SelectedNode.Nodes.Clear();
                }

                TreeNode newNode = new TreeNode(Path.GetFileName(newFolderPath)) { Tag = newFolderPath };
                newNode.Nodes.Add("Загрузка...");
                treeView.SelectedNode.Nodes.Add(newNode);
                treeView.SelectedNode.Expand();
            }
        }

          

        private void CopyItem(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                copiedPath = treeView.SelectedNode.Tag.ToString();
                isFolder = Directory.Exists(copiedPath); 

                MessageBox.Show($"Скопирован {(isFolder ? "каталог" : "файл")}!", "Копирование", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (pasteMenuItem != null)
                {
                    pasteMenuItem.Enabled = true;
                }
            }
        }

        private void PasteItem(object sender, EventArgs e)
        { 
            if (copiedPath != null && treeView.SelectedNode != null)
            {
                string destinationPath = treeView.SelectedNode.Tag.ToString();
                string newItemPath = Path.Combine(destinationPath, Path.GetFileName(copiedPath));

                try
                {
                    if (isFolder)
                    {
                        CopyDirectory(copiedPath, newItemPath);
                    }
                    else
                    {
                        File.Copy(copiedPath, newItemPath, true);
                    }

                    MessageBox.Show($"{(isFolder ? "Папка" : "Файл")} вставлен!", "Вставка", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    TreeNode newNode = new TreeNode(Path.GetFileName(newItemPath)) { Tag = newItemPath };
                    treeView.SelectedNode.Nodes.Add(newNode);
                    treeView.SelectedNode.Expand();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CopyDirectory(string sourceDir, string destinationDir)
        {
            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            foreach (var dir in Directory.GetDirectories(sourceDir))
            {
                string destDir = Path.Combine(destinationDir, Path.GetFileName(dir));
                CopyDirectory(dir, destDir);
            }
        }

    }
}
