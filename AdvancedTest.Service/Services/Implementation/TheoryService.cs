using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AdvancedTest.Data.Context;
using AdvancedTest.Data.Model;
using AdvancedTest.Service.Services.Interface;

namespace AdvancedTest.Service.Services.Implementation
{
    /// <summary>
    /// Сервис для работы с главами
    /// </summary>
    public class TheoryService : ITheoryService
    {
        private readonly AppDbContext _context;
        private readonly IDocumentService _documentService;

        public TheoryService(AppDbContext context, IDocumentService documentService)
        {
            _context = context;
            _documentService = documentService;
        }

        /// <summary>
        /// Функция получения списка глав
        /// </summary>
        public List<TheoryPart> GetTheoryList()
        {
            return _context.TheoryParts
                .Include(tp => tp.TheoryTestParts.Select(t=> t.Answers))
                .Include(tp => tp.TheoryDocuments)
                .OrderBy(t=> t.Seq)
                .ToList();
        }

        /// <summary>
        /// Функция открытия главы для пользователя
        /// </summary>
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

        /// <summary>
        /// Функция проверки завршенности всех глав
        /// </summary>
        public bool IsTheoryComplete(int userId)
        {
            return _context.UserTheoryTestMarks.FirstOrDefault(ut => ut.TheoryPart.IsLast && ut.UserId.Equals(userId)) != null;
        }

        /// <summary>
        /// Функция получения результатов пользователя
        /// </summary>
        public List<string> GetUserResults(int userId, out bool allTheoryComplete)
        {
            allTheoryComplete = true;
            var allTheory = _context.TheoryParts.OrderBy(t => t.Seq).ToList();
            List<string> result = new List<string>();
            var allResults = _context.UserTheoryTestMarks.Where(ut => ut.UserId.Equals(userId))
                .ToList()
                .GroupBy(ut => ut.TheoryPartId)
                .Select(utg => utg.OrderBy(ut => ut.Result).FirstOrDefault())
                .Where(ut => ut != null)
                .ToList();
            foreach (TheoryPart theoryPart in allTheory)
            {
                var currentTheoryResult = allResults.FirstOrDefault(r => r.TheoryPartId.Equals(theoryPart.Id));
                if (currentTheoryResult != null)
                {
                    result.Add($"{theoryPart.Name} - {currentTheoryResult.Result:F2}%");
                }
                else
                {
                    allTheoryComplete = false;
                    result.Add($"{theoryPart.Name} - не пройдено.");
                }
            }
               return result;
        }
    }
}
