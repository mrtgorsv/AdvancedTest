using System.Collections.ObjectModel;
using AdvancedTest.Data.Model;
using AdvancedTest.Service.Services.Interface;

namespace AdvancedTest.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ITheoryService _theoryService;
        public MainViewModel(ITheoryService theoryService)
        {
            _theoryService = theoryService;
            LoadTheory();
        }

        private void LoadTheory()
        {
            TheoryParts = new ObservableCollection<TheoryPart>(_theoryService.GetTheoryList());
        }

        public object SelectedPart { get; set; }
        public ObservableCollection<TheoryPart> TheoryParts { get; set; }

    }
}
