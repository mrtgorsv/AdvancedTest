using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AdvancedTest.Data.Context;
using AdvancedTest.Data.Model;
using AdvancedTest.Service.Services.Interface;

namespace AdvancedTest.Service.Services.Implementation
{
    public class TestService : ITestService
    {
        private readonly AppDbContext _context;

        public TestService(AppDbContext context)
        {
            _context = context;
        }

        public List<TheoryTestPart> GetParts(int theoryId)
        {
            return _context.TheoryTestParts
                .Include(tp => tp.Answers)
                .Where(tp=> tp.TheoryId.Equals(theoryId))
                .ToList();
        }
    }
}
