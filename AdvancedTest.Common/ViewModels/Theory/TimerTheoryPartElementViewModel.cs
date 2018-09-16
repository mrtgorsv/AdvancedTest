using System;
using System.Windows;
using System.Windows.Threading;
using AdvancedTest.Common.Event;

namespace AdvancedTest.Common.ViewModels.Theory
{
    /// <summary>
    /// Модель представления для элемента теории
    /// </summary>
    [Serializable]
    public class TimerTheoryPartElementViewModel : TheoryPartElementViewModel
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer(DispatcherPriority.Render, Application.Current.Dispatcher);
        private TimeSpan _testTime;
        private bool _endless;
        protected TimeSpan _elapsedTime = TimeSpan.Zero;
        private int _timerTick = 1;
        private bool _isStarted;
        // Оставшееся время на выполнения
        public string TestTime { get; set; }
        public string ElapsedTime => _elapsedTime.Equals(TimeSpan.Zero) ? string.Empty : _elapsedTime.ToString("T");

        public virtual bool CanEdit { get; set; } = true;

        // Флаг, указывающий, что задание можно запустить
        public virtual bool CanStart => !_isStarted && CanEdit;

        // Флаг, указывающий, что тест начался
        public bool IsStarted
        {
            get => _isStarted;
            set
            {
                _isStarted = value;
                OnPropertyChanged(nameof(IsStarted));
                OnPropertyChanged(nameof(CanStart));
            }
        }
        public TimerTheoryPartElementViewModel()
        {
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTimerTick;
        }
        // Событие завершения теста
        public event TestCompletedEventHandler TestCompleted;
        // Обработчик события завершения теста
        public delegate void TestCompletedEventHandler(object sender, TestCompletedEventArgs args);

        // Функция обработки события переключения таймера
        private void OnTimerTick(object sender, EventArgs e)
        {
            if (_testTime == TimeSpan.Zero && !_endless)
            {
                Complete();
            }

            _testTime = _testTime.Add(TimeSpan.FromSeconds(-_timerTick));
            _elapsedTime = _elapsedTime.Add(TimeSpan.FromSeconds(_timerTick));
            TestTime = _testTime.ToString("T");
            OnPropertyChanged(nameof(TestTime));
            OnPropertyChanged(nameof(ElapsedTime));
        }

        // Функция вызова события завершения задания
        protected virtual void OnTestCompleted(TestCompletedEventArgs result)
        {
            TestCompleted?.Invoke(this, result);
        }
        // Функция вызова события завершения задания
        protected virtual void OnTestFailed(TestCompletedEventArgs result)
        {
            TestCompleted?.Invoke(this, result);
        }

        // Функция установки начального времени, отведенного на тест
        public void SetTestTime(TimeSpan testTime)
        {
            _testTime = testTime;
        }

        // Функция запуска теста
        protected virtual void Start()
        {
            //
        }

        // Функция завершения теста
        protected virtual void Complete()
        {
            //
        }

        protected void StartTimer()
        {
            _timer.Start();
        }

        protected void StopTimer()
        {
            _timer.Stop();
        }
        protected void SetEndless(bool endless)
        {
            _endless = endless;
        }
    }
}
