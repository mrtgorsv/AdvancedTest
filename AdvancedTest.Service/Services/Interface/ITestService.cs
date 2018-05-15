using System.Collections.Generic;
using AdvancedTest.Data.Model;

namespace AdvancedTest.Service.Services.Interface
{
    public interface ITestService
    {
        List<TheoryTestPart> GetParts(int theoryId);
    }
}
