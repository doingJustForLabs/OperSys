using System;
using System.Data;
using System.Windows.Forms;
using static Lab6.WindowsUtils;

namespace Lab6
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void showBtn_Click(object sender, EventArgs e)
        {
            try
            {
                updateBtn.Enabled = true;
                resizeBtn.Enabled = true;

                DataTable data = new DataTable();

                data.Columns.Add("Окно");
                data.Columns.Add("Класс");
                data.Columns.Add("exe");

                EnumWindows((hWnd, lParam) =>
                {
                    if (!IsWindowVisible(hWnd))
                        return true;

                    string title = GetWindowTitle(hWnd);
                    if (string.IsNullOrWhiteSpace(title))
                        return true;

                    string className = GetClass(hWnd);

                    GetWindowThreadProcessId(hWnd, out uint processId);
                    string exeName = GetProcessExePath(processId);

                    if (GetWindowRect(hWnd, out RECT rect))
                    {
                        int width = rect.Right - rect.Left;
                        int height = rect.Bottom - rect.Top;
                    }

                    data.Rows.Add(title, className, exeName);
                    return true;

                }, IntPtr.Zero);

                dataGridView.DataSource = data;
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.CurrentCell != null && dataGridView.CurrentCell.ColumnIndex == 0)
            {
                IntPtr hWnd = FindWindow(null, dataGridView.CurrentCell.Value?.ToString());

                chosenTextBox.Text = dataGridView.CurrentCell.Value?.ToString();
                if (GetWindowRect(hWnd, out RECT rect))
                {
                    int width = rect.Right - rect.Left;
                    int height = rect.Bottom - rect.Top;
                    sizeTextBox.Text = $"{width}x{height}";
                    xPosTextBox.Text = $"{rect.Left}";
                    yPosTextBox.Text = $"{rect.Top}";
                }

            } else
            {
                MessageBox.Show("Выберите ячейку из столбца 'Окно'", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private bool TryParseSize(string sizeText, out int width, out int height)
        {
            width = 0;
            height = 0;

            if (string.IsNullOrWhiteSpace(sizeText))
                return false;

            string[] parts = sizeText.Split('x', '×', ' ');
            if (parts.Length != 2)
                return false;

            return int.TryParse(parts[0], out width) && int.TryParse(parts[1], out height);
        }
        private void resizeBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string windowTitle = dataGridView.CurrentCell.Value?.ToString();
                IntPtr hWnd = FindWindow(null, windowTitle);

                SetWindowTitle(hWnd, chosenTextBox.Text);
                dataGridView.CurrentCell.Value = chosenTextBox.Text;


                if (!TryParseSize(sizeTextBox.Text, out int width, out int height))
                {
                    MessageBox.Show("Некорректный формат размера!\nПример: 800x600", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(xPosTextBox.Text, out int x)) {
                    return;
                }
                if (!int.TryParse(yPosTextBox.Text, out int y))
                {
                    return;
                }

                if (GetWindowRect(hWnd, out RECT rect))
                {
                    bool success = MoveWindow(
                        hWnd,
                        X: x,
                        Y: y,
                        nWidth: width,
                        nHeight: height,
                        bRepaint: true
                    );
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                IntPtr hWnd = FindWindow(null, dataGridView.CurrentCell.Value?.ToString());

                chosenTextBox.Text = dataGridView.CurrentCell.Value?.ToString();
                if (GetWindowRect(hWnd, out RECT rect))
                {
                    int width = rect.Right - rect.Left;
                    int height = rect.Bottom - rect.Top;
                    sizeTextBox.Text = $"{width}x{height}";
                    xPosTextBox.Text = $"{rect.Left}";
                    yPosTextBox.Text = $"{rect.Top}";
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
