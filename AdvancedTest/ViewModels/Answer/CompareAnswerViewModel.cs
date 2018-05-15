using System.Collections.ObjectModel;

namespace AdvancedTest.ViewModels.Answer
{
    public class CompareAnswerViewModel : AnswerViewModel
    {
        private CompareAnswerOptionViewModel _selectedOption;

        public CompareAnswerViewModel()
        {
        }

        public ObservableCollection<CompareAnswerOptionViewModel> Options { get; set; }
        public CompareAnswerOptionViewModel SelectedOption
        {
            get => _selectedOption;
            set
            {
                _selectedOption = value;
                OnPropertyChanged(nameof(SelectedOption));
            }
        }
    }
}
