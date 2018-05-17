using System.Linq;

namespace AdvancedTest.ViewModels.TestPart
{
    public class SelectOneTestPartViewModel : TestPartViewModelBase
    {
        public override string GetUserAnswer()
        {
            return Answers.FirstOrDefault(a => a.IsSelected)?.Seq.ToString() ?? string.Empty;
        }
    }
}