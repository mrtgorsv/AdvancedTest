using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using AdvancedTest.Data.Context;
using AdvancedTest.Data.Model;
using AdvancedTest.Service.Services.Interface;

namespace AdvancedTest.Service.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public User LogIn(string login, string password)
        {
            return _context.Users
                .Include(u => u.UserTheoryTests)
                .FirstOrDefault(u => u.Login.Equals(login) && u.Password.Equals(password));
        }

        public void MarkDocumentAsViewed(int documentId, int userId)
        {
            var record =
                _context.UserTheoryDocumentMarks.FirstOrDefault(el =>
                    el.UserId == userId && el.DocumentId == documentId);
            if (record == null)
            {
                var newRecord = new UserTheoryDocumentMark
                {
                    UserId = userId,
                    DocumentId = documentId,
                    IsCompleted = true
                };
                _context.UserTheoryDocumentMarks.Add(newRecord);
                _context.SaveChanges();
            }
        }

        public void CompleteTest(int testId, double result, DateTime endTime)
        {
            var record =
                _context.UserTheoryTestMarks.FirstOrDefault(el =>
                    el.Id.Equals(testId));
            if (record != null)
            {
                record.Result = result;
                record.EndTime = endTime;
                record.IsCompleted = result > 70 || record.Attempt >= 3;
                _context.UserTheoryTestMarks.AddOrUpdate(record);
                _context.SaveChanges();
            }
        }

        public int StartTest(int theoryId, int userId, DateTime startDate)
        {
            var newUserTest = _context.UserTheoryTestMarks.Create();
            newUserTest.StartTime = startDate;
            newUserTest.TheoryPartId = theoryId;
            newUserTest.UserId = userId;
            newUserTest.Attempt = GetPreviousAttempt(theoryId, userId) + 1;
            _context.SaveChanges();
            return newUserTest.Id;

        }

        private int GetPreviousAttempt(int theoryId, int userId)
        {
            return _context.UserTheoryTestMarks.Count(el => el.TheoryPartId.Equals(theoryId) && el.UserId.Equals(userId));
        }
    }
}