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
        private readonly IDocumentService _documentService;

        public TheoryService(AppDbContext context, IDocumentService documentService)
        {
            _context = context;
            _documentService = documentService;
        }
        public List<TheoryPart> GetTheoryList()
        {
            return _context.TheoryParts
                .Include(tp => tp.TheoryTestParts.Select(t=> t.Answers))
                .Include(tp => tp.TheoryDocuments)
                .OrderBy(t=> t.Seq)
                .ToList();
        }

        public void OpenTheory(int theoryId, int userId)
        {
            var record =
                _context.UserTheoryDocumentMarks.FirstOrDefault(el =>
                    el.UserId == userId && el.DocumentId == null && el.TheoryPartId.Equals(theoryId));
            if (record == null)
            {
                var newRecord = new UserTheoryDocumentMark
                {
                    UserId = userId,
                    TheoryPartId = theoryId,
                    IsCompleted = true
                };
                _context.UserTheoryDocumentMarks.Add(newRecord);
                _context.SaveChanges();

                var firstDoc = _context.TheoryDocuments.Where(td => td.TheoryPartId.Equals(theoryId))
                    .OrderBy(td => td.Seq).FirstOrDefault();

                if (firstDoc != null)
                {
                    _documentService.OpenDocument(firstDoc.Id , userId);
                }
            }
        }
    }
}
