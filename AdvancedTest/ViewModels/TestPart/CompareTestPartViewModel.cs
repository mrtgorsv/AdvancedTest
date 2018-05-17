using System.Linq;

namespace AdvancedTest.ViewModels.TestPart
{
    public class CompareTestPartViewModel : TestPartViewModelBase
    {
        public override string GetUserAnswer()
        {
            return string.Join("", Answers.Select(a => a.SelectedOption));
        }
    }
}