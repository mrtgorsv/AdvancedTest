using AdvancedTest.Service.Services.Interface;

namespace AdvancedTest.ViewModel.Theory
{
    public class DocumentViewModel : TheoryPartViewModel
    {
        private readonly ITheoryService _theoryService;

        private int _currentTheoryPartId;

        public DocumentViewModel(int theoryPartId , ITheoryService theoryService)
        {
            _theoryService = theoryService;
            _currentTheoryPartId = theoryPartId;
        }
    }
}
