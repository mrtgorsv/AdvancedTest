using System;
using AdvancedTest.Data.Model;

namespace AdvancedTest.Service.Services.Interface
{
    public interface IUserService
    {
        User LogIn(string login, string password);
        void MarkDocumentAsViewed(int documentId , int userId);
        void CompleteTest(int testId, double result, DateTime endTime);
        int StartTest(int theoryId, int userId, DateTime startDate);
    }
}
