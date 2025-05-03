using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Lab10
{
    public partial class Form1 : Form
    {
        private string logPath = "registry_log.txt";
        private string backupPath = "registry_backup.reg";

        public Form1()
        {
            InitializeComponent();
            LoadRootRegistryKeys();
        }

        private void LoadRootRegistryKeys()
        {
            treeRegistry.Nodes.Clear();

            AddRootKey("HKEY_CLASSES_ROOT", Registry.ClassesRoot);
            AddRootKey("HKEY_CURRENT_USER", Registry.CurrentUser);
            AddRootKey("HKEY_LOCAL_MACHINE", Registry.LocalMachine);
            AddRootKey("HKEY_USERS", Registry.Users);
            AddRootKey("HKEY_CURRENT_CONFIG", Registry.CurrentConfig);
        }

        private void AddRootKey(string name, RegistryKey key)
        {
            TreeNode node = new TreeNode(name) { Tag = key };
            node.Nodes.Add("Loading..."); // Заглушка для ленивой загрузки
            treeRegistry.Nodes.Add(node);
        }

        private void treeRegistry_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Nodes.Count == 1 && node.Nodes[0].Text == "Loading...")
            {
                node.Nodes.Clear();
                RegistryKey parentKey = node.Tag as RegistryKey;
                if (parentKey == null) return;

                try
                {
                    foreach (string subKeyName in parentKey.GetSubKeyNames())
                    {
                        try
                        {
                            RegistryKey subKey = parentKey.OpenSubKey(subKeyName);
                            TreeNode subNode = new TreeNode(subKeyName) { Tag = subKey };
                            subNode.Nodes.Add("Loading...");
                            node.Nodes.Add(subNode);
                        }
                        catch { }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка доступа: " + ex.Message);
                }
            }
        }

        private void treeRegistry_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lstValues.Items.Clear();
            RegistryKey selectedKey = e.Node.Tag as RegistryKey;

            if (selectedKey == null) return;

            // Построим путь к выбранному узлу
            txtPath.Text = GetRegistryPath(e.Node);

            try
            {
                foreach (string valueName in selectedKey.GetValueNames())
                {
                    object value = selectedKey.GetValue(valueName);
                    lstValues.Items.Add($"{valueName} = {value}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка доступа: " + ex.Message);
            }
        }

        private string GetRegistryPath(TreeNode node)
        {
            string path = node.Text;
            TreeNode current = node;
            while (current.Parent != null)
            {
                current = current.Parent;
                path = current.Text + "\\" + path;
            }
            return path;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(txtPath.Text.Replace("HKEY_CURRENT_USER\\", ""));
                if (key != null)
                {
                    object value = key.GetValue(txtName.Text);
                    txtValue.Text = value?.ToString() ?? "Значение не найдено";
                    MessageBox.Show($"Прочитано значение: {txtName.Text} = {txtValue.Text}");
                    Log($"Прочитано значение: {txtName.Text} = {txtValue.Text}");
                }
                else
                {
                    MessageBox.Show("Ключ не найден");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(txtPath.Text.Replace("HKEY_CURRENT_USER\\", ""));
                key.SetValue(txtName.Text, txtValue.Text);
                MessageBox.Show($"Изменено значение: {txtName.Text} = {txtValue.Text}");
                Log($"Изменено значение: {txtName.Text} = {txtValue.Text}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                var path = txtPath.Text;
                var psi = new System.Diagnostics.ProcessStartInfo("reg", $"export \"{path}\" \"{backupPath}\" /y")
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };
                System.Diagnostics.Process.Start(psi).WaitForExit();
                MessageBox.Show("Бэкап создан");
                Log("Бэкап создан");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                var psi = new System.Diagnostics.ProcessStartInfo("reg", $"import \"{backupPath}\"")
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };
                System.Diagnostics.Process.Start(psi).WaitForExit();
                Log("Реестр восстановлен из бэкапа");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnTestChange_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\TestKey");
                key.SetValue("MyValue", "Hello");
                Log("Создан тестовый ключ: Software\\TestKey\\MyValue = Hello");
                MessageBox.Show("Тестовый ключ Software\\TestKey\\MyValue = Hello успешно создан!");
 
                LoadRootRegistryKeys();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Log(string message)
        {
            File.AppendAllText(logPath, $"{DateTime.Now}: {message}{Environment.NewLine}");
        }
    }
}
