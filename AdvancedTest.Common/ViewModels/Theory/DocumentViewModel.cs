
namespace AdvancedTest.Common.ViewModels.Theory
{
    /// <summary>
    /// Модель представления для документов теории
    /// </summary>
    public class DocumentViewModel : TheoryPartElementViewModel
    {
        public string DocumentPath { get; set; }
        public int DocumentId { get; set; }

        public DocumentViewModel()
        {
        }
    }
}
