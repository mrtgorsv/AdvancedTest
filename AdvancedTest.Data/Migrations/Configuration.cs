using System.Collections.Generic;
using AdvancedTest.Data.Context;
using AdvancedTest.Data.Model;

namespace AdvancedTest.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AdvancedTest.Data.Context.AppDbContext>
    {
        private int _theorySeq = 0;
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
                Seq = _theorySeq
            };
            _theorySeq++;
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
                new TheoryTestPartAnswer {Text = "Уменьшился на 20 бит", TheoryTestPart = second}
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

        private void CreateFirstTest(AppDbContext context)
        {
            context.SaveChanges();

            var firstTheoryPart = new TheoryPart
            {
                Description = "Глава 1. Транспортирование информации",
                Name = "Глава 1. Транспортирование информации",
                Seq = _theorySeq
            };
            _theorySeq++;
            var first = context.TheoryTestParts.Create();
            first.TheoryPart = firstTheoryPart;
            first.Seq = 1;
            first.Answers = new List<TheoryTestPartAnswer>
            {
                new TheoryTestPartAnswer {Text = "Html", TheoryTestPart = first},
                new TheoryTestPartAnswer {Text = "ftp", TheoryTestPart = first},
                new TheoryTestPartAnswer {Text = "www", TheoryTestPart = first},
                new TheoryTestPartAnswer {Text = "http ", TheoryTestPart = first, IsCorrect = true}
            };
            var second = context.TheoryTestParts.Create();
            second.TheoryPart = firstTheoryPart;
            second.Seq = 2;
            second.Answers = new List<TheoryTestPartAnswer>
            {
                new TheoryTestPartAnswer {Text = "www.mail.ru", TheoryTestPart = second, IsCorrect = true},
                new TheoryTestPartAnswer {Text = "mail.ru/chair806", TheoryTestPart = second},
                new TheoryTestPartAnswer {Text = "chair806", TheoryTestPart = second},
                new TheoryTestPartAnswer {Text = "ftp.html", TheoryTestPart = second},
                new TheoryTestPartAnswer {Text = "html", TheoryTestPart = second}
            };
            var third = context.TheoryTestParts.Create();
            third.TheoryPart = firstTheoryPart;
            third.Seq = 3;
            third.Answers = new List<TheoryTestPartAnswer>
            {
                new TheoryTestPartAnswer {Text = "Cеть, объединяющая компьютерные сети таким образом, чтобы пользователи и компьютеры, где бы они ни находились, могли взаимодействовать со всеми остальными участниками сети", TheoryTestPart = third, IsCorrect = true},
                new TheoryTestPartAnswer {Text = "сеть, объединяющая несколько компьютеров и дает возможность пользователям совместно использовать компьютерные ресурсы, а также подключенных в сети периферийных устройств", TheoryTestPart = third},
                new TheoryTestPartAnswer {Text = "сеть, объединяющая компьютеры и дает возможность пользователям совместно использовать информационные ресурсы в разных частях города", TheoryTestPart = third},
                new TheoryTestPartAnswer {Text = "сеть, объединяющая компьютеры и принадлежащая одной организации, дает возможность пользователям совместно использовать компьютерные ресурсы в соответствии с правилами этой организации", TheoryTestPart = third}
            };
            firstTheoryPart.TheoryTestParts = new List<TheoryTestPart> { first, second, third };

            firstTheoryPart.TheoryDocuments = new List<TheoryDocument>
            {
                new TheoryDocument {TheoryPart = firstTheoryPart , IsVisible = true , Name = "Транспортирование информации(версия 1)" , Seq = 1},
                new TheoryDocument {TheoryPart = firstTheoryPart , IsVisible = true , Name = "Транспортирование информации(версия 2)" , Seq = 2}
            };
            context.TheoryParts.Add(firstTheoryPart);
            context.SaveChanges();
        }
    }
}