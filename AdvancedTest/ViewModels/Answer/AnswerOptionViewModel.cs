using AdvancedTest.ViewModels.Base;

namespace AdvancedTest.ViewModels.Answer
{
    public class AnswerOptionViewModel : ViewModelBase
    {
        private bool _isSelected;
        public int Value { get; set; }
        public string Name { get; set; }

        public AnswerOptionViewModel(string answer , int index)
        {
            Name = answer;
            Value = index;
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