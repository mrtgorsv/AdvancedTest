using AdvancedTest.Service.Services.Interface;

namespace AdvancedTest.ViewModel.Theory
{
    public class TestViewModel : TheoryPartViewModel
    {
        private readonly ITheoryService _theoryService;

        private int _currentTheoryPartId;

        public TestViewModel(int theoryPartId , ITheoryService theoryService)
        {
            _theoryService = theoryService;
            _currentTheoryPartId = theoryPartId;
        }
    }
}
