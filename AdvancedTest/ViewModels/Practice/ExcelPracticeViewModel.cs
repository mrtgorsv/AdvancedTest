using System.Collections.ObjectModel;
using AdvancedTest.ViewModels.Answer;
using AdvancedTest.ViewModels.TestPart;
using AdvancedTest.ViewModels.Theory;

namespace AdvancedTest.ViewModels.Practice
{
    /// <summary>
    /// Модель представления для документов теории
    /// </summary>
    public partial class ExcelPracticeViewModel : TimerTheoryPartElementViewModel
    {
        private string _buttonText;
        public int DocumentId { get; set; }
        public string DocumentPath => $"2\\{SelectedOption + 1}";
        public string Description { get; set; }
        public string ButtonText
        {
            get => _buttonText;
            set
            {
                _buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }
        public string SelectionDescription => Options[_selectedOption].Description;

        public ExcelPracticeViewModel()
        {
            ButtonText = "Начать";
            SetEndless(true);
            InitializeCommands();
        }


        private int _selectedOption;

        public TestPartViewModelBase CurrentTestPart { get; set; }

        public ObservableCollection<AnswerOptionViewModel> Options { get; set; }
        public int SelectedOption
        {
            get => _selectedOption;
            set
            {
                _selectedOption = value;
                OnPropertyChanged(nameof(SelectedOption));
                OnPropertyChanged(nameof(SelectionDescription));
            }
        }
    }
}
