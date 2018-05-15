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
            if (!CanNext)
            {
                NextButtonText = "Завершить";
            }
        }
        private void StartTest()
        {
            CurrentTestPart = _testParts.FirstOrDefault();
            IsStarted = true;
            NextButtonText = "Далее";
        }

        public void CompleteTest()
        {

        }
    }
}
