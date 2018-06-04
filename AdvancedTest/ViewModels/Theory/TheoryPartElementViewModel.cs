using AdvancedTest.ViewModels.Base;

namespace AdvancedTest.ViewModels.Theory
{
    /// <summary>
    /// Модель представления для элемента теории
    /// </summary>
    public class TheoryPartElementViewModel : ViewModelBase
    {
        // Флаг видимости 
        private bool _isVisible;

        // Текущая глава
        public TheoryViewModel CurrentTheory { get; set; }
        public virtual int CurrentTheoryId { get; set; }
        // Название
        public string Name { get; set; }

        // Флаг, указывающий, что это практическое задание
        public bool IsPractice { get; set; }
        // Флаг, указывающий, что документ уже был открыт
        public bool IsOpened { get; set; }

        // Порядок документа в списке
        public int Seq { get; set; }


        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }
    }
}
