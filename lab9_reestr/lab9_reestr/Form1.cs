using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab9_reestr
{
    public partial class Form1 : Form
    {

        private RegistryKey currentKey;
        private string currentPath = "";
        private readonly string logFile = "D:\\holn\\Desktop\\registry_editor.log";
        private readonly string backupFolder = "D:\\holn\\Desktop\\Backups";

        public Form1()
        {
            InitializeComponent();
            Directory.CreateDirectory(backupFolder);
            Log("Запуск");
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await InitializeTreeView();
        }

        private async Task InitializeTreeView()
        {
            treeView.BeginUpdate();
            treeView.Nodes.Clear();

            var roots = new[] {
                ("HKEY_CLASSES_ROOT", Registry.ClassesRoot),
                ("HKEY_CURRENT_USER", Registry.CurrentUser),
                ("HKEY_LOCAL_MACHINE", Registry.LocalMachine),
                ("HKEY_USERS", Registry.Users),
                ("HKEY_CURRENT_CONFIG", Registry.CurrentConfig)
            };

            await Task.Run(() =>
            {
                foreach (var (name, key) in roots)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        var node = treeView.Nodes.Add(name);
                        node.Tag = key; // Сохраняем ключ в Tag
                        node.Nodes.Add("Загрузка...");
                    });
                }
            });

            treeView.EndUpdate();
        }

        //private void LoadSubKeys(TreeNode parentNode)
        //{
        //    parentNode.Nodes.Clear();

        //    var rootKey = GetRootKey(parentNode);
        //    var path = GetRegistryPath(parentNode);

        //    try
        //    {
        //        using (var key = string.IsNullOrEmpty(path) ? rootKey : rootKey.OpenSubKey(path))
        //        {
        //            if (key != null)
        //            {
        //                foreach (var subKey in key.GetSubKeyNames().Take(100)) // Ограничение для производительности
        //                {
        //                    var childNode = parentNode.Nodes.Add(subKey);
        //                    childNode.Nodes.Add("Loading..."); // Добавляем заглушку для дочерних элементов
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        parentNode.Nodes.Add($"(Ошибка: {ex.Message})");
        //    }
        //}

        private async void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count == 1 &&
                (e.Node.Nodes[0].Text == "Загрузка..." || e.Node.Nodes[0].Text == "Loading..."))
            {
                e.Node.Nodes[0].Text = "Загрузка...";
                e.Node.Collapse();

                try
                {
                    var subKeys = await GetSubKeysAsync(e.Node);

                    this.Invoke((MethodInvoker)delegate
                    {
                        e.Node.Nodes.Clear();
                        foreach (var subKey in subKeys)
                        {
                            var childNode = e.Node.Nodes.Add(subKey);
                            childNode.Nodes.Add("Загрузка...");
                        }
                        e.Node.Expand();
                    });
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        e.Node.Nodes.Clear();
                        e.Node.Nodes.Add($"(Ошибка: {ex.Message})");
                        e.Node.Expand();
                    });
                }
            }
        }

        private async Task<string[]> GetSubKeysAsync(TreeNode node)
        {
            try
            {
                var rootKey = node.Tag as RegistryKey ?? GetRootKey(node);
                var path = GetRegistryPath(node);

                return await Task.Run(() =>
                {
                    using (var key = string.IsNullOrEmpty(path) ? rootKey : rootKey.OpenSubKey(path))
                    {
                        return key?.GetSubKeyNames() ?? Array.Empty<string>();
                    }
                });
            }
            catch
            {
                return new[] { "(Ошибка доступа)" };
            }
        }

        private void LoadRegistryRecursive(TreeNode parentNode, int maxDepth = 3, int currentDepth = 0)
        {
            if (currentDepth >= maxDepth)
            {
                parentNode.Nodes.Add("Загрузка...");
                return;
            }

            RegistryKey parentKey = GetRegistryKey(parentNode);
            if (parentKey == null) return;

            try
            {
                foreach (string subKeyName in parentKey.GetSubKeyNames())
                {
                    TreeNode childNode = parentNode.Nodes.Add(subKeyName);

                    using (RegistryKey childKey = parentKey.OpenSubKey(subKeyName))
                    {
                        if (childKey != null)
                        {
                            LoadRegistryRecursive(childNode, maxDepth, currentDepth + 1);
                        }
                    }
                }
            }
            catch
            {
                parentNode.Nodes.Add("(нет доступа)");
            }
        }

        private RegistryKey GetRegistryKey(TreeNode node)
        {
            RegistryKey rootKey = GetRootKey(node);
            if (rootKey == null) return null;

            string path = GetRegistryPath(node);

            if (string.IsNullOrEmpty(path))
                return rootKey;

            try
            {
                return rootKey.OpenSubKey(path);
            }
            catch
            {
                return null;
            }
        }

        private string GetRegistryPath(TreeNode node)
        {
            if (node.Parent == null) return "";

            var pathParts = new List<string>();
            var current = node;

            while (current.Parent != null)
            {
                pathParts.Insert(0, current.Text);
                current = current.Parent;
            }

            // Для корневых узлов второго уровня (HKEY_CURRENT_USER\Software (как примерчик))
            if (pathParts.Count == 1 && current.Parent == null)
                return pathParts[0];

            return string.Join("\\", pathParts);
        }

        private void Log(string message)
        {
            File.AppendAllText(logFile, $"[{DateTime.Now}] {message}\n", Encoding.UTF8);
        }

        private RegistryKey GetRootKey(TreeNode node)
        {
            while (node.Parent != null)
                node = node.Parent;

            switch (node.Text)
            {
                case "HKEY_CLASSES_ROOT": return Registry.ClassesRoot;
                case "HKEY_CURRENT_USER": return Registry.CurrentUser;
                case "HKEY_LOCAL_MACHINE": return Registry.LocalMachine;
                case "HKEY_USERS": return Registry.Users;
                case "HKEY_CURRENT_CONFIG": return Registry.CurrentConfig;
                default: return null;
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            currentKey = GetRootKey(e.Node);
            currentPath = GetFullPath(e.Node);
            LoadValues();
            //Log($"Selected: {GetRootName(currentKey)}\\{currentPath}");
        }

        private async void LoadValues()
        {
            if (currentKey == null) return;

            listView.BeginUpdate();
            listView.Items.Clear();

            try
            {
                await Task.Run(() =>
                {
                    using (var key = string.IsNullOrEmpty(currentPath) ? currentKey : currentKey.OpenSubKey(currentPath))
                    {
                        if (key != null)
                        {
                            var values = key.GetValueNames();
                            this.Invoke((MethodInvoker)delegate
                            {
                                foreach (string valueName in values)
                                {
                                    try
                                    {
                                        object value = key.GetValue(valueName);
                                        RegistryValueKind kind = key.GetValueKind(valueName);

                                        var item = new ListViewItem(valueName ?? "(Default)");
                                        item.SubItems.Add(kind.ToString());
                                        item.SubItems.Add(value?.ToString() ?? "");
                                        listView.Items.Add(item);
                                    }
                                    catch { /* Игнорируем ошибки отдельных значений */ }
                                }
                            });
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Log($"Ошибка загрузки значения: {ex.Message}");
                MessageBox.Show($"Ошибка загрузки значения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                listView.EndUpdate();
            }
        }

        private string GetFullPath(TreeNode node)
        {
            if (node.Parent == null) return "";

            string path = node.Text;
            node = node.Parent;

            while (node.Parent != null)
            {
                path = node.Text + "\\" + path;
                node = node.Parent;
            }

            return path;
        }

        private string GetRootName(RegistryKey key)
        {
            if (key == Registry.ClassesRoot) return "HKEY_CLASSES_ROOT";
            if (key == Registry.CurrentUser) return "HKEY_CURRENT_USER";
            if (key == Registry.LocalMachine) return "HKEY_LOCAL_MACHINE";
            if (key == Registry.Users) return "HKEY_USERS";
            if (key == Registry.CurrentConfig) return "HKEY_CURRENT_CONFIG";
            return "UNKNOWN";
        }

        //private void btnCreateKey_Click(object sender, EventArgs e)
        //{
        //    string name = Microsoft.VisualBasic.Interaction.InputBox("Enter key name:", "Create Key");
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        try
        //        {
        //            using (RegistryKey key = currentKey.CreateSubKey($"{currentPath}\\{name}"))
        //            {
        //                treeView.SelectedNode.Nodes.Add(name);
        //                treeView.SelectedNode.Expand();
        //                Log($"Key created: {GetRootName(currentKey)}\\{currentPath}\\{name}");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Log($"Error creating key: {ex.Message}");
        //            MessageBox.Show($"Error creating key: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        //private void btnDeleteKey_Click(object sender, EventArgs e)
        //{
        //    if (treeView.SelectedNode != null && treeView.SelectedNode.Parent != null)
        //    {
        //        if (MessageBox.Show("Delete this key?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        //        {
        //            try
        //            {
        //                currentKey.DeleteSubKeyTree(currentPath);
        //                treeView.SelectedNode.Remove();
        //                Log($"Key deleted: {GetRootName(currentKey)}\\{currentPath}");
        //            }
        //            catch (Exception ex)
        //            {
        //                Log($"Error deleting key: {ex.Message}");
        //                MessageBox.Show($"Error deleting key: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //    }
        //}

        private void btnCreateValue_Click(object sender, EventArgs e)
        {
            using (var form = new EditValueForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (RegistryKey key = currentKey.CreateSubKey(currentPath))
                        {
                            key.SetValue(form.ValueName, form.Value, form.ValueKind);
                            Log($"Значение создано: {form.ValueName}={form.Value} ({form.ValueKind})");
                            LoadValues();
                        }
                    }
                    catch (Exception ex)
                    {
                        Log($"Ошибка при создании значения: {ex.Message}");
                        MessageBox.Show($"Ошибка при создании значения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnEditValue_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите значение для изменения", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ListViewItem selectedItem = listView.SelectedItems[0];
            string valueName = selectedItem.Text == "(Default)" ? "" : selectedItem.Text;

            try
            {
                using (RegistryKey key = currentKey.OpenSubKey(currentPath, true))
                {
                    if (key != null)
                    {
                        object currentValue = key.GetValue(valueName);
                        RegistryValueKind valueKind = key.GetValueKind(valueName);

                        using (var editForm = new EditValueForm(valueName, valueKind, currentValue))
                        {
                            if (editForm.ShowDialog() == DialogResult.OK)
                            {
                                key.SetValue(editForm.ValueName, editForm.Value, editForm.ValueKind);
                                Log($"Значение изменено: {editForm.ValueName}");
                                LoadValues();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Ошибка изменения значения: {ex.Message}");
                MessageBox.Show($"Ошибка при изменении значения: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteValue_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                string valueName = listView.SelectedItems[0].Text;
                if (MessageBox.Show($"Удалить значение '{valueName}'?", "Подтвердить", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        using (RegistryKey key = currentKey.OpenSubKey(currentPath, true))
                        {
                            key.DeleteValue(valueName);
                            Log($"Значение удалено: {valueName}");
                            LoadValues();
                        }
                    }
                    catch (Exception ex)
                    {
                        Log($"Ошибка удаления значения: {ex.Message}");
                        MessageBox.Show($"Ошибка удаления значения: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (currentKey == null) return;

            string backupName = $"{GetRootName(currentKey).Replace("HKEY_", "")}_{currentPath.Replace("\\", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.regbak";
            string backupPath = Path.Combine(backupFolder, backupName);

            try
            {
                StringBuilder backupData = new StringBuilder();
                BackupRegistryRecursive(currentKey, currentPath, backupData);

                File.WriteAllText(backupPath, backupData.ToString(), Encoding.UTF8);
                Log($"Бэкапчик создан: {backupName}");
                MessageBox.Show("Бэкапчик создан успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Log($"Ошибка создания бэкапа: {ex.Message}");
                MessageBox.Show($"Ошибка создания бэкапа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BackupRegistryRecursive(RegistryKey baseKey, string subPath, StringBuilder sb)
        {
            using (RegistryKey key = baseKey.OpenSubKey(subPath))
            {
                if (key == null) return;

                string fullPath = $"{GetRootName(baseKey)}\\{subPath}";
                foreach (string valueName in key.GetValueNames())
                {
                    object value = key.GetValue(valueName);
                    RegistryValueKind kind = key.GetValueKind(valueName);
                    sb.AppendLine($"{fullPath}|{valueName}={kind}={value}");
                }

                foreach (string subKeyName in key.GetSubKeyNames())
                {
                    BackupRegistryRecursive(baseKey, Path.Combine(subPath, subKeyName), sb);
                }
            }
        }


        private void btnRestore_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = backupFolder;
                dialog.Filter = "Backup files (*.regbak)|*.regbak|All files (*.*)|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (MessageBox.Show("Восстановить из бэкапа? Значения будут переписаны.", "Подтвердить",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        try
                        {
                            string[] lines = File.ReadAllLines(dialog.FileName);

                            foreach (string line in lines)
                            {
                                string[] splitPathAndValue = line.Split(new[] { '|' }, 2);
                                if (splitPathAndValue.Length != 2) continue;

                                string keyPath = splitPathAndValue[0];
                                string[] valueParts = splitPathAndValue[1].Split(new[] { '=' }, 3);
                                if (valueParts.Length != 3) continue;

                                string valueName = valueParts[0];
                                RegistryValueKind kind = (RegistryValueKind)Enum.Parse(typeof(RegistryValueKind), valueParts[1]);
                                object value = valueParts[2];

                                RegistryKey root = GetRootKeyFromPath(keyPath, out string relativePath);
                                using (RegistryKey key = root.CreateSubKey(relativePath))
                                {
                                    if (kind == RegistryValueKind.DWord && int.TryParse(value.ToString(), out int intVal))
                                        key.SetValue(valueName, intVal, kind);
                                    else
                                        key.SetValue(valueName, value, kind);
                                }
                            }

                            Log($"Восстановлено из: {Path.GetFileName(dialog.FileName)}");
                            MessageBox.Show("Восстановление завершено!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadValues();

                            if (treeView.SelectedNode != null)
                            {
                                treeView.SelectedNode.Nodes.Clear();
                                treeView.SelectedNode.Nodes.Add("Загрузка...");
                                treeView.SelectedNode.Expand();
                            }
                        }
                        catch (Exception ex)
                        {
                            Log($"Ошибка восстановления: {ex.Message}");
                            MessageBox.Show($"Ошибка восстановления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private RegistryKey GetRootKeyFromPath(string fullPath, out string subPath)
        {
            subPath = "";

            if (fullPath.StartsWith("HKEY_LOCAL_MACHINE\\"))
            {
                subPath = fullPath.Substring("HKEY_LOCAL_MACHINE\\".Length);
                return Registry.LocalMachine;
            }
            else if (fullPath.StartsWith("HKEY_CURRENT_USER\\"))
            {
                subPath = fullPath.Substring("HKEY_CURRENT_USER\\".Length);
                return Registry.CurrentUser;
            }
            else if (fullPath.StartsWith("HKEY_CLASSES_ROOT\\"))
            {
                subPath = fullPath.Substring("HKEY_CLASSES_ROOT\\".Length);
                return Registry.ClassesRoot;
            }
            else if (fullPath.StartsWith("HKEY_USERS\\"))
            {
                subPath = fullPath.Substring("HKEY_USERS\\".Length);
                return Registry.Users;
            }
            else if (fullPath.StartsWith("HKEY_CURRENT_CONFIG\\"))
            {
                subPath = fullPath.Substring("HKEY_CURRENT_CONFIG\\".Length);
                return Registry.CurrentConfig;
            }

            throw new InvalidOperationException("Invalid registry path.");
        }



        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                var selectedNode = treeView.SelectedNode;
                var parentNode = selectedNode.Parent;

                // Получаем путь до пэрент
                string parentPath = parentNode != null ? GetFullPath(parentNode) : "";

                selectedNode.Remove();
                var newNode = parentNode?.Nodes.Add(selectedNode.Text) ?? treeView.Nodes.Add(selectedNode.Text);
                newNode.Tag = selectedNode.Tag;
                newNode.Nodes.Add("Загрузка...");

                LoadValues();
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCreateSubKey_Click(object sender, EventArgs e)
        {

            if (treeView.SelectedNode == null)
            {
                MessageBox.Show("Выберите раздел реестра, в котором нужно создать подраздел", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string subKeyName = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите имя нового подраздела:", "Создание подраздела");

            if (string.IsNullOrWhiteSpace(subKeyName))
            {
                return;
            }

            try
            {
                RegistryKey rootKey = GetRegistryKey(treeView.SelectedNode);
                string fullPath = GetFullPath(treeView.SelectedNode);

                using (RegistryKey parentKey = string.IsNullOrEmpty(fullPath)
                    ? rootKey
                    : rootKey.OpenSubKey(fullPath, true))
                {
                    if (parentKey == null)
                    {
                        MessageBox.Show("Не удалось открыть раздел для записи", "Ошибка");
                        return;
                    }

                    using (RegistryKey newKey = parentKey.CreateSubKey(subKeyName))
                    {
                        TreeNode newNode = treeView.SelectedNode.Nodes.Add(subKeyName);
                        newNode.Nodes.Add("Загрузка...");
                        treeView.SelectedNode.Expand();

                        Log($"Создан подраздел: {GetRootName(rootKey)}\\{fullPath}\\{subKeyName}");
                        MessageBox.Show("Подраздел успешно создан!", "Успех");
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Нет прав для создания подраздела в этом разделе реестра", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log($"Ошибка доступа при создании подраздела: {subKeyName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании подраздела: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log($"Ошибка создания подраздела: {ex.Message}");
            }
        }

        private void btnDeleteSubKey_Click(object sender, EventArgs e)
        {

            if (treeView.SelectedNode == null || treeView.SelectedNode.Parent == null)
            {
                MessageBox.Show("Выберите подраздел для удаления (нельзя удалять корневые разделы)", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string subKeyName = treeView.SelectedNode.Text;
            string fullPath = GetFullPath(treeView.SelectedNode.Parent);

            DialogResult result = MessageBox.Show(
                $"Вы точно хотите удалить подраздел '{subKeyName}'?\nЭто действие нельзя отменить!",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes)
                return;

            try
            {
                RegistryKey parentKey = GetRegistryKey(treeView.SelectedNode.Parent);

                parentKey.DeleteSubKeyTree(subKeyName);

                treeView.SelectedNode.Remove();

                Log($"Удален подраздел: {GetRootName(currentKey)}\\{fullPath}\\{subKeyName}");
                MessageBox.Show("Подраздел успешно удален!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Нет прав для удаления этого подраздела", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log($"Ошибка доступа при удалении подраздела: {subKeyName}");
            }
            catch (ArgumentException ex) when (ex.Message.Contains("существует"))
            {
                MessageBox.Show("Удаляемый подраздел не существует", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log($"Попытка удаления несуществующего подраздела: {subKeyName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении подраздела: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log($"Ошибка удаления подраздела: {ex.Message}");
            }
        }
    }
}