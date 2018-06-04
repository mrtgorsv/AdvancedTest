using System.Linq;

namespace AdvancedTest.ViewModels.TestPart
{
    /// <summary>
    /// Модель представления для формы задания с множественным выбором ответов
    /// </summary>
    public class SelectManyTestPartViewModel : TestPartViewModelBase
    {
        public override string GetUserAnswer()
        {
            return string.Join(",", Answers.Where(a => a.IsSelected).Select(a => a.Seq));
        }
    }
}