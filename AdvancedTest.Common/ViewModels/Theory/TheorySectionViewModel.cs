using System.Collections.ObjectModel;
using AdvancedTest.Common.ViewModels.Base;

namespace AdvancedTest.Common.ViewModels.Theory
{
        // Текущая раздел
    public class TheorySectionViewModel : ViewModelBase
    {
        private bool _isVisible;
        public int CurrentSectionId { get; set; }
        public string Name { get; set; }
        public int Seq { get; set; }

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public ObservableCollection<TheoryViewModel> TheoryParts { get; set; }

        public TheorySectionViewModel()
        {
            TheoryParts = new ObservableCollection<TheoryViewModel>();
        }
    }
}