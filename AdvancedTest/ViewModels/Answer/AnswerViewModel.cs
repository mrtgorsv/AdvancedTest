using AdvancedTest.ViewModel;
using AdvancedTest.ViewModels.Test;

namespace AdvancedTest.ViewModels.Answer
{
    public class AnswerViewModel : ViewModelBase
    {
        private TestPartViewModel CurrentTestPartViewModel{ get; set; }

        public int Seq { get; set; }
        public int AnswerId { get; set; }
        public string Text { get; set; }
        public string ImagePath { get; set; }

        public TestPartViewModel CurrentTestPart { get; set; }

        public AnswerViewModel()
        {
        }
    }
}
