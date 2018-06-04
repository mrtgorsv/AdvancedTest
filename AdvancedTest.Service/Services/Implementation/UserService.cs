using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using AdvancedTest.Data.Context;
using AdvancedTest.Data.Model;
using AdvancedTest.Service.Services.Interface;

namespace AdvancedTest.Service.Services.Implementation
{
    /// <summary>
    /// Сервис для работы с пользователями
    /// </summary>
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Функция входа пользователя в систему
        /// </summary>
        public User LogIn(string login, string password)
        {
            return _context.Users
                .Include(u => u.UserTheoryTests)
                .FirstOrDefault(u => u.Login.Equals(login) && u.Password.Equals(password));
        }

        /// <summary>
        /// Функция сохранения записи о завершении теста
        /// </summary>
        public void CompleteTest(int testId, double result, DateTime endTime)
        {
            var record =
                _context.UserTheoryTestMarks.FirstOrDefault(el =>
                    el.Id.Equals(testId));
            if (record != null && !record.IsCompleted)
            {
                record.Result = result;
                record.EndTime = endTime;
                record.IsCompleted = result > 70 || record.Attempt >= 3;
                _context.UserTheoryTestMarks.AddOrUpdate(record);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Функция сохранения записи о начале теста
        /// </summary>
        public UserTheoryTestMark StartTest(int theoryId, int userId, DateTime startDate)
        {
            var newUserTest = _context.UserTheoryTestMarks.Create();
            newUserTest.StartTime = startDate;
            newUserTest.TheoryPartId = theoryId;
            newUserTest.UserId = userId;
            newUserTest.Attempt = GetPreviousAttempt(theoryId, userId) + 1;
            _context.UserTheoryTestMarks.Add(newUserTest);
            _context.SaveChanges();
            return newUserTest;

        }

        /// <summary>
        /// Функция получения результатов всех попыток прохождения тестов
        /// </summary>
        public List<UserTheoryTestMark> GetUserTestProgress(int userId)
        {
            return _context.UserTheoryTestMarks.Where(um => um.UserId.Equals(userId)).ToList();
        }

        /// <summary>
        /// Функция получения списка просмотренных документов
        /// </summary>
        public List<UserTheoryDocumentMark> GetUserDocProgress(int userId)
        {
            return _context.UserTheoryDocumentMarks.Where(um => um.UserId.Equals(userId)).ToList();
        }

        /// <summary>
        /// Функция получения количества предыдущих прохождений тестов
        /// </summary>
        private int GetPreviousAttempt(int theoryId, int userId)
        {
            return _context.UserTheoryTestMarks.Count(el => el.TheoryPartId.Equals(theoryId) && el.UserId.Equals(userId));
        }

        public User Create(string login, string password)
        {
            if (_context.Users.FirstOrDefault(u => u.Login.Equals(login)) == null)
            {
                var newUser = _context.Users.Create();
                newUser.Login = login;
                newUser.Name = login;
                newUser.Password = password;
                _context.Users.AddOrUpdate(newUser);
                _context.SaveChanges();
                return newUser;
            }
            throw new InvalidOperationException("Пользователь с таким логином уже существует");
        }
    }
}