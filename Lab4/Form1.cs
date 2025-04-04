using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1: Form
    {
        private Dictionary<string, ThreadManager> threadManagers = new Dictionary<string, ThreadManager>();

        public Form1()
        {
            InitializeComponent();
            InitializeThreadManagers();
        }

        private void InitializeThreadManagers()
        {
            threadManagers.Add("Дочерний поток 1", new ThreadManager("Дочерний поток 1", LogMessage, ThreadPriority.Lowest));
            threadManagers.Add("Дочерний поток 2", new ThreadManager("Дочерний поток 2", LogMessage, ThreadPriority.AboveNormal));
            threadManagers.Add("Дочерний поток 3", new ThreadManager("Дочерний поток 3", LogMessage, ThreadPriority.Normal));

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 2;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string threadName = button.Tag.ToString();

            if (threadManagers.TryGetValue(threadName, out var manager))
            {
                manager.Start();
            }
        }

        private void pauseBtn_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string threadName = button.Tag.ToString();

            if (threadManagers.TryGetValue(threadName, out var manager))
            {
                manager.Pause();
            }
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string threadName = button.Tag.ToString();

            if (threadManagers.TryGetValue(threadName, out var manager))
            {
                manager.Stop();
            }
        }

        private void LogMessage(string message)
        {
            if (logTextBox.InvokeRequired)
            {
                logTextBox.Invoke((Action)(() => logTextBox.AppendText(message + Environment.NewLine)));
            }
            else
            {
                logTextBox.AppendText(message + Environment.NewLine);
            }
        }

        private void resumeBtn_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string threadName = button.Tag.ToString();

            if (threadManagers.TryGetValue(threadName, out var manager))
            {
                manager.Resume();
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            logTextBox.Text = "";
        }

        private void startAllBtn_Click(object sender, EventArgs e)
        {

            try
            {
                int startedCount = 0;

                foreach (var manager in threadManagers.Values)
                {
                    if (!manager.thread?.IsAlive ?? true)
                    {
                        manager.Start();
                        startedCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Ошибка при массовом запуске: {ex.Message}");
            }
        }

        private void stopAllBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int startedCount = 0;

                foreach (var manager in threadManagers.Values)
                {
                    if (manager.thread?.IsAlive ?? true)
                    {
                        manager.Stop();
                        startedCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Ошибка при массовой остановке: {ex.Message}");
            }
        }

        private void priority_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBox = (ComboBox)sender;
                string threadName = comboBox.Tag.ToString();
                string priority = comboBox.SelectedItem.ToString();

                Dictionary<string, ThreadPriority> hmap = new Dictionary<string, ThreadPriority>
                {
                    { "Низкий", ThreadPriority.Lowest },
                    { "Средний", ThreadPriority.Normal },
                    { "Ниже среднего", ThreadPriority.BelowNormal},
                    { "Выше среднего", ThreadPriority.AboveNormal }
                };

                if (threadManagers.TryGetValue(threadName, out var manager))
                {
                    manager.SetPriority(hmap[priority]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
    public class ThreadManager
    {
        public Thread thread { get; private set; }
        private CancellationTokenSource cts;
        private ManualResetEventSlim pauseEvent = new ManualResetEventSlim(true);
        
        public string Name { get; }
        private Action<string> logAction;
        public ThreadPriority priority;

        public ThreadManager(string name, Action<string> logAction, ThreadPriority priority)
        {
            Name = name;
            this.priority = priority;
            this.logAction = logAction;
        }

        public void Start()
        {
            cts = new CancellationTokenSource();
            thread = new Thread(ChildMethod)
            {
                IsBackground = true,
                Name = Name,
                Priority = this.priority
            };
            thread.Start(cts.Token);

            logAction?.Invoke($"{Name}: запущен");
        }

        public void Pause()
        {
            if (thread != null && thread.IsAlive)
            {
                pauseEvent.Reset();
                logAction?.Invoke($"{Name}: приостановлен");
            } else
            {
                MessageBox.Show($"{Name} не запущен", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void Resume()
        {
            if (thread != null && thread.IsAlive)
            {
                pauseEvent.Set();
                logAction?.Invoke($"{Name}: возобновлен");
            } else
            {
                MessageBox.Show($"{Name} не запущен", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void Stop()
        {
            if (thread != null && thread.IsAlive)
            {
                cts?.Cancel();
                pauseEvent.Set(); // Разблокируем для завершения
                thread?.Join(500); // Ожидаем завершение
                logAction?.Invoke($"{Name}: остановлен");
            } else
            {
                MessageBox.Show($"{Name} не запущен", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void SetPriority(ThreadPriority new_priority)
        {
            if (thread != null && thread.IsAlive)
            {
                this.priority = new_priority;
                thread.Priority = this.priority;
                logAction?.Invoke($"{Name}: приоритет изменен на {this.priority}");
            }
        }

        private void ChildMethod(object obj)
        {
            var token = (CancellationToken)obj;
            int iteration = 0;

            while (!token.IsCancellationRequested)
            {
                pauseEvent.Wait(token); // Ожидание при паузе

                try
                {
                    iteration++;
                    logAction?.Invoke($"{Name}: итерация {iteration}");
                    Thread.Sleep(1000); // Имитация работы
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}
