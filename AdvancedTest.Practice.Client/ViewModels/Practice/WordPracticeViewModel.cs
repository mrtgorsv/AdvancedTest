using AdvancedTest.Common.ViewModels.Practice;
using AdvancedTest.Data.Enum;

namespace AdvancedTest.Practice.Client.ViewModels.Practice
{
    /// <summary>
    /// Модель представления для документов теории
    /// </summary>
    public partial class WordPracticeViewModel : PracticeViewModel
    {
        private string _buttonText;
        public byte[] StartDocument { get; set; }
        public byte[] ResultDocument { get; set; }
        public string RulesDescription { get; set; }

        public override PracticeType PracticeType => PracticeType.Word;

        public override string ButtonText
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
            _buttonText = "Начать";
            InitializeCommands();
        }
    }
}
