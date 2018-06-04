﻿using AdvancedTest.EventArgs;
using System;
using System.Windows;
using System.Windows.Threading;

namespace AdvancedTest.ViewModels.Theory
{
    /// <summary>
    /// Модель представления для элемента теории
    /// </summary>
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

        // Флаг, указывающий, что задание можно запустить
        public bool CanStart => !_isStarted;

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
        private void OnTimerTick(object sender, System.EventArgs e)
        {
            if (_testTime == TimeSpan.Zero && !_endless)
            {
                CompletePart();
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
        protected virtual void StartTest()
        {
            //
        }

        // Функция завершения теста
        protected virtual void CompletePart()
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
