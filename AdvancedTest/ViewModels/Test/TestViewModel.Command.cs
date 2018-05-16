using System;
using System.Linq;
using AdvancedTest.Extensions;
using AdvancedTest.Utils;

namespace AdvancedTest.ViewModels.Test
{
    public partial class TestViewModel
    {
        public DelegateCommand NextPartCommand { get; set; }
        public DelegateCommand StartTestCommand { get; set; }

        private void InitializeCommands()
        {
            NextPartCommand = new DelegateCommand(NextTestPart);
            StartTestCommand = new DelegateCommand(StartTest);
        }

        private void NextTestPart()
        {
            var nextPart = _testParts.GetNext(CurrentTestPart);
            if (nextPart != null)
            {
                CurrentTestPart = nextPart;
            }
            else
            {
                CompleteTest();
            }
            if (CanComplete())
            {
                NextButtonText = "Завершить";
            }
        }

        private bool CanComplete()
        {
            return _testParts.GetNext(CurrentTestPart) == null;
        }

        private void StartTest()
        {
            CurrentTestPart = _testParts.FirstOrDefault();
            IsStarted = true;
            NextButtonText = "Далее";
            _userTestId = _userService.StartTest(_theoryId, _securityManager.CurrentUser.Id, DateTime.Now);
        }

        public void CompleteTest()
        {
            _userService.CompleteTest(_userTestId, GetTestResult(), DateTime.Now);
        }
    }
}
