using System.Collections.Generic;
using AdvancedTest.Data.Context;
using AdvancedTest.Data.Enum;
using AdvancedTest.Data.Model;

namespace AdvancedTest.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        private int _theorySeq;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppDbContext context)
        {
            if (!context.TheoryParts.Any())
            {
                CreateDefaultUser(context);
                CreateInitialTest(context);
                CreateFirstTest(context);
            }
        }

        private void CreateDefaultUser(AppDbContext context)
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
                Seq = _theorySeq,
                TestLength = 40
            };
            _theorySeq++;
            var first = new TheoryTestPart
            {
                TheoryPart = inputTheory,
                Seq = 1,
                TestType = TestPartType.SingleChoice,
                CorrectAnswer = "4"
            };
            first.Answers = new List<TheoryTestPartAnswer>
            {
                CreateAnswer(first , "11010010" , 1),
                CreateAnswer(first , "11100000" , 2),
                CreateAnswer(first , "11001110" , 3),
                CreateAnswer(first , "11010000" , 4)
            };
            var second = new TheoryTestPart
            {
                TheoryPart = inputTheory,
                Seq = 2,
                TestType = TestPartType.SingleChoice,
                CorrectAnswer = "1"
            };
            second.Answers = new List<TheoryTestPartAnswer>
            {
                CreateAnswer(second , "Уменьшился на 20 байт" , 1),
                CreateAnswer(second , "Увеличился на 20 байт" , 2),
                CreateAnswer(second , "Увеличился на 20 бит" , 3),
                CreateAnswer(second , "Уменьшился на 20 бит" , 4)
            };
            var third = new TheoryTestPart
            {
                TheoryPart = inputTheory,
                Seq = 3,
                TestType = TestPartType.SingleChoice,
                CorrectAnswer = "3"
            };
            third.Answers = new List<TheoryTestPartAnswer>
            {
                CreateAnswer(third , "F:\\Класс10\\" , 1),
                CreateAnswer(third , "F:\\Класс10\\Задания\\Русский\\" , 2),
                CreateAnswer(third , "F:\\Класс10\\Задания" , 3 ),
                CreateAnswer(third , "F:\\" , 4)
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
                Seq = _theorySeq,
                TestLength = 20
            };
            _theorySeq++;
            var first = new TheoryTestPart
            {
                TheoryPart = firstTheoryPart,
                Seq = 1,
                TestType = TestPartType.SingleChoice,
                CorrectAnswer = "4"
            };
            first.Answers = new List<TheoryTestPartAnswer>
            {
                new TheoryTestPartAnswer {Text = "Html", TheoryTestPart = first, AnswerNumber = 1},
                new TheoryTestPartAnswer {Text = "ftp", TheoryTestPart = first, AnswerNumber = 2},
                new TheoryTestPartAnswer {Text = "www", TheoryTestPart = first, AnswerNumber = 3},
                new TheoryTestPartAnswer {Text = "http ", TheoryTestPart = first, AnswerNumber = 4}
            };
            var second = new TheoryTestPart
            {
                TheoryPart = firstTheoryPart,
                Seq = 2,
                TestType = TestPartType.SingleChoice,
                CorrectAnswer = "1"
            };
            second.Answers = new List<TheoryTestPartAnswer>
            {
                new TheoryTestPartAnswer {Text = "www.mail.ru", TheoryTestPart = second, AnswerNumber = 1},
                new TheoryTestPartAnswer {Text = "mail.ru/chair806", TheoryTestPart = second, AnswerNumber = 2},
                new TheoryTestPartAnswer {Text = "chair806", TheoryTestPart = second, AnswerNumber = 3},
                new TheoryTestPartAnswer {Text = "ftp.html", TheoryTestPart = second, AnswerNumber = 4},
                new TheoryTestPartAnswer {Text = "html", TheoryTestPart = second, AnswerNumber = 5}
            };
            var third = new TheoryTestPart
            {
                TheoryPart = firstTheoryPart,
                Seq = 3,
                TestType = TestPartType.MultiplyChoice,
                CorrectAnswer = "1"
            };
            third.Answers = new List<TheoryTestPartAnswer>
            {
                new TheoryTestPartAnswer
                {
                    Text =
                        "Cеть, объединяющая компьютерные сети таким образом, чтобы пользователи и компьютеры, где бы они ни находились, могли взаимодействовать со всеми остальными участниками сети",
                    TheoryTestPart = third,
                    AnswerNumber = 1
                },
                new TheoryTestPartAnswer
                {
                    Text =
                        "сеть, объединяющая несколько компьютеров и дает возможность пользователям совместно использовать компьютерные ресурсы, а также подключенных в сети периферийных устройств",
                    TheoryTestPart = third,
                    AnswerNumber = 2
                },
                new TheoryTestPartAnswer
                {
                    Text =
                        "сеть, объединяющая компьютеры и дает возможность пользователям совместно использовать информационные ресурсы в разных частях города",
                    TheoryTestPart = third,
                    AnswerNumber = 3
                },
                new TheoryTestPartAnswer
                {
                    Text =
                        "сеть, объединяющая компьютеры и принадлежащая одной организации, дает возможность пользователям совместно использовать компьютерные ресурсы в соответствии с правилами этой организации",
                    TheoryTestPart = third,
                    AnswerNumber = 4
                }
            };
            firstTheoryPart.TheoryTestParts = new List<TheoryTestPart> {first, second, third};

            firstTheoryPart.TheoryDocuments = new List<TheoryDocument>
            {
                new TheoryDocument
                {
                    TheoryPart = firstTheoryPart,
                    IsVisible = true,
                    Name = "Транспортирование информации(версия 1)",
                    Seq = 1
                },
                new TheoryDocument
                {
                    TheoryPart = firstTheoryPart,
                    IsVisible = true,
                    Name = "Транспортирование информации(версия 2)",
                    Seq = 2
                }
            };
            context.TheoryParts.Add(firstTheoryPart);
            context.SaveChanges();
        }

        private TheoryTestPartAnswer CreateAnswer(TheoryTestPart test, string text, int seq , string imagePath = null)
        {
            return new TheoryTestPartAnswer
            {
                AnswerNumber = seq,
                ImagePath = imagePath,
                Text = text,
                TheoryTestPart = test
            };
        }
    }
}