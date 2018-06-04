using AdvancedTest.ViewModels.Theory;

namespace AdvancedTest.ViewModels.Practice
{
    /// <summary>
    /// Модель представления для документов теории
    /// </summary>
    public partial class WordPracticeViewModel : TimerTheoryPartElementViewModel
    {
        private string _buttonText;
        public string DocumentPath { get; set; }
        public int DocumentId { get; set; }

        public byte[] StartDocument { get; set; }
        public byte[] ResultDocument { get; set; }
        public string RulesDescription { get; set; }
        public string ButtonText
        {
            get => _buttonText;
            set
            {
                _buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        public WordPracticeViewModel()
        {
            ButtonText = "Начать";
            InitializeCommands();
        }
    }
}
