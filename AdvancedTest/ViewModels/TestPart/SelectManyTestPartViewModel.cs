using System.Linq;

namespace AdvancedTest.ViewModels.TestPart
{
    public class SelectManyTestPartViewModel : TestPartViewModelBase
    {
        public override string GetUserAnswer()
        {
            return string.Join(",", Answers.Where(a => a.IsSelected));
        }
    }
}