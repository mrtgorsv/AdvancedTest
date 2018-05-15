using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AdvancedTest.Data.Context;
using AdvancedTest.Data.Model;
using AdvancedTest.Service.Services.Interface;

namespace AdvancedTest.Service.Services.Implementation
{
    public class TheoryService : ITheoryService
    {
        private readonly AppDbContext _context;
        public TheoryService(AppDbContext context)
        {
            _context = context;
        }
        public List<TheoryPart> GetTheoryList()
        {
            return _context.TheoryParts
                .Include(tp => tp.TheoryTestParts.Select(t=> t.Answers))
                .Include(tp => tp.TheoryDocuments)
                .ToList();
        }
    }
}
