using System;
using System.Linq;
using AdvancedTest.Common.EventArgs;
using AdvancedTest.Common.Extensions;
using AdvancedTest.Common.Utils;

namespace AdvancedTest.Common.ViewModels.Test
{
    /// <summary>
    /// Модель представления для формы теста
    /// </summary>
    public partial class TestViewModel
    {
        // Команда перехода к следующему заданию теста
        public DelegateCommand NextPartCommand { get; set; }
        // Команда запуска теста
        public DelegateCommand StartTestCommand { get; set; }

        // Функция инициализации команд
        private void InitializeCommands()
        {
            NextPartCommand = new DelegateCommand(NextTestPart);
            StartTestCommand = new DelegateCommand(Start);
        }

        // Функция перехода к следующему заданию
        private void NextTestPart()
        {
            var nextPart = _testParts.GetNext(CurrentTestPart);
            if (nextPart != null)
            {
                CurrentTestPart = nextPart;
            }
            else
            {
                Complete();
            }
            if (CanComplete())
            {
                NextButtonText = "Завершить";
            }
        }

        // Функция проверки завершенности заданий
        private bool CanComplete()
        {
            return _testParts.GetNext(CurrentTestPart) == null;
        }

        // Функция запуска теста
        protected override void Start()
        {
            CurrentTestPart = _testParts.FirstOrDefault();
            IsStarted = true;
            NextButtonText = "Далее";
            _userTest = _userService.StartTest(_theoryId, _securityManager.CurrentUser.Id, DateTime.Now);
            StartTimer();
        }

        // Функция завершения теста
        protected override void Complete()
        {
            StopTimer();
            var result = GetTestResult();
            _userService.CompleteTest(_userTest.Id, result, DateTime.Now);
            OnTestCompleted(new TestCompletedEventArgs(result, CurrentTheoryId, _userTest.Attempt , TimeSpan.Zero));
        }
    }
}
