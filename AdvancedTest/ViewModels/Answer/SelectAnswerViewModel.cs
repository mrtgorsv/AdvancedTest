namespace AdvancedTest.ViewModels.Answer
{
    public class SelectAnswerViewModel : AnswerViewModel
    {
        private bool _isSelected;
        public SelectAnswerViewModel()
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
