using System.Collections.ObjectModel;
using AdvancedTest.Common.ViewModels.Base;

namespace AdvancedTest.Common.ViewModels.Theory
{
        // Текущая глава
    public class TheoryViewModel : ViewModelBase
    {
        private bool _isVisible;
        public int CurrentTheoryId { get; set; }
        public TheorySectionViewModel TheorySection { get; set; }
        public string Name { get; set; }
        public int Seq { get; set; }
        public int TestTime { get; set; }

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public ObservableCollection<TheoryPartElementViewModel> TheoryPartElements { get; set; }

        public TheoryViewModel()
        {
            TheoryPartElements = new ObservableCollection<TheoryPartElementViewModel>();
        }
    }
}