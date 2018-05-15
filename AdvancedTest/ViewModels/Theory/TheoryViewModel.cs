using System.Collections.ObjectModel;
using AdvancedTest.ViewModel;

namespace AdvancedTest.ViewModels.Theory
{
    public class TheoryViewModel : ViewModelBase
    {
        public int CurrentTheoryId { get; set; }
        public string Name { get; set; }
        public int Seq { get; set; }

        public ObservableCollection<TheoryPartElementViewModel> TheoryPartElements { get; set; }

        public TheoryViewModel()
        {
            TheoryPartElements = new ObservableCollection<TheoryPartElementViewModel>();
        }
    }
}