using System.Linq;

namespace AdvancedTest.ViewModels.TestPart
{
    /// <summary>
    /// Модель представления для формы задания с сопоставлением
    /// </summary>
    public class CompareTestPartViewModel : TestPartViewModelBase
    {
        public override string GetUserAnswer()
        {
            return string.Join("", Answers.Select(a => a.SelectedOption));
        }
    }
}