using System.Collections.Generic;
using AdvancedTest.Data.Context;
using AdvancedTest.Data.Model;

namespace AdvancedTest.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AdvancedTest.Data.Context.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppDbContext context)
        {
            if (!context.TheoryParts.Any())
            {
                CreateDeafaultUser(context);
                CreateInitialTest(context);
            }
        }

        private void CreateDeafaultUser(AppDbContext context)
        {
            var user = new User
            {
                Login = "test",
                Name = "test",
                Password = "test"
            };
            context.Users.Add(user);
            context.SaveChanges();
        }

        private void CreateInitialTest(AppDbContext context)
        {
            context.SaveChanges();
            var inputTheory = new TheoryPart
            {
                Description = "Входное тестирование",
                Name = "Входное тестирование",
                Seq = 0
            };
            var first = context.TheoryTestParts.Create();
            first.TheoryPart = inputTheory;
            first.Seq = 1;
            first.Answers = new List<TheoryTestPartAnswer>
            {
                new TheoryTestPartAnswer {Text = "11010010", TheoryTestPart = first},
                new TheoryTestPartAnswer {Text = "11100000", TheoryTestPart = first},
                new TheoryTestPartAnswer {Text = "11001110", TheoryTestPart = first},
                new TheoryTestPartAnswer {Text = "11010000 ", TheoryTestPart = first, IsCorrect = true}
            };
            var second = context.TheoryTestParts.Create();
            second.TheoryPart = inputTheory;
            second.Seq = 2;
            second.Answers = new List<TheoryTestPartAnswer>
            {
                new TheoryTestPartAnswer {Text = "Уменьшился на 20 байт", TheoryTestPart = second, IsCorrect = true},
                new TheoryTestPartAnswer {Text = "Увеличился на 20 байт", TheoryTestPart = second},
                new TheoryTestPartAnswer {Text = "Увеличился на 20 бит", TheoryTestPart = second},
                new TheoryTestPartAnswer {Text = "Уменьшился на 20 бит ", TheoryTestPart = second}
            };
            var third = context.TheoryTestParts.Create();
            third.TheoryPart = inputTheory;
            third.Seq = 3;
            third.Answers = new List<TheoryTestPartAnswer>
            {
                new TheoryTestPartAnswer {Text = "F:\\Класс10\\", TheoryTestPart = third},
                new TheoryTestPartAnswer {Text = "F:\\Класс10\\Задания\\Русский\\", TheoryTestPart = third},
                new TheoryTestPartAnswer {Text = "F:\\Класс10\\Задания", TheoryTestPart = third, IsCorrect = true},
                new TheoryTestPartAnswer {Text = "F:\\", TheoryTestPart = third}
            };
            inputTheory.TheoryTestParts = new List<TheoryTestPart> {first, second, third};
            context.TheoryParts.Add(inputTheory);
            context.SaveChanges();
        }
    }
}