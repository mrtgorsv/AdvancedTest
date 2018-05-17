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
                CreateInitialTest(context, _theorySeq++);
                CreateFirstTest(context, _theorySeq++);
                CreateSecondTest(context, _theorySeq++);
                CreateTotalTest(context);
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

        private void CreateInitialTest(AppDbContext context, int seq)
        {
            int pSeq = 1;
            var theory = new TheoryPart
            {
                Description = "Входное тестирование",
                Name = "Входное тестирование",
                Seq = seq,
                TestTime = 40,
                TheoryTestParts = new List<TheoryTestPart>()
            };
            CreateSingle(theory , pSeq++, "4", "11010010", "11100000", "11001110", "11010000");
            CreateSingle(theory, pSeq++, "1",  "Уменьшился на 20 байт", "Увеличился на 20 байт", "Увеличился на 20 бит","Уменьшился на 20 бит");
            CreateSingle(theory, pSeq++, "3", "F:\\Класс10\\", "F:\\Класс10\\Задания\\Русский\\", "F:\\Класс10\\Задания", "F:\\");
            CreateSingle(theory, pSeq++, "2", "111102", "110002","111002", "1011002");
            CreateSingle(theory, pSeq++, "3", "69D3", "CABDAC", "D3A6", "6A3D");
            CreateSingle(theory, pSeq++, "1", null, null,null, null);
            CreateSingle(theory, pSeq++, "4", "YWV", "ZXY", "XZZ", "YWY");
            CreateSingle(theory, pSeq++, "2", "х = 10, у = 16", "х = 6, у = 12", "х = 13, у = 9", "х = 13, у = 19");
            CreateSingle(theory, pSeq++, "2", "X V Y V ¬Z", "¬X V ¬Y V Z", "¬X Λ ¬Y Λ Z", "X Λ Y Λ Z");
            CreateSingle(theory, pSeq++, "4", "¬A Λ ¬B V C V D", "¬A Λ ¬B Λ ¬C V D", "A Λ B Λ C Λ D", "¬A Λ B Λ ¬C V D");
            CreateSingle(theory, pSeq++, "1", "5", "12", "20", "4");
            CreateSingle(theory, pSeq++, "4", null, null, null, null);
            CreateSingle(theory, pSeq++, "3", "3", "2", "1", "14");
            CreateSingle(theory, pSeq++, "2", "Синий", "Серый", "Зеленый", "Красный");
            CreateSingle(theory, pSeq++, "2", "Анатолий", "Римма", "Дмитрий", "Светлана");
            CreateSingle(theory, pSeq++, "1", "180 байт", "160 байт", "140 байт", "1600 бит");
            CreateSingle(theory, pSeq++, "4", "B[100]", "B[1]", "B[50]", "B[51]");
            CreateSingle(theory, pSeq++, "2", "4", "3", "1", "2");

            CreateCustom(theory, pSeq++, "2");
            CreateCustom(theory, pSeq++, "17");
            CreateCustom(theory, pSeq++, "12112");
            CreateCustom(theory, pSeq++, "128.192.136.0");
            CreateCustom(theory, pSeq++, "1020");
            CreateCustom(theory, pSeq++, "21000");
            CreateCustom(theory, pSeq++, "ВБ,ПА,СМ,УИ");
            CreateCustom(theory, pSeq++, "BAABAAC");
            CreateCustom(theory, pSeq++, "2341");

            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateFirstTest(AppDbContext context, int seq)
        {
            int pSeq = 1;

            var theory = new TheoryPart
            {
                Description = "Глава 1. Транспортирование информации",
                Name = "Глава 1. Транспортирование информации",
                Seq = seq,
                TestTime = 20,
                TheoryTestParts = new List<TheoryTestPart>()
            };
            CreateSingle(theory , pSeq++ , "4", "Html", "ftp", "www", "http");
            CreateSingle(theory , pSeq++ , "1", "www.mail.ru", "mail.ru/chair806", "chair806", "ftp.html" , "html");
            CreateMultiply(theory , pSeq++ , "1" , "Cеть, объединяющая компьютерные сети таким образом, чтобы пользователи и компьютеры, где бы они ни находились, могли взаимодействовать со всеми остальными участниками сети",
                "Сеть, объединяющая несколько компьютеров и дает возможность пользователям совместно использовать компьютерные ресурсы, а также подключенных в сети периферийных устройств",
                "Сеть, объединяющая компьютеры и дает возможность пользователям совместно использовать информационные ресурсы в разных частях города",
                "Сеть, объединяющая компьютеры и принадлежащая одной организации, дает возможность пользователям совместно использовать компьютерные ресурсы в соответствии с правилами этой организации");
            CreateMultiply(theory, pSeq++, "123", "Коммутаторы", "Маршрутизаторы", "Мосты", "Селекторы");
            CreateCustom(theory, pSeq++, "500");
            CreateCustom(theory, pSeq++, "240");
            CreateCustom(theory, pSeq++, "512");
            CreateCustom(theory, pSeq++, "2316745");
            CreateSingle(theory, pSeq++, "1", "http", "ftp", "ospf", "dhcp");
            CreateCustom(theory, pSeq++, "транспортировка данных");
            CreateCustom(theory, pSeq++, "маршрутизация");
            CreateCustom(theory, pSeq++, "tcp");
            CreateCustom(theory, pSeq++, "smtp");
            CreateCustom(theory, pSeq++, "ftp");

            theory.TheoryDocuments = new List<TheoryDocument>
            {
                new TheoryDocument
                {
                    TheoryPart = theory,
                    IsVisible = true,
                    Name = "Транспортирование информации(версия 1)",
                    Seq = 1
                },
                new TheoryDocument
                {
                    TheoryPart = theory,
                    IsVisible = true,
                    Name = "Транспортирование информации(версия 2)",
                    Seq = 2
                }
            };
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateSecondTest(AppDbContext context, int seq)
        {
            int pSeq = 1;

            var theory = new TheoryPart
            {
                Description = "Глава 2. Хранение информации",
                Name = "Глава 2. Хранение информации",
                Seq = seq,
                TestTime = 20,
                TheoryTestParts = new List<TheoryTestPart>()
            };

            CreateСompare(theory , pSeq++ , "321","Определение данных;Хранение данных;Выборка данных" , "Запрашивание существующих данных пользователями и извлечение данных для использования прикладными программами" ,
                "Вставка новых данных в уже существующие сруктуры данных, обновление данных в существующих структурах, удаление данных из существующих структур",
                "Определение новых структур данных для базы данных, удаление ненужных структур из базы, модификация структуры существующих данных");

            CreateСompare(theory, pSeq++, "231", "Концептуальный уровень;Физический уровень;Логический уровень", "Создание фактического хранения данных в физической памяти ЭВМ.",
                "Создание модели данных на основе инфологической модели предметной области",
                "Системное описание предметной области на основе информационных потребностей пользователей информационной системы");

            CreateSingle(theory, pSeq++, "1", "код_спортсмена + код_дистанции", "код_спортсмена + время", "код_спортсмена", "код_спортсмена + дата_соревнований + время" , "код_спортсмена + код_дистанции + время");
            CreateSingle(theory, pSeq++, "5", "1,5,6,3,2,4", "1,5,3,6,4,2", "5,1,3,4,6,2", "5,1,3,6,2,4" , "5,1,3,6,4,2");
            CreateMultiply(theory, pSeq++, "23", "Математическая модель", "Модель Чена", "ER-диаграмма");
            CreateMultiply(theory, pSeq++, "123", "Многие ко многим", "один ко многим", "Один к одному" , "Бинарные");
            CreateSingle(theory, pSeq++, "1", "Иерархической", "Сетевой", "Реляционной");
            CreateSingle(theory, pSeq++, "1", "Сетевой", "Иерархической", "Реляционной");
            CreateSingle(theory, pSeq++, "1", "Реляционной", "Сетевой", "Иерархической");
            CreateSingle(theory, pSeq++, "5", "школа (по убыванию)", "телефон (по убыванию)", "школа (по возрастанию)", "директор (по убыванию)" , "телефон (по возрастанию)");
            CreateSingle(theory, pSeq++, "1", "2, 3, 5, 6", "5, 6", "1, 2, 5, 6", "1, 3, 4, 6" , "1, 2");
            CreateCustom(theory, pSeq++, "база данных");
            CreateCustom(theory, pSeq++, "аппаратная");
            CreateCustom(theory, pSeq++, "Репозиторий");
            CreateCustom(theory, pSeq++, "физический");

            theory.TheoryDocuments = new List<TheoryDocument>
            {
                new TheoryDocument
                {
                    TheoryPart = theory,
                    IsVisible = true,
                    Name = "Хранение данных(версия 1)",
                    Seq = 1
                },
                new TheoryDocument
                {
                    TheoryPart = theory,
                    IsVisible = true,
                    Name = "Хранение данных(версия 2)",
                    Seq = 2
                },
                new TheoryDocument
                {
                    TheoryPart = theory,
                    IsVisible = true,
                    Name = "Хранение данных(версия 3)",
                    Seq = 3
                }
            };
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateTotalTest(AppDbContext context)
        {
            var totalTest = new TheoryPart
            {
                Description = "Итоговое тестирование",
                Name = "Итоговое тестирование",
                Seq = 19,
                TestTime = 40,
                IsLast = true
            };
            context.TheoryParts.Add(totalTest);
            context.SaveChanges();
        }

        private void CreateSingle(TheoryPart theory, int seq, string correctAnswer, params string[] answers)
        {
            theory.TheoryTestParts.Add(CreatePart(theory, seq, correctAnswer, TestPartType.SingleChoice, answers));
        }
        private void CreateMultiply(TheoryPart theory, int seq, string correctAnswer, params string[] answers)
        {
            theory.TheoryTestParts.Add(CreatePart(theory, seq, correctAnswer, TestPartType.MultiplyChoice, answers));
        }
        private void CreateCustom(TheoryPart theory, int seq, string correctAnswer)
        {
            theory.TheoryTestParts.Add(CreatePart(theory, seq, correctAnswer, TestPartType.Manual , new string[0]));
        }
        private void CreateСompare(TheoryPart theory, int seq, string correctAnswer , string options, params string[] answers)
        {
            theory.TheoryTestParts.Add(CreatePart(theory, seq, correctAnswer, TestPartType.Compare, answers , options: options));
        }

        private TheoryTestPart CreatePart(TheoryPart theory, int seq, string correntAnswer,
            TestPartType testType, string[] answers , string options = null)
        {
            var testPart = new TheoryTestPart
            {
                TheoryPart = theory,
                Seq = seq,
                TestType = testType,
                CorrectAnswer = correntAnswer,
                Answers = new List<TheoryTestPartAnswer>()
            };

            for (int i = 0; i < answers.Length; i++)
            {
                testPart.Answers.Add(CreateAnswer(testPart, answers[i], i + 1 , options));
            }

            return testPart;
        }

        private TheoryTestPartAnswer CreateAnswer(TheoryTestPart test, string text, int seq, string options = null)
        {
            return new TheoryTestPartAnswer
            {
                AnswerNumber = seq,
                Text = text,
                TheoryTestPart = test,
                Options = options
            };
        }
    }
}