using System.Collections.ObjectModel;
using AdvancedTest.ViewModels.Base;
using AdvancedTest.ViewModels.TestPart;

namespace AdvancedTest.ViewModels.Answer
{
    public class AnswerViewModel : ViewModelBase
    {
        private bool _isSelected;

        public int Seq { get; set; }
        public int AnswerId { get; set; }
        public bool TextMode { get; set; }

        public bool ImageMode
        {
            get { return !TextMode; }
        }

        private int _selectedOption;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public TestPartViewModelBase CurrentTestPart { get; set; }

        public ObservableCollection<AnswerOptionViewModel> Options { get; set; }
        public int SelectedOption
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
