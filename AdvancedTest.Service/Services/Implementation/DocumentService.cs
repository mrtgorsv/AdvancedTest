using System.Data.Entity.Migrations;
using System.Linq;
using AdvancedTest.Data.Context;
using AdvancedTest.Data.Model;
using AdvancedTest.Service.Services.Interface;

namespace AdvancedTest.Service.Services.Implementation
{
    /// <summary>
    /// Сервис для работы с документами
    /// </summary>
    public class DocumentService : IDocumentService
    {
        private readonly AppDbContext _context;

        public DocumentService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Функция открытия документа для пользователя
        /// </summary>
        public void OpenDocument(int documentId, int userId)
        {
            var document = _context.TheoryDocuments.Find(documentId);
            if (document != null)
            {
                var newRecord = new UserTheoryDocumentMark
                {
                    UserId = userId,
                    TheoryPartId = document.TheoryPartId,
                    DocumentId = document.Id,
                    IsCompleted = false
                };
                _context.UserTheoryDocumentMarks.Add(newRecord);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Функция проставления отметки об открытии документа
        /// </summary>
        public void ViewDocument(int documentId, int userId)
        {
            var userDocument =
                _context.UserTheoryDocumentMarks.FirstOrDefault(ud =>
                    ud.DocumentId == documentId && ud.UserId.Equals(userId));
            if (userDocument != null)
            {
                userDocument.IsCompleted = true;
                _context.UserTheoryDocumentMarks.AddOrUpdate(userDocument);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Функция открытия следующего документа
        /// </summary>
        public int OpenNextDocument(int theoryId, int userId)
        {
            var userDocIds =
                _context.UserTheoryDocumentMarks.Where(ud => ud.UserId.Equals(userId) && ud.DocumentId.HasValue)
                    .Select(ud => ud.DocumentId.Value)
                    .ToList();

            var nextDocument = _context.TheoryDocuments
                .Where(td => td.TheoryPartId.Equals(theoryId) && !userDocIds.Contains(td.Id))
                .OrderBy(td => td.Seq)
                .FirstOrDefault();

            if (nextDocument == null) return 0;

            OpenDocument(nextDocument.Id , userId);
            return nextDocument.Id;

        }
    }
}