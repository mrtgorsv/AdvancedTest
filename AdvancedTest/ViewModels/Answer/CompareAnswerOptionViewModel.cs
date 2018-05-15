using AdvancedTest.ViewModel;

namespace AdvancedTest.ViewModels.Answer
{
    public class CompareAnswerOptionViewModel : ViewModelBase
    {
        private bool _isSelected;
        public int Value { get; set; }
        public string Name { get; set; }

        public CompareAnswerOptionViewModel()
        {
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
    }
}