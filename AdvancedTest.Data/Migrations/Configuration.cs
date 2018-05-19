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
                CreateFirstTheory(context, _theorySeq++);
                CreateSecondTheory(context, _theorySeq++);
                CreateThirdTheory(context, _theorySeq++);
                CreateFourTheory(context, _theorySeq++);
                CreateFiveTheory(context, _theorySeq++);
                CreateSixTheory(context, _theorySeq++);
                CreateSevenTheory(context, _theorySeq++);
                CreateEighthTheory(context, _theorySeq++);
                CreateNinthTheory(context, _theorySeq++);
                CreateTenTheory(context, _theorySeq++);
                CreateEleventhTheory(context, _theorySeq++);
                CreateTwelfthTheory(context, _theorySeq++);
                CreateThirteenthTheory(context, _theorySeq++);
                CreateFourteenthTheory(context, _theorySeq++);
                CreateFifteenthTheory(context, _theorySeq++);
                CreateSixteenTheory(context, _theorySeq++);
                CreateSeventeenTheory(context, _theorySeq++);
                CreateEighthteenTheory(context, _theorySeq++);
                CreateTotalTest(context, _theorySeq++);
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
            TheoryPart theory = CreateTheory("Входное тестирование", seq, 40);
            theory.IsInitial = true;
            CreateSingle(theory, pSeq++, "4", "11010010", "11100000", "11001110", "11010000");
            CreateSingle(theory, pSeq++, "1", "Уменьшился на 20 байт", "Увеличился на 20 байт", "Увеличился на 20 бит",
                "Уменьшился на 20 бит");
            CreateSingle(theory, pSeq++, "3", "F:\\Класс10\\", "F:\\Класс10\\Задания\\Русский\\",
                "F:\\Класс10\\Задания", "F:\\");
            CreateSingle(theory, pSeq++, "2", "111102", "110002", "111002", "1011002");
            CreateSingle(theory, pSeq++, "3", "69D3", "CABDAC", "D3A6", "6A3D");
            CreateSingle(theory, pSeq++, "1", null, null, null, null);
            CreateSingle(theory, pSeq++, "4", "YWV", "ZXY", "XZZ", "YWY");
            CreateSingle(theory, pSeq++, "2", "х = 10, у = 16", "х = 6, у = 12", "х = 13, у = 9", "х = 13, у = 19");
            CreateSingle(theory, pSeq++, "2", "X V Y V ¬Z", "¬X V ¬Y V Z", "¬X Λ ¬Y Λ Z", "X Λ Y Λ Z");
            CreateSingle(theory, pSeq++, "4", "¬A Λ ¬B V C V D", "¬A Λ ¬B Λ ¬C V D", "A Λ B Λ C Λ D",
                "¬A Λ B Λ ¬C V D");
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
            CreateCustom(theory, pSeq, "2341");

            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateTotalTest(AppDbContext context, int seq)
        {
            TheoryPart theory = CreateTheory("Итоговое тестирование", seq, 40);
            theory.IsLast = true;
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        #region Theory

        private void CreateFirstTheory(AppDbContext context, int seq)
        {
            int pSeq = 1;

            TheoryPart theory = CreateTheory("Глава 1. Транспортирование информации", seq);

            CreateSingle(theory, pSeq++, "4", "Html", "ftp", "www", "http");
            CreateSingle(theory, pSeq++, "1", "www.mail.ru", "mail.ru/chair806", "chair806", "ftp.html", "html");
            CreateMultiply(theory, pSeq++, "1",
                "Cеть, объединяющая компьютерные сети таким образом, чтобы пользователи и компьютеры, где бы они ни находились, могли взаимодействовать со всеми остальными участниками сети",
                "Сеть, объединяющая несколько компьютеров и дает возможность пользователям совместно использовать компьютерные ресурсы, а также подключенных в сети периферийных устройств",
                "Сеть, объединяющая компьютеры и дает возможность пользователям совместно использовать информационные ресурсы в разных частях города",
                "Сеть, объединяющая компьютеры и принадлежащая одной организации, дает возможность пользователям совместно использовать компьютерные ресурсы в соответствии с правилами этой организации");
            CreateMultiply(theory, pSeq++, "3",
                "Cеть, объединяющая компьютерные сети таким образом, чтобы пользователи и компьютеры, где бы они ни находились, могли взаимодействовать со всеми остальными участниками сети",
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
            CreateCustom(theory, pSeq, "ftp");

            CreateDocuments(theory, 2);
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateSecondTheory(AppDbContext context, int seq)
        {
            int pSeq = 1;

            TheoryPart theory = CreateTheory("Глава 2. Хранение информации", seq);
            CreateСompare(theory, pSeq++, "321", "Определение данных;Хранение данных;Выборка данных",
                "Запрашивание существующих данных пользователями и извлечение данных для использования прикладными программами",
                "Вставка новых данных в уже существующие сруктуры данных, обновление данных в существующих структурах, удаление данных из существующих структур",
                "Определение новых структур данных для базы данных, удаление ненужных структур из базы, модификация структуры существующих данных");

            CreateСompare(theory, pSeq++, "231", "Концептуальный уровень;Физический уровень;Логический уровень",
                "Создание фактического хранения данных в физической памяти ЭВМ.",
                "Создание модели данных на основе инфологической модели предметной области",
                "Системное описание предметной области на основе информационных потребностей пользователей информационной системы");

            CreateSingle(theory, pSeq++, "1", "код_спортсмена + код_дистанции", "код_спортсмена + время",
                "код_спортсмена", "код_спортсмена + дата_соревнований + время",
                "код_спортсмена + код_дистанции + время");
            CreateSingle(theory, pSeq++, "5", "1,5,6,3,2,4", "1,5,3,6,4,2", "5,1,3,4,6,2", "5,1,3,6,2,4",
                "5,1,3,6,4,2");
            CreateMultiply(theory, pSeq++, "23", "Математическая модель", "Модель Чена", "ER-диаграмма");
            CreateMultiply(theory, pSeq++, "123", "Многие ко многим", "один ко многим", "Один к одному", "Бинарные");
            CreateSingle(theory, pSeq++, "1", "Иерархической", "Сетевой", "Реляционной");
            CreateSingle(theory, pSeq++, "1", "Сетевой", "Иерархической", "Реляционной");
            CreateSingle(theory, pSeq++, "1", "Реляционной", "Сетевой", "Иерархической");
            CreateSingle(theory, pSeq++, "5", "школа (по убыванию)", "телефон (по убыванию)", "школа (по возрастанию)",
                "директор (по убыванию)", "телефон (по возрастанию)");
            CreateSingle(theory, pSeq++, "1", "2, 3, 5, 6", "5, 6", "1, 2, 5, 6", "1, 3, 4, 6", "1, 2");
            CreateCustom(theory, pSeq++, "база данных");
            CreateCustom(theory, pSeq++, "аппаратная");
            CreateCustom(theory, pSeq++, "Репозиторий");
            CreateCustom(theory, pSeq, "физический");

            CreateDocuments(theory, 3);

            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateThirdTheory(AppDbContext context, int seq)
        {
            int pSeq = 1;
            TheoryPart theory = CreateTheory("Глава 3. Извлечение, представление и использование информации", seq);

            CreateСompare(theory, pSeq++, "231", "Предмет;Класс;Объект", "множество книг и периодических изданий",
                "название книги, автор, год издания ",
                "конкретная книга \"Информационные технологии\" автора Советова Б.Я.издана в 2003 году");
            CreateMultiply(theory, pSeq++, "235", "Концептуальном", "Информационном", "Формальном", "Маcштабируемом",
                "Реальном");
            CreateCustom(theory, pSeq++, "методы обогащения информации");
            CreateCustom(theory, pSeq++, "наследование");
            CreateCustom(theory, pSeq++, "семантическое обогащение");
            CreateCustom(theory, pSeq++, "класс");
            CreateCustom(theory, pSeq++, "объект");
            CreateCustom(theory, pSeq, "формы исследования данных");

            CreateDocuments(theory, 3);

            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateFourTheory(AppDbContext context, int seq)
        {
            int pSeq = 1;
            TheoryPart theory = CreateTheory("Глава 4. Обработка информации", seq);

            CreateСompare(theory, pSeq++, "21",
                "Структурирование, поиск, кодирование;Вычисления по формулам, логическое рассуждение, исследование моделей",
                "Обработка, связанная с получением нового содержания",
                "Обработка, связанная с изменением формы представления информации");
            CreateСompare(theory, pSeq++, "312", "MIMD;SIMD;MISD", "Наличие конвейерных ЭВМ", "Наличие мультобработки",
                "Наличие центрального контроллера, управляющего рядом одинаковых процессоров");

            CreateSingle(theory, pSeq++, "4", "Принятие решений в условиях многокритериальности",
                "Принятие решений в условиях неопределенности", "Принятие решений в условиях риска",
                "Принятие решений в условиях определенности");
            CreateSingle(theory, pSeq++, "3", "Принятие решений в условиях многокритериальности",
                "Принятие решений в условиях определенности", "Принятие решений в условиях неопределенности",
                "Принятие решений в условиях риска");
            CreateSingle(theory, pSeq++, "4", "Принятие решений в условиях неопределенности",
                "Принятие решений в условиях определенности", "Принятие решений в условиях риска",
                "Принятие решений в условиях многокритериальности");

            CreateMultiply(theory, pSeq++, "1245", "доказательство «от противного»", "структурная индукция",
                "агрегирование", "эвристика", "машинная аналогия");
            CreateMultiply(theory, pSeq++, "123", "ситуационное моделирование", "обобщение", "прогнозирование",
                "математическое моделирование");

            CreateCustom(theory, pSeq++, "обработка");
            CreateCustom(theory, pSeq++, "процессор");
            CreateCustom(theory, pSeq, "структурирование");

            CreateDocuments(theory, 3);
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateFiveTheory(AppDbContext context, int seq)
        {
            int pSeq = 1;
            TheoryPart theory = CreateTheory("Глава 5. Программные и технические средства", seq);

            CreateCustom(theory, pSeq++, "система управления базами данных");
            CreateCustom(theory, pSeq++, "реализация языка");
            CreateCustom(theory, pSeq++, "язык программирования");

            CreateSingle(theory, pSeq++, "1", "Верно", "Неверно");
            CreateSingle(theory, pSeq++, "1", "Верно", "Неверно");
            CreateSingle(theory, pSeq++, "1", "Верно", "Неверно");

            CreateСompare(theory, pSeq++, "132", "MIMD;SISD;MISD", "Наличие мультобработки", "Наличие конвейерных ЭВМ",
                "Наличие архитектуры фон Неймана + КЭШ + память + конвейеризация");

            CreateSingle(theory, pSeq++, "2", "процессорные ансамбли", "ассоциативные процессоры",
                "конвейерные процессоры", "матричные процессоры");
            CreateSingle(theory, pSeq++, "4", "процессорные ансамбли", "ассоциативные процессоры",
                "матричные процессоры", "конвейерные процессоры");
            CreateSingle(theory, pSeq, "3", "ассоциативные процессоры", "процессорные ансамбли", "матричные процессоры",
                "конвейерные процессоры");

            CreateDocuments(theory, 3);
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateSixTheory(AppDbContext context, int seq)
        {
            int pSeq = 1;
            TheoryPart theory = CreateTheory("Глава 6. Методические средства ИТ", seq);

            CreateCustom(theory, pSeq++, "Унификация");
            CreateCustom(theory, pSeq++, "ISO/OSI");

            CreateSingle(theory, pSeq++, "1", "Верно", "Неверно");
            CreateSingle(theory, pSeq++, "1", "Верно", "Неверно");
            CreateSingle(theory, pSeq++, "1", "Верно", "Неверно");
            CreateSingle(theory, pSeq++, "1", "Верно", "Неверно");
            CreateSingle(theory, pSeq++, "1", "Верно", "Неверно");
            CreateSingle(theory, pSeq, "1", "Верно", "Неверно");

            CreateDocuments(theory, 3);
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateSevenTheory(AppDbContext context, int seq)
        {
            int pSeq = 1;
            TheoryPart theory = CreateTheory("Глава 7. Мультимедиа технологии", seq);

            CreateSingle(theory, pSeq++, "1",
                "Объект, отдельные элементы которого наследуют свойства родительских структур",
                "Геометрическая фигура, в которой один и тот же мотив повторяется в последовательно увеличивающемся масштабе",
                "Целая размерность");
            CreateSingle(theory, pSeq++, "1", "Провести пространственную дискретизацию",
                "Провести качественную дискретизацию", "Провести временную дискретизацию",
                "Провести количественную дискретизацию");
            CreateSingle(theory, pSeq++, "4", "(r1+r2* g1+g2*b1+b2)", "(r1+r2+ g1+g2+b1+b2)",
                "(r1+r2+r3, g1+g2+g3, b1+b2+b3)", "(r1+r2, g1+g2, b1+b2)");
            CreateSingle(theory, pSeq++, "4", "SSIM", "HSB", "RGB", "CMYK", "HSV");
            CreateMultiply(theory, pSeq++, "14", "HSV", "CMYK", "HSB", "RGB");
            CreateMultiply(theory, pSeq++, "12", "576 - количество строк", "720 - количество точек в строке",
                "576 - количество точек в строке", "720 - количество строк");
            CreateSingle(theory, pSeq++, "3", "биты", "Гц", "пиксели", "байты");
            CreateSingle(theory, pSeq++, "3",
                "изображение разбивается точки, и каждой точке присваивается свой код цвета",
                "значения параметров описываются интервальными величинами, заданными интервалом, образованным минимальным и максимально возможными значениями параметра",
                "непрерывная зависимость амплитуды сигнала от времени заменяется на дискретную последовательность уровней громкости");
            CreateSingle(theory, pSeq++, "4", "громкость, тембр, длительность", "громкость, высота, тембр",
                "громкость, высота, ширина, длительность", "громкость, высота, тембр, длительность");
            CreateSingle(theory, pSeq, "1",
                "комплекс аппаратных и программных средств, позволяющих пользователю работать в диалоговом режиме с мультимедийными данными, организованными в единой информационной среде»",
                "программные средства компьютерной графики и фотоизображения",
                "звук, записанный на звуковом носителе, запись и воспроизведение звука, звукозаписывающая и звуковоспроизводящая аппаратура",
                "технология записи, обработки, передачи, хранения и воспроизведения визуального и аудиовизуального материала на мониторах");

            CreateDocuments(theory, 3);
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateEighthTheory(AppDbContext context, int seq)
        {
            int pSeq = 1;
            TheoryPart theory = CreateTheory("Глава 8. ГИС технологии", seq);

            CreateMultiply(theory, pSeq++, "15", "мультимедиа", "корпоративная", "информационно управляющая",
                "автоматизированного проектирования", "геоинформационная");
            CreateSingle(theory, pSeq++, "4", "трехмерной", "фрактальной", "растровой", "векторной");
            CreateSingle(theory, pSeq++, "3", "сканированием", "пикселизацией", "векторизацией", "координацией",
                "форматизацией");
            CreateСompare(theory, pSeq++, "231", "Проблемная ориентация;Целевое назначение;Териториальный охват",
                "По функциональности", "По способу организации географических данных",
                "По проблемно-тематической ориентации");
            CreateMultiply(theory, pSeq++, "25", "статические", "растровые", "географические", "математические",
                "векторные");
            CreateSingle(theory, pSeq++, "1", "векторная", "математическая", "концептуальная", "фрактальная",
                "растровая");
            CreateCustom(theory, pSeq++, "векторном");
            CreateCustom(theory, pSeq++, "матрицу масштаба");
            CreateCustom(theory, pSeq++, "геоинформационная");
            CreateCustom(theory, pSeq, "векторная");

            CreateDocuments(theory, 2);
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateNinthTheory(AppDbContext context, int seq)
        {
            int pSeq = 1;
            TheoryPart theory = CreateTheory("Глава 9. Технологии защиты информации", seq);

            CreateMultiply(theory, pSeq++, "135",
                "устранение ошибок на этапах разработки программно-аппаратных средств",
                "защита конфиденциальной информации при ее хранении на открытых носителях",
                "внесение структурной, временной, информационной и функциональной избыточности компьютерных ресурсов",
                "защита конфиденциальности информации, передаваемой по открытым каналам связи",
                "защита от некорректного использования ресурсов компьютерной системы");
            CreateMultiply(theory, pSeq++, "124", "потеря информации вследствие повреждения носителей",
                "нарушения в работе аппаратных средств из-за повреждения или износа",
                "несанкционированное использование компьютерных ресурсов",
                "ошибки в работе программных средств, не выявленные в процессе отладки",
                "несанкционированный доступ к компьютерным ресурсам");
            CreateMultiply(theory, pSeq++, "24", "логические", "асимметрические", "экспертные", "симметрические",
                "аналитические");
            CreateMultiply(theory, pSeq++, "123", "Использование ключей", "Парольная защита", "Биометрическая защита",
                "Физическая защита данных", "Межсетевой экран");
            CreateSingle(theory, pSeq++, "1", "простые и динамические", "постоянные и временные",
                "параметрические и непараметрические", "статические и динамические", "одноразовые и многоразовые");
            CreateMultiply(theory, pSeq++, "345", "постоянное", "сеансовое", "функциональное", "всеобщее", "временное");
            CreateMultiply(theory, pSeq++, "23", "пароль", "алгоритм", "ключ", "pin-код", "индентификатор");
            CreateCustom(theory, pSeq++, "регламентация");
            CreateCustom(theory, pSeq++, "межсетевой");
            CreateCustom(theory, pSeq, "криптографией");

            CreateDocuments(theory, 2);
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateTenTheory(AppDbContext context, int seq)
        {
            int pSeq = 1;
            TheoryPart theory = CreateTheory("Глава 10. Телекоммуникационные технологии", seq);

            CreateSingle(theory, pSeq++, "1", "цветы & (Тайвань | Хонсю)", "цветы & Тайвань & Хонсю",
                "цветы | Тайвань | Хонсю", "цветы & (остров | Тайвань | Хонсю)");
            CreateCustom(theory, pSeq++, "3214");
            CreateSingle(theory, pSeq++, "2", "mail.ru/chair806", "www.mail.ru", "chair806", "ftp.html", "html");
            CreateCustom(theory, pSeq++, "2");
            CreateCustom(theory, pSeq++, "12000");
            CreateCustom(theory, pSeq++, "512");
            CreateCustom(theory, pSeq++, "клиент-серверная");
            CreateCustom(theory, pSeq++, "Клиент");
            CreateMultiply(theory, pSeq++, "345", "продуктивность", "время реакции", "надежность защиты",
                "минимизация денежных затрат", "гибкость настройки");
            CreateSingle(theory, pSeq, "3", "одноранговая архитектура",
                "архитектура «клиент – сервер» на основе web-технологии", "классическая архитектура «клиент – сервер»");

            CreateDocuments(theory, 2);
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateEleventhTheory(AppDbContext context, int seq)
        {
            int pSeq = 1;
            TheoryPart theory = CreateTheory("Глава 11. Технологии искусственного интеллекта", seq);

            CreateSingle(theory, pSeq++, "1", "дедуктивное рассуждение", "абдуктивное рассуждение",
                "индуктивное рассуждение", "рассуждение по аналогии", "рассуждение по прецеденту");
            CreateSingle(theory, pSeq++, "1", "индуктивное рассуждение", "рассуждение аргументации",
                "абдуктивное рассуждение", "дедуктивное рассуждение");
            CreateSingle(theory, pSeq++, "1", "абдуктивное рассуждение", "индуктивное рассуждение",
                "рассуждение по прецеденту", "рассуждение по аналогии", "дедуктивное рассуждение");
            CreateCustom(theory, pSeq++, "комбинированные");
            CreateCustom(theory, pSeq++, "интегрированная");
            CreateSingle(theory, pSeq++, "2", "учение", "читатель", "книга", "учебник", "смотритель");
            CreateSingle(theory, pSeq++, "4", "бревно", "рубанок", "лесоруб", "столяр", "куст");
            CreateSingle(theory, pSeq, "1", "дом", "озеро", "окно", "дверь", "глазок");

            CreateDocuments(theory, 3);
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateTwelfthTheory(AppDbContext context, int seq)
        {
            int pSeq = 1;
            TheoryPart theory = CreateTheory("Глава 12. Технологии представления знаний", seq);

            CreateMultiply(theory, pSeq++, "34", "достоверность", "актуальность", "понятность", "однородность");
            CreateCustom(theory, pSeq++, "Данные");
            CreateCustom(theory, pSeq++, "декларативные");
            CreateCustom(theory, pSeq++, "глубинные");
            CreateCustom(theory, pSeq++, "представления");
            CreateCustom(theory, pSeq++, "поверхностные");
            CreateCustom(theory, pSeq++, "экспертные знания");
            CreateCustom(theory, pSeq, "открытая");

            CreateDocuments(theory, 3);
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateThirteenthTheory(AppDbContext context, int seq)
        {
            int pSeq = 1;
            TheoryPart theory = CreateTheory("Глава 13. Технологии БД", seq);

            CreateSingle(theory, pSeq++, "1", "Панель для управления базой данных",
                "Панель для изменения параметров проекта C#", "Панель свойств объектов формы",
                "Панель со списком элементов управления");
            CreateSingle(theory, pSeq++, "1", "Подойдет любой", "Приложение Windows Forms", "Консольное приложение",
                "Приложение Silverlight");
            CreateSingle(theory, pSeq++, "1", "Файл - Подключить к обозревателю объектов", "Файл - Создать – Проект",
                "Базы данных - Создать базу данных", "Вид - Другие окна - Окно команд");
            CreateSingle(theory, pSeq++, "1", "Набор текстовых инструкций на языке SQL",
                "Текстовый файл, содержащий данные ячеек таблиц, разделенные пробелами",
                "Двоичный файл со строго прописанной структурой", "Документ Excel");
            CreateSingle(theory, pSeq, "1", "DataGridView", "ListView", "Panel", "GroupBox");

            CreatePracticeDocuments(theory, 3);
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateFourteenthTheory(AppDbContext context, int seq)
        {
            int pSeq = 1;
            TheoryPart theory = CreateTheory("Глава 14. Проигрывание файлов формата WMA, получение метаданных", seq);

            CreateSingle(theory, pSeq++, "3", "Загрузки содержимого файла в память",
                "Обновления атрибутов времени чтения файла", "Отображения стандартного диалога открытия файла",
                "Проигрывания файлов с аудио данными");
            CreateSingle(theory, pSeq++, "1", "MediaPlayer", "MusicPlayer", "MediaPlayerClassic",
                "Windows Media Player");
            CreateSingle(theory, pSeq++, "1", "Year", "FirstPerformer", "Title", "Comment");
            CreateSingle(theory, pSeq++, "2", "Фильтрации списка файлов по имени каталога",
                "Фильтрации списка файлов по расширению файла", "Фильтрации списка файлов по имени файла",
                "Определения начального каталога");
            CreateSingle(theory, pSeq, "1", "Список подключенных сборок .NET", "Список конфигурационных файлов",
                "Дерево файлов проекта", "Набор файлов с параметрами проекта");

            CreatePracticeDocuments(theory, 1);
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateFifteenthTheory(AppDbContext context, int seq)
        {
            int pSeq = 1;
            TheoryPart theory = CreateTheory("Глава 15. ГИС-системы", seq);

            CreateSingle(theory, pSeq++, "1", "Load", "Activated", "Closed", "Shown");
            CreateSingle(theory, pSeq++, "4", "ShowTileGridLines", "ShowTileGridLines", "CanDropMap", "CanDragMap");
            CreateSingle(theory, pSeq++, "1", "Определяет, карты какого производителя будут использоваться",
                "Обеспечивает доступ к информационным сетевым службам",
                "Используется для ограничения функциональности карт",
                "Указывает расположения файла с лицензией картографического сервиса");
            CreateSingle(theory, pSeq++, "2", "Оконные координаты (X, Y)", "Широта и долгота", "Полярные координаты",
                "Тангент и бинормаль");
            CreateSingle(theory, pSeq, "1", "Слой, содержащий набор объектов",
                "Метод, позволяющий создавать программы, занимающие больше памяти, чем установлено в системе",
                "Режим смешивания двух растровых изображений",
                "Специальный графический интерфейс, отображающийся поверх карты");

            CreatePracticeDocuments(theory, 1);
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateSixteenTheory(AppDbContext context, int seq)
        {
            int pSeq = 1;
            TheoryPart theory = CreateTheory("Глава 16. Технологии защиты информации", seq);

            CreateSingle(theory, pSeq++, "1", "Получения хэша MD5", "Обратимого шифрования произвольных данных",
                "Управления пакетами параметров SQL Server", "Доступа к набору глобальных объектов шифрования");
            CreateSingle(theory, pSeq++, "1", "Encoding", "Convert", "string.ToByteArray", "Parse");
            CreateSingle(theory, pSeq++, "4", "Задать базу данных по умолчанию", "Указать способ проверки подлинности",
                "Определить имя пользователя и пароль", "Настроить роли пользователя на сервере");
            CreateSingle(theory, pSeq++, "2", "Вставка", "Резервное копирование", "Обновление", "Выборка");
            CreateSingle(theory, pSeq, "3", "Хэш к паролю, введенному на клиенте, и пароль, хранимый на сервере",
                "Пароль, введенный на клиенте, и хэш, хранимый на сервере",
                "Хэш к паролю, введенному на клиенте, и хэш, хранимый на сервере",
                "Пароль, введенный на клиенте, и пароль, хранимый на сервере");

            CreatePracticeDocuments(theory, 1);
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateSeventeenTheory(AppDbContext context, int seq)
        {
            int pSeq = 1;
            TheoryPart theory = CreateTheory("Глава 17. Телекоммуникационные технологии", seq);

            CreateSingle(theory, pSeq++, "1", "Системные исключения", "Обмен сообщениями", "Объекты синхронизации",
                "Механизмы разделения памяти");
            CreateSingle(theory, pSeq++, "3",
                "Объект, обеспечивающий приложениям координированный доступ к ресурсам компьютера",
                "Общий для процессов сегмент памяти", "Двусторонний канал связи",
                "Объект, ограничивающий количество потоков, которые могут войти в заданный участок кода");
            CreateSingle(theory, pSeq++, "1", "Отвечает (Respond)", "Прослушивается (Listen)", "Открыт (Open)",
                "Закрыт (Closed) ");
            CreateSingle(theory, pSeq++, "2", "Транзакций", "Пакетов", "Массивов байт", "Общей области памяти");
            CreateSingle(theory, pSeq, "4", "Открытия сокета", "Получения блока данных", "Закрытия сокета",
                "Извлечения из очереди ожидающих запросов первого запроса на соединение");

            CreatePracticeDocuments(theory, 1);
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        private void CreateEighthteenTheory(AppDbContext context, int seq)
        {
            int pSeq = 1;
            TheoryPart theory = CreateTheory("Глава 18. Технологии искусственного интеллекта", seq);

            CreateSingle(theory, pSeq++, "2", "Запись двоичного файла на диск",
                "Процесс преобразования какого-либо объекта в поток байтов", "Очистка памяти от накопившегося мусора",
                "Процесс преобразования потока байт в объект");
            CreateSingle(theory, pSeq++, "1", "Преобразования объекта в блок бинарных данных",
                "Форматирования строки текста в двоичном представлении", "Открытия и чтения двоичного файла",
                "Форматирования содержимого таблицы DataGridView");
            CreateSingle(theory, pSeq++, "1", "При помощи полей или методов, объявленных с модификатором public",
                "Через возвращаемое значение DialogResult", "При помощи передачи сообщения в родительское окно",
                "Посредством файлов");
            CreateSingle(theory, pSeq++, "3", "Hide", "Display", "Show", "Present");
            CreateSingle(theory, pSeq, "2", "MessageBoxButtons.YesNoCancel", "MessageBoxButtons.YesNo",
                "MessageBoxButtons.AbortRetryIgnore", "MessageBoxButtons.OKCancel");

            CreatePracticeDocuments(theory, 1);
            context.TheoryParts.Add(theory);
            context.SaveChanges();
        }

        #endregion

        #region Answers

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
            theory.TheoryTestParts.Add(CreatePart(theory, seq, correctAnswer, TestPartType.Manual, new string[0]));
        }

        private void CreateСompare(TheoryPart theory, int seq, string correctAnswer, string options,
            params string[] answers)
        {
            theory.TheoryTestParts.Add(CreatePart(theory, seq, correctAnswer, TestPartType.Compare, answers,
                options: options));
        }

        #endregion

        #region Entities

        private TheoryPart CreateTheory(string name, int seq, int testTime = 20, string description = null)
        {
            return new TheoryPart
            {
                Description = description ?? name,
                Name = name,
                Seq = seq,
                TestTime = testTime,
                TheoryTestParts = new List<TheoryTestPart>()
            };
        }

        private TheoryTestPart CreatePart(TheoryPart theory, int seq, string correntAnswer,
            TestPartType testType, string[] answers, string options = null)
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
                testPart.Answers.Add(CreateAnswer(testPart, answers[i], i + 1, options));
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

        #endregion

        private void CreatePracticeDocuments(TheoryPart theory, int count)
        {
            theory.TheoryDocuments = new List<TheoryDocument>();

            for (int i = 0; i < count; i++)
            {
                theory.TheoryDocuments.Add(
                    new TheoryDocument
                    {
                        TheoryPart = theory,
                        IsVisible = true,
                        IsPractice = true,
                        Name = $"Практическое задание {i + 1}",
                        Seq = i + 1
                    });
            }
        }

        private void CreateDocuments(TheoryPart theory, int count)
        {
            theory.TheoryDocuments = new List<TheoryDocument>();

            for (int i = 0; i < count; i++)
            {
                theory.TheoryDocuments.Add(
                    new TheoryDocument
                    {
                        TheoryPart = theory,
                        IsVisible = true,
                        Name = $"Теория (версия {i + 1})",
                        Seq = i + 1
                    });
            }
        }
    }
}