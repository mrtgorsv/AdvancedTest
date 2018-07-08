using System.Collections.Generic;
using AdvancedTest.Data.Model;

namespace AdvancedTest.Service.Services.Interface
{
    public interface ITheoryService
    {
        List<TheoryPart> GetTheoryList();
        List<TheorySection> GetTheorySectionList();
        void OpenTheory(int theoryId, int userId);
        bool IsTheoryComplete(int userId);
        List<string> GetUserResults(int userId , out bool allTheoryComplete);
    }
}
