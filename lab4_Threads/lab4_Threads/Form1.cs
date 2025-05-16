using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace lab4_Threads
{
    public partial class Form1: Form
    {
        private readonly List<WorkerThread> threads = new List<WorkerThread>();
        private readonly ConcurrentQueue<ThreadMessage> messageQueue = new ConcurrentQueue<ThreadMessage>();
        private int threadCounter = 0;
        private readonly object syncLock = new object();
        //private System.Windows.Forms.Timer priorityChangeTimer;
        //private ManualResetEvent pauseEvent = new ManualResetEvent(true);

        //private Thread workingThread;
        //private volatile bool isPaused = false;
        //private volatile bool isRunning = false;
        //private ThreadPriority currentPrior = ThreadPriority.Normal;

        public Form1()
        {
            InitializeComponent();
            priorityChangeTimer.Interval = 3000;
            priorityChangeTimer.Start();
        }

        private void ChangePriorityDyn(object sender, EventArgs e)
        {
            lock (syncLock)
            {
                if (threads.Count == 0) return;

                foreach (var thread in threads)
                {
                    if (!thread.IsPaused)
                    {
                        thread.ChangePriority();
                    }
                }

                UpdateThreadList();

                //AddLogEntry("SYSTEM", Color.Purple, "Приоритеты всех потоков изменены циклически");
            }
        }

        private void AddThread(object sender, EventArgs e)
        {
            lock (syncLock)
            {
                var thread = new WorkerThread(++threadCounter, messageQueue, AddLogEntry);
                threads.Add(thread);
                thread.Start();
                UpdateThreadList();
                pauseButton.Enabled = stopButton.Enabled = true;
                AddLogEntry("SYSTEM", Color.Gray, $"Поток {threadCounter} запущен");
            }
        }

        private void PauseAllThreads(object sender, EventArgs e)
        {
            lock (syncLock)
            {
                if (pauseButton.Text == "Пауза всех")
                {
                    foreach (var t in threads) t.Pause();
                    pauseButton.Text = "Возобновить все";
                    AddLogEntry("SYSTEM", Color.Orange, "Все потоки приостановлены");
                }
                else
                {
                    foreach (var t in threads) t.Resume();
                    pauseButton.Text = "Пауза всех";
                    AddLogEntry("SYSTEM", Color.Green, "Все потоки возобновлены");
                }
            }
        }

        private void StopAllThreads(object sender, EventArgs e)
        {
            lock (syncLock)
            {
                foreach (var t in threads) t.Stop();
                threads.Clear();
                pauseButton.Enabled = stopButton.Enabled = false;
                pauseButton.Text = "Пауза всех";
                AddLogEntry("SYSTEM", Color.Red, "Все потоки остановлены");
            }
        }

        private void ChangePriorityForSelected(object sender, EventArgs e)
        {
            if (lstThreads.SelectedIndex >= 0)
            {
                lock (syncLock)
                {
                    threads[lstThreads.SelectedIndex].ChangePriority();
                    UpdateThreadList();
                }
            }
        }

        private void UpdateThreadList()
        {
            lstThreads.Items.Clear();
            foreach (var thread in threads)
            {
                lstThreads.Items.Add($"Поток {thread.Id} ({thread.CurrentPriority})");
            }
        }

        private void AddLogEntry(string threadType, Color color, string message)
        {
            if (logView.InvokeRequired)
            {
                logView.Invoke(new Action(() => AddLogEntry(threadType, color, message)));
                return;
            }

            string threadId = threadType.StartsWith("THREAD-") ? threadType.Split('-')[1] : "SYSTEM";

            var item = new ListViewItem(DateTime.Now.ToString("HH:mm:ss.fff"));
            item.UseItemStyleForSubItems = false;
            item.SubItems.Add(threadType);
            item.SubItems.Add(threadId);
            item.SubItems.Add(message).ForeColor = color;

            logView.Items.Add(item);
            logView.EnsureVisible(logView.Items.Count - 1);

            if (logView.Items.Count > 10)
            {
                logView.Items.RemoveAt(0);
            }
        }

        //private class WorkerThread
        //{
        //    public int Id { get; }
        //    public ThreadPriority CurrentPriority { get; private set; }
        //    private Thread _thread;
        //    private bool _isRunning;
        //    private ManualResetEvent _pauseEvent = new ManualResetEvent(true);
        //    private ConcurrentQueue<ThreadMessage> _queue;
        //    private Action<string, Color, string> _logAction;
        //    private Random _random = new Random();

        //    public bool IsPaused { get; private set; }

        //    public WorkerThread(int id, ConcurrentQueue<ThreadMessage> queue, Action<string, Color, string> logAction)
        //    {
        //        Id = id;
        //        _queue = queue;
        //        _logAction = logAction;
        //        CurrentPriority = ThreadPriority.Normal;
        //        _thread = new Thread(Work) { IsBackground = true, Priority = CurrentPriority };
        //        _isRunning = true;
        //    }

        //    public void Start() => _thread.Start();

        //    public void Pause()
        //    {
        //        _pauseEvent.Reset();
        //        IsPaused = true;
        //        _logAction?.Invoke($"THREAD-{Id}", Color.Orange, $"Поток {Id} приостановлен");
        //    }

        //    public void Resume()
        //    {
        //        _pauseEvent.Set();
        //        IsPaused = false;
        //        _logAction?.Invoke($"THREAD-{Id}", Color.Green, $"Поток {Id} возобновлен");
        //    }

        //    public void Stop()
        //    {
        //        _isRunning = false;
        //        Resume(); // Разблокируем перед остановкой
        //        //_pauseEvent.Set();
        //        if (!_thread.Join(300))
        //        {
        //            _thread.Interrupt(); // Мягкое прерывание
        //            if (!_thread.Join(100))
        //                _thread.Abort();
        //        }
        //        _logAction?.Invoke($"THREAD-{Id}", Color.Red, $"Поток {Id} остановлен");
        //    }

        //    public void ChangePriority()
        //    {
        //        switch (CurrentPriority)
        //        {
        //            case ThreadPriority.Lowest:
        //                CurrentPriority = ThreadPriority.BelowNormal;
        //                break;
        //            case ThreadPriority.BelowNormal:
        //                CurrentPriority = ThreadPriority.Normal;
        //                break;
        //            case ThreadPriority.Normal:
        //                CurrentPriority = ThreadPriority.AboveNormal;
        //                break;
        //            case ThreadPriority.AboveNormal:
        //                CurrentPriority = ThreadPriority.Highest;
        //                break;
        //            default:
        //                CurrentPriority = ThreadPriority.Lowest;
        //                break;
        //        }
        //        _thread.Priority = CurrentPriority;
        //        _logAction?.Invoke($"THREAD-{Id}", Color.Green, $"Приоритет изменен на {CurrentPriority}");
        //    }

        //    private void Work()
        //    {
        //        _logAction?.Invoke($"THREAD-{Id}", Color.Blue, $"Поток {Id} начал работу");
        //        while (_isRunning)
        //        {
        //            _pauseEvent.WaitOne(); // Пауза

        //            if (!_isRunning) break;

        //            // Отправка сообщения
        //            while (_queue.TryDequeue(out var receivedMsg))
        //            {
        //                if (receivedMsg.SenderId != Id)
        //                {
        //                    _logAction?.Invoke($"THREAD-{Id}", Color.DarkCyan,
        //                        $"Получено от {receivedMsg.SenderId}: {receivedMsg.Content}");
        //                    Thread.Sleep(1000); // Искусственная задержка 
        //                }
        //            }

        //            if (_random.Next(0, 100) < 30)
        //            {
        //                var msg = new ThreadMessage(Id, _random.Next(1, 101));
        //                _queue.Enqueue(msg);
        //                _logAction?.Invoke($"THREAD-{Id}", Color.DarkBlue, $"Отправлено: {msg.Content}");
        //            }

        //            Thread.Sleep(300);
        //        }
        //    }
        //}

        //private class ThreadMessage
        //{
        //    public int SenderId { get; }
        //    public int Content { get; }

        //    public ThreadMessage(int senderId, int content)
        //    {
        //        SenderId = senderId;
        //        Content = content;
        //    }
        //}

        //private void AddThread_Click(object sender, EventArgs e)
        //{
        //    if (!isRunning)
        //    {
        //        StartThread();
        //        startButton.Enabled = false;
        //        pauseButton.Enabled = true;
        //        stopButton.Enabled = true;
        //    }
        //}

        //private void PauseButton_Click(object sender, EventArgs e)
        //{
        //    isPaused = !isPaused;

        //    if (isPaused)
        //    {
        //        pauseEvent.Reset(); // Ставим на паузу
        //        pauseButton.Text = "Возобновить";
        //    }
        //    else
        //    {
        //        pauseEvent.Set();
        //        pauseButton.Text = "Пауза";
        //    }
        //}

        //private void StopButton_Click(object sender, EventArgs e)
        //{
        //    StopThread();
        //}

        //private void StartThread()
        //{
        //    isRunning = true;
        //    isPaused = false;
        //    pauseEvent.Set();
        //    workingThread = new Thread(DoWork)
        //    {
        //        IsBackground = true,
        //        Priority = ThreadPriority.Normal
        //    };
        //    workingThread.Start();
        //}

        //private void DoWork()
        //{
        //    while (isRunning)
        //    {
        //        pauseEvent.WaitOne(); // Блокируется, если пауза
        //        if (!isRunning) break;
        //        // Ваши вычисления
        //        double result = DoCalculations();

        //        // Обновляем UI
        //        if (textBoxResult.InvokeRequired)
        //        {
        //            textBoxResult.Invoke(new Action(() =>
        //            {
        //                textBoxResult.Text = $"Результат: {result:F2}";
        //            }));
        //        }
        //        else
        //        {
        //            textBoxResult.Text = $"Результат: {result:F2}";
        //        }

        //        Thread.Sleep(100);
        //    }
        //}

        //private double DoCalculations()
        //{
        //    return new Random().NextDouble() * 100;
        //}

        //private void StopThread()
        //{
        //    isRunning = false;
        //    pauseEvent.Set();

        //    if (workingThread != null && workingThread.IsAlive)
        //    {
        //        workingThread.Join(); // Ожидаем завершение потока
        //    }
        //    startButton.Enabled = true;
        //    pauseButton.Enabled = false;
        //    stopButton.Enabled = false;
        //    pauseButton.Text = "Пауза";
        //}

        //private void PriorityButton_Click(object sender, EventArgs e)
        //{
        //    if (workingThread == null) return;

        //    switch (currentPrior)
        //    {
        //        case ThreadPriority.Lowest:
        //            currentPrior = ThreadPriority.BelowNormal;
        //            break;
        //        case ThreadPriority.BelowNormal:
        //            currentPrior = ThreadPriority.Normal;
        //            break;
        //        case ThreadPriority.Normal:
        //            currentPrior = ThreadPriority.AboveNormal;
        //            break;
        //        case ThreadPriority.AboveNormal:
        //            currentPrior = ThreadPriority.Highest;
        //            break;
        //        default:
        //            currentPrior = ThreadPriority.Lowest;
        //            break;
        //    }

        //    workingThread.Priority = currentPrior;
        //    priorityButton.Text = $"Приоритет: {currentPrior}";
        //}

        //protected override void OnFormClosing(FormClosingEventArgs e)
        //{
        //    StopButton_Click(null, null);
        //    base.OnFormClosing(e);
        //}

        //private void UpdateLog(string message)
        //{
        //    if (textLog.InvokeRequired)
        //    {
        //        textLog.Invoke(new Action(() => textLog.AppendText(message + Environment.NewLine)));
        //    }
        //    else
        //    {
        //        textLog.AppendText(message + Environment.NewLine);
        //    }
        //}

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
