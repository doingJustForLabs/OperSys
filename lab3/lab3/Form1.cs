using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using Shell32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
//using Shell32;

namespace lab3
{
    public partial class Form1: Form
    {
        private bool isRecycleBinShown = false; // Флаг для отслеживания состояния


        private string currentDirectory;

        public Form1()
        {
            InitializeComponent();

            currentDirectory = "D:\\holn\\Desktop\\test";

            textBoxPath.Text = currentDirectory;
            LoadDirectory(currentDirectory);

            // обработчик для отслеживания изменений в папке вне программы
            fileSystemWatcher.Path = Path.GetDirectoryName(currentDirectory);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


       private void LoadDirectory(string path)
        {
            // Сбрасываем флаг
            isRecycleBinShown = false;
            UpdateButtonText();

            listView1.Items.Clear();
            currentDirectory = path;
            textBoxPath.Text = path;
            fileSystemWatcher.Path = Path.GetDirectoryName(path);
            fileSystemWatcher.Filter = Path.GetFileName(path);
            //fileSystemWatcher.Path = path;

            try
            {
                foreach (string dir in Directory.GetDirectories(path))
                {
                    listView1.Items.Add(Path.GetFileName(dir)).SubItems.Add("Папка");
                }

                foreach (string file in Directory.GetFiles(path))
                {
                    listView1.Items.Add(Path.GetFileName(file)).SubItems.Add("Файл");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки директории:\n{ex.Message}");
            }
        }

        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string selectedItem = listView1.SelectedItems[0].Text;
                string selectedPath = Path.Combine(currentDirectory, selectedItem);

                if (Directory.Exists(selectedPath))
                {
                    LoadDirectory(selectedPath);
                }
            }
        }

        private void TextBoxPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string newPath = textBoxPath.Text;
                if (Directory.Exists(newPath))
                {
                    LoadDirectory(newPath);
                }
            }
        }

        private void SelectPathButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Выберите путь";
                folderDialog.SelectedPath = currentDirectory;

                if(folderDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxPath.Text = folderDialog.SelectedPath;
                    LoadDirectory(folderDialog.SelectedPath);
                }
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            //string fileName = textBoxFileName.Text;
            //string fullPath = Path.Combine(currentDirectory, fileName);

            string itemName = Interaction.InputBox(
                "Введите имя файла или папки.\nДля создания папки напишите в конце \\ или /",
                "Создание",
                "");

            if (string.IsNullOrEmpty(itemName))
            {
                MessageBox.Show("Имя не может быть пустым.");
                return;
            }

            CreateItem(itemName);
        }

        private void CreateItem(string itemName)
        {
            try
            {
                string fullPath = Path.Combine(currentDirectory, itemName);
                fullPath = GenerateUniquePath(fullPath);
                if (File.Exists(fullPath) || Directory.Exists(fullPath))
                {
                    
                    MessageBox.Show("Файл или папка с таким именем уже существует.");
                    return;
                }

                if (itemName.EndsWith("\\") || itemName.EndsWith("/"))
                {
                    Directory.CreateDirectory(fullPath);
                }
                else
                {
                    File.Create(fullPath).Close();
                }

                LoadDirectory(currentDirectory);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании: ", ex.Message);
            }
        }

        private void RenameButton_Click(object sender, EventArgs e)
        {
            string sourcePath = GetSelectedItemPath();
            if (sourcePath == null) return;

            string oldName = Path.GetFileName(sourcePath);
            //string oldPath = Path.Combine(currentDirectory, oldName);

            string newName = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите новое имя: ",
                "Переименование",
                oldName);

            if (string.IsNullOrEmpty(newName) || newName == oldName)
            {
                MessageBox.Show("Имя не изменено.");
                return;
            }

            string newPath = Path.Combine(currentDirectory, newName);

            try
            {
                if (Directory.Exists(sourcePath))
                {
                    Directory.Move(sourcePath, newPath);
                }
                else if (File.Exists(sourcePath))
                {
                    File.Move(sourcePath, newPath);
                }
                else
                {
                    MessageBox.Show("Выбранный элемент не существует");
                    return;
                }
                LoadDirectory(currentDirectory);


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при переименовании:\n{ex.Message}");
            }
        }


        private void CopyButton_Click(object sender, EventArgs e)
        {
            string sourcePath = GetSelectedItemPath();
            if (sourcePath == null) return;

            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Выберите целевую папку для копирования";
                folderDialog.SelectedPath = currentDirectory;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string destinationPath = Path.Combine(folderDialog.SelectedPath, Path.GetFileName(sourcePath));

                    // Генерируем уникальное имя с суффиксом "копия"
                    destinationPath = GenerateUniqueCopyName(destinationPath);

                    try
                    {
                        if (Directory.Exists(sourcePath))
                        {
                            CopyDirectory(sourcePath, destinationPath, overwrite: true);
                            MessageBox.Show("Папка успешно скопирована.");
                        }
                        else if (File.Exists(sourcePath))
                        {
                            CopyFileWithWinAPI(sourcePath, destinationPath, overwrite: true);
                            MessageBox.Show("Файл успешно скопирован.");
                        }
                        else
                        {
                            MessageBox.Show("Выбранный элемент не существует.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при копировании: {ex.Message}");
                    }
                }
            }
        }

        private void CopyDirectory(string sourceDir, string destinationDir, bool overwrite)
        {
            try
            {
                if (!Directory.Exists(destinationDir))
                {
                    Directory.CreateDirectory(destinationDir);
                }

                foreach (string dir in Directory.GetDirectories(sourceDir))
                {
                    string destDir = Path.Combine(destinationDir, Path.GetFileName(dir));
                    CopyDirectory(dir, destDir, overwrite: true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при копировании папки:\n{ex.Message}");
            }
        }

        // Генератор уникальных имен для копий
        private string GenerateUniqueCopyName(string originalPath)
        {
            string directory = Path.GetDirectoryName(originalPath);
            string fileName = Path.GetFileNameWithoutExtension(originalPath);
            string extension = Path.GetExtension(originalPath);
            int counter = 1;

            string newPath = originalPath;

            // Проверяем существование и добавляем суффикс
            while (File.Exists(newPath) || Directory.Exists(newPath))
            {
                string tempName = $"{fileName} - копия";
                if (counter > 1) tempName += $" {counter}";
                newPath = Path.Combine(directory, tempName + extension);
                counter++;
            }

            return newPath;
        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            string sourcePath = GetSelectedItemPath();
            if (sourcePath == null) return;

            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Выберите целевую папку для перещения";
                folderDialog.SelectedPath = currentDirectory;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string destinationPath = Path.Combine(folderDialog.SelectedPath, Path.GetFileName(sourcePath));

                    // Перемещаем файл или папку
                    MoveItem(sourcePath, destinationPath);

                    // Обновляем содержимое текущей директории
                    LoadDirectory(currentDirectory);
                }
            }

        }

        private void MoveItem(string sourcePath, string destinationPath)
        {
            try
            {
                if (Directory.Exists(sourcePath))
                {
                    // Перемещаем папку
                    Directory.Move(sourcePath, destinationPath);
                    MessageBox.Show("Папка успешно перемещена.");
                }
                else if (File.Exists(sourcePath))
                {
                    // Перемещаем файл
                    File.Move(sourcePath, destinationPath);
                    MessageBox.Show("Файл успешно перемещен.");
                }
                else
                {
                    MessageBox.Show("Выбранный элемент не существует.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при перемещении: {ex.Message}");
            }
        }

        private void CopyFileWithWinAPI(string sourceFile, string destinattionFile, bool overwrite)
        {
            try
            {
                bool result = WinAPI.CopyFile(sourceFile, destinattionFile, !overwrite);

                if (!result)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    MessageBox.Show($"Ошибка при копировании:\n{errorCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка:\n{ex.Message}");
            }
        }

        private void SizeIndButton_Click(object sender, EventArgs e)
        {
            string itemPath = GetSelectedItemPath();
            if (itemPath == null) return;

            long sizeInBytes = GetItemSize(itemPath);

            string sizeFormatted = FormatSize(sizeInBytes);
            MessageBox.Show($"Размер выбранного элемента: {sizeFormatted}");
        }

        private long GetItemSize(string itemPath)
        {
            if (File.Exists(itemPath))
            {
                return GetFileSize(itemPath);
            }
            else if (Directory.Exists(itemPath))
            {
                return GetDirectorySize(itemPath);
            }
            else
            {
                MessageBox.Show("Выбранный элемент не существует.");
                return 0;
            }
        }

        private long GetFileSize(string filePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                return fileInfo.Length;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении размера файла: {ex.Message}");
                return 0;
            }
        }

        private long GetDirectorySize(string directoryPath)
        {
            try
            {
                long size = 0;

                // Суммируем размер всех файлов в текущей папке
                foreach (string file in Directory.GetFiles(directoryPath))
                {
                    size += GetFileSize(file);
                }

                // Рекурсивно суммируем размер подпапок
                foreach (string dir in Directory.GetDirectories(directoryPath))
                {
                    size += GetDirectorySize(dir);
                }

                return size;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении размера папки: {ex.Message}");
                return 0;
            }
        }

        // красивый вывод размера файла или папки
        private string FormatSize(long sizeInBytes)
        {
            string[] sizes = { "Б", "КБ", "МБ", "ГБ", "ТБ" };
            int order = 0;
            double size = sizeInBytes;

            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size /= 1024;
            }

            return $"{size:0.##} {sizes[order]}";
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            // обновляем ListView при любом изменении извне (создание, удаление, переименование и тд)
            this.Invoke((MethodInvoker)delegate
            {
                LoadDirectory(currentDirectory);
            });
        }

        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (e.OldFullPath.Equals(currentDirectory, StringComparison.OrdinalIgnoreCase))
                {
                    currentDirectory = e.FullPath;
                    textBoxPath.Text = e.FullPath;
                    fileSystemWatcher.Path = e.FullPath;

                    fileSystemWatcher.Path = Path.GetDirectoryName(currentDirectory);
                    fileSystemWatcher.Filter = Path.GetFileName(currentDirectory);
                }
                LoadDirectory(currentDirectory);
            });
        }

        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (e.FullPath.Equals(currentDirectory, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Папка была удалена.");

                    currentDirectory = Path.GetDirectoryName(currentDirectory);

                    LoadDirectory(currentDirectory);
                }
            });
        }

        private string GetSelectedItemPath()
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите файл или папку.");
                return null;
            }

            ListViewItem selectedItem = listView1.SelectedItems[0];
            return Path.Combine(currentDirectory, selectedItem.Text);
        }

        private void InfoButton_Click(object sender, EventArgs e)
        {
            string itemPath = GetSelectedItemPath();
            if (itemPath == null) return;

            string itemInfo = GetItemInfo(itemPath);

            MessageBox.Show(itemInfo, "Информация о элементе");
        }

        private string GetItemInfo(string itemPath)
        {
            try
            {
                if (File.Exists(itemPath))
                {
                    FileInfo fileInfo = new FileInfo(itemPath);
                    return $"Имя: {fileInfo.Name}\n" +
                           $"Размер: {FormatSize(fileInfo.Length)}\n" +
                           $"Дата создания: {fileInfo.CreationTime}\n" +
                           $"Дата изменения: {fileInfo.LastWriteTime}\n" +
                           $"Расширение: {fileInfo.Extension}";
                }
                else if (Directory.Exists(itemPath))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(itemPath);
                    return $"Имя: {dirInfo.Name}\n" +
                           $"Размер: {FormatSize(GetDirectorySize(itemPath))}\n" +
                           $"Дата создания: {dirInfo.CreationTime}\n" +
                           $"Дата изменения: {dirInfo.LastWriteTime}\n" +
                           $"Количество файлов: {dirInfo.GetFiles().Length}\n" +
                           $"Количество папок: {dirInfo.GetDirectories().Length}";
                }
                else
                {
                    return "Выбранный элемент не существует.";
                }
            }
            catch (Exception ex)
            {
                return $"Ошибка при получении информации: {ex.Message}";
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string itemPath = GetSelectedItemPath();
            if (itemPath == null) return;

            DialogResult result = MessageBox.Show(
                "Вы уверены, что хотите переместить этот элемент в корзину?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                MoveToRecycleBin(itemPath);
                LoadDirectory(currentDirectory);
            }
                
        }

        private void MoveToRecycleBin(string path)
        {
            try
            {
                if (!File.Exists(path) && !Directory.Exists(path))
                {
                    MessageBox.Show("Элемент не существует.");
                    return;
                }

                WinAPI.SHFILEOPSTRUCT fileOp = new WinAPI.SHFILEOPSTRUCT
                {
                    wFunc = WinAPI.FO_DELETE, // Операция удаления
                    pFrom = path + '\0' + '\0', // Путь к файлу или папке (два нуля в конце)
                    fFlags = WinAPI.FOF_ALLOWUNDO | WinAPI.FOF_NOCONFIRMATION // Флаги: перемещение в корзину и без подтверждения
                };

                int result = WinAPI.SHFileOperation(ref fileOp);

                if (result != 0)
                {
                    MessageBox.Show($"Ошибка при удалении: {result}");
                }
                else
                {
                    MessageBox.Show("Элемент перемещен в корзину.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void RestoreButton_Click(object sender, EventArgs e)
        {
            if (isRecycleBinShown)
            {
                // Режим восстановления
                string itemPath = GetSelectedItemPath();
                if (itemPath == null) return; // Если ничего не выбрано, выходим

                // Получаем выбранный элемент из ListView
                ListViewItem selectedItem = listView1.SelectedItems[0];
                //FolderItem item = selectedItem.Tag as FolderItem;
                string itemName = selectedItem.Text;

                if (itemName != null)
                {
                    // Восстанавливаем файл
                    RestoreItemFromRecycleBin(itemName);

                    // Обновляем список файлов в корзине
                    LoadRecycleBinIntoListView(listView1);

                    // Возвращаем кнопку в исходное
                    ResetButtonState();
                }
            }
            else
            {
                // Режим показа корзины
                LoadRecycleBinIntoListView(listView1);
                isRecycleBinShown = true;
            }

            // Обновляем текст кнопки
            UpdateButtonText();
        }

        private void UpdateButtonText()
        {
            if (isRecycleBinShown)
            {
                restoreButton.Text = "Восстановить";
            }
            else
            {
                restoreButton.Text = "Показать корзину";
            }
        }

        private void ResetButtonState()
        {
            isRecycleBinShown = false;
            UpdateButtonText();
        }

        private void LoadRecycleBinIntoListView(ListView listView)
        {
            try
            {
                listView.Items.Clear();

                // Создаем объект Shell
                Shell shell = new Shell();

                // Получаем корзину
                Folder recycleBin = shell.NameSpace(10); // 10 — это идентификатор корзины

                // Перебираем элементы в корзине
                foreach (FolderItem item in recycleBin.Items())
                {
                    string itemName = item.Name; // Имя элемента
                    string originalPath = recycleBin.GetDetailsOf(item, 0); // Оригинальный путь

                    // Добавляем элемент в ListView
                    ListViewItem listItem = new ListViewItem(itemName);
                    listItem.SubItems.Add(originalPath);
                    listItem.Tag = item; // Сохраняем FolderItem для восстановления
                    listView.Items.Add(listItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void RestoreItemFromRecycleBin(string itemName)
        {
            try
            {
                // Создаем объект Shell
                Shell shell = new Shell();

                // Получаем корзину (Recycle Bin)
                Folder recycleBin = shell.NameSpace(10); // 10 — это идентификатор корзины

                // Ищем файл в корзине
                foreach (FolderItem item in recycleBin.Items())
                {
                    if (item.Name == itemName)
                    {
                        string displayName = item.Name;
                        // Получаем оригинальный путь (индекс 1)
                        string originalPath = recycleBin.GetDetailsOf(item, 1); // Оригинальный путь

                        // Нормализация пути
                        originalPath = Path.Combine(originalPath, displayName);

                        // Извлекаем директорию
                        string directoryPath = Path.GetDirectoryName(originalPath);

                        // Проверяем, существует ли оригинальная папка
                        if (!Directory.Exists(directoryPath))
                        {
                            MessageBox.Show($"Оригинальная папка не существует: {directoryPath}");
                            return;
                        }

                        // Создать целевую директорию
                        string directory = Path.GetDirectoryName(originalPath);
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }

                        // Генерация уникального пути
                        string finalPath = GenerateUniquePath(originalPath);
                        MessageBox.Show($"FinalPath: {finalPath}");

                        // Подготавливаем структуру для SHFileOperation
                        WinAPI.SHFILEOPSTRUCT fileOp = new WinAPI.SHFILEOPSTRUCT
                        {
                            wFunc = WinAPI.FO_MOVE, // Операция перемещения
                            pFrom = item.Path + '\0' + '\0', // Путь к файлу в корзине
                            pTo = finalPath + '\0' + '\0', // Оригинальный путь
                            fFlags = (ushort)(WinAPI.FOF_ALLOWUNDO | WinAPI.FOF_NOCONFIRMATION) // Приводим к ushort
                        };

                        // Выполняем операцию
                        int shResult = WinAPI.SHFileOperation(ref fileOp);

                        if (shResult == 0)
                        {
                            MessageBox.Show($"Файл {itemName} успешно восстановлен в {originalPath}.");
                        }
                        else
                        {
                            MessageBox.Show($"Ошибка при восстановлении файла: {shResult}");
                        }

                        return;
                    }
                }

                MessageBox.Show($"Файл {itemName} не найден в корзине.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при восстановлении файла: {ex.Message}");
            }
        }

        private string GenerateUniquePath(string originalPath)
        {
            int counter = 1;
            string dir = Path.GetDirectoryName(originalPath);
            string fileName = Path.GetFileNameWithoutExtension(originalPath);
            string ext = Path.GetExtension(originalPath);

            while (File.Exists(originalPath))
            {
                originalPath = Path.Combine(dir, $"{fileName} ({counter++}){ext}");
            }
            return originalPath;
        }
    }
}
