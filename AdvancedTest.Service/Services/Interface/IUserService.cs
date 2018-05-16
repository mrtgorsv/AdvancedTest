using System;
using System.Collections.Generic;
using AdvancedTest.Data.Model;

namespace AdvancedTest.Service.Services.Interface
{
    public interface IUserService
    {
        User LogIn(string login, string password);
        void CompleteTest(int testId, double result, DateTime endTime);
        UserTheoryTestMark StartTest(int theoryId, int userId, DateTime startDate);
        List<UserTheoryTestMark> GetUserTestProgress(int userId);
        List<UserTheoryDocumentMark> GetUserDocProgress(int userId);
    }
}
