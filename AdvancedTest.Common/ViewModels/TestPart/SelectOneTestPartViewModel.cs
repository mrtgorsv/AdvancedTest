using System.Linq;

namespace AdvancedTest.Common.ViewModels.TestPart
{
    /// <summary>
    /// Модель представления для формы задания с единичным выбором ответа
    /// </summary>
    public class SelectOneTestPartViewModel : TestPartViewModelBase
    {
        public override string GetUserAnswer()
        {
            return Answers.FirstOrDefault(a => a.IsSelected)?.Seq.ToString() ?? string.Empty;
        }
    }
}