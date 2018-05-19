using AdvancedTest.ViewModels.Base;

namespace AdvancedTest.ViewModels.Theory
{
    public class TheoryPartElementViewModel : ViewModelBase
    {
        private bool _isVisible;
        public TheoryViewModel CurrentTheory { get; set; }

        public string Name { get; set; }

        public bool IsPractice { get; set; }
        public bool IsOpened { get; set; }

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

        public TheoryPartElementViewModel()
        {
        }
    }
}
