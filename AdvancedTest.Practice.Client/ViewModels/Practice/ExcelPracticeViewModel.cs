using AdvancedTest.Common.ViewModels.Practice;

namespace AdvancedTest.Practice.Client.ViewModels.Practice
{
    /// <summary>
    /// Модель представления для документов теории
    /// </summary>
    public partial class ExcelPracticeViewModel : PracticeViewModel
    {
        private string _buttonText;
        public override string DocumentPath => $"2\\{SelectedOption + 1}";
        public override string ButtonText
        {
            get => _buttonText;
            set
            {
                _buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }
        public override string SelectionDescription => Options[_selectedOption].Description;

        public ExcelPracticeViewModel()
        {
            _buttonText = "Начать";
        }

        private int _selectedOption;

        public override int SelectedOption
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
