using System.Collections.Generic;
using AdvancedTest.Data.Model;

namespace AdvancedTest.Service.Services.Interface
{
    public interface ITheoryService
    {
        List<TheoryPart> GetTheoryList();
    }
}
