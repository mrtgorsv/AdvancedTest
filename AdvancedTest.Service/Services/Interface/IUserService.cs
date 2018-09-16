using System;
using System.Collections.Generic;
using AdvancedTest.Data.Model;

namespace AdvancedTest.Service.Services.Interface
{
    public interface IUserService
    {
        User LogIn(string login, string password);
        User Create(string login, string password);
        void CompleteTest(int testId, double result, DateTime endTime);
        UserTheoryTestMark CreateWork(int theoryId, int userId, DateTime? startDate = null, int? optionId = null);
        List<UserTheoryTestMark> GetUserTestProgress(int userId);
        UserTheoryTestMark GetPractice(int userId , int practiceId);
        List<UserTheoryDocumentMark> GetUserDocProgress(int userId);
        void CompleteWork(UserTheoryTestMark practiceEntity);
        void StartWork(UserTheoryTestMark practice);
    }
}
