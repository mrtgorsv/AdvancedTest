using AdvancedTest.Service.Services.Interface;

namespace AdvancedTest.ViewModel.Theory
{
    public class TheoryPartViewModel : ViewModelBase
    {
        private readonly ITheoryService _theoryService;

        private int _currentTheoryPartId;

        public TheoryPartViewModel(int theoryPartId , ITheoryService theoryService)
        {
            _theoryService = theoryService;
            _currentTheoryPartId = theoryPartId;
        }
    }
}
