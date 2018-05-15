using AdvancedTest.ViewModel;

namespace AdvancedTest.ViewModels.Theory
{
    public class TheoryPartElementViewModel : ViewModelBase
    {
        public TheoryViewModel CurrentTheory { get; set; }

        public string Name { get; set; }

        public int Seq { get; set; }

        public bool IsVisible { get; set; }

        public TheoryPartElementViewModel()
        {
        }
    }
}
