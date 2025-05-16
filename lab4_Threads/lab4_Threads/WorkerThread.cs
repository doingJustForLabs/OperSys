using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab4_Threads
{
    public class WorkerThread
    {
        public int Id { get; }
        public ThreadPriority CurrentPriority { get; private set; }
        private Thread _thread;
        private bool _isRunning;
        private ManualResetEvent _pauseEvent = new ManualResetEvent(true);
        private ConcurrentQueue<ThreadMessage> _queue;
        private Action<string, Color, string> _logAction;
        private Random _random = new Random();

        public bool IsPaused { get; private set; }

        public WorkerThread(int id, ConcurrentQueue<ThreadMessage> queue, Action<string, Color, string> logAction)
        {
            Id = id;
            _queue = queue;
            _logAction = logAction;
            CurrentPriority = ThreadPriority.Normal;
            _thread = new Thread(Work) { IsBackground = true, Priority = CurrentPriority };
            _isRunning = true;
        }

        public void Start() => _thread.Start();

        public void Pause()
        {
            _pauseEvent.Reset();
            IsPaused = true;
            _logAction?.Invoke($"THREAD-{Id}", Color.Orange, $"Поток {Id} приостановлен");
        }

        public void Resume()
        {
            _pauseEvent.Set();
            IsPaused = false;
            _logAction?.Invoke($"THREAD-{Id}", Color.Green, $"Поток {Id} возобновлен");
        }

        public void Stop()
        {
            _isRunning = false;
            Resume(); // Разблокируем перед остановкой
                      //_pauseEvent.Set();
            if (!_thread.Join(300))
            {
                _thread.Interrupt(); // Мягкое прерывание
                if (!_thread.Join(100))
                    _thread.Abort();
            }
            _logAction?.Invoke($"THREAD-{Id}", Color.Red, $"Поток {Id} остановлен");
        }

        public void ChangePriority()
        {
            switch (CurrentPriority)
            {
                case ThreadPriority.Lowest:
                    CurrentPriority = ThreadPriority.BelowNormal;
                    break;
                case ThreadPriority.BelowNormal:
                    CurrentPriority = ThreadPriority.Normal;
                    break;
                case ThreadPriority.Normal:
                    CurrentPriority = ThreadPriority.AboveNormal;
                    break;
                case ThreadPriority.AboveNormal:
                    CurrentPriority = ThreadPriority.Highest;
                    break;
                default:
                    CurrentPriority = ThreadPriority.Lowest;
                    break;
            }
            _thread.Priority = CurrentPriority;
            _logAction?.Invoke($"THREAD-{Id}", Color.Green, $"Приоритет изменен на {CurrentPriority}");
        }

        private void Work()
        {
            _logAction?.Invoke($"THREAD-{Id}", Color.Blue, $"Поток {Id} начал работу");
            while (_isRunning)
            {
                _pauseEvent.WaitOne(); // Ждет сигнала

                if (!_isRunning) break;

                // Отправка сообщения
                if (_queue.TryDequeue(out var receivedMsg))
                {
                    if (receivedMsg.SenderId != Id)
                    {
                        _logAction?.Invoke($"THREAD-{Id}", Color.DarkCyan,
                            $"Получено от {receivedMsg.SenderId}: {receivedMsg.Content}");
                        Thread.Sleep(500); // Искусственная задержка 
                    }
                }

                if (_random.Next(0, 100) < 30)
                {
                    var msg = new ThreadMessage(Id, _random.Next(1, 101));

                    _queue.Enqueue(msg);
                    _logAction?.Invoke($"THREAD-{Id}", Color.DarkBlue, $"Отправлено: {msg.Content}");
                }

                Thread.Sleep(500);
            }
        }
    }

    public class ThreadMessage
    {
        public int SenderId { get; }
        public int Content { get; }

        public ThreadMessage(int senderId, int content)
        {
            SenderId = senderId;
            Content = content;
        }
    }
}
