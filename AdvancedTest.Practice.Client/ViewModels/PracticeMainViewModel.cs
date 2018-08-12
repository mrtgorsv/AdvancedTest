using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using AdvancedTest.Common.Event;
using AdvancedTest.Common.Utils;
using AdvancedTest.Common.ViewModels;
using AdvancedTest.Common.ViewModels.Answer;
using AdvancedTest.Common.ViewModels.Practice;
using AdvancedTest.Common.ViewModels.Theory;
using AdvancedTest.Data.Enum;
using AdvancedTest.Practice.Client.Properties;
using AdvancedTest.Practice.Client.Utils;
using AdvancedTest.Practice.Client.ViewModels.Practice;
using AdvancedTest.Service.Services.Interface;

namespace AdvancedTest.Practice.Client.ViewModels
{
    public partial class PracticeMainViewModel : MainViewModel
    {
        public PracticeMainViewModel(ITheoryService theoryService, ViewModelLocator locator, IUserService userService,
            ISecurityManager securityManager, IDocumentService documentService) : base(theoryService, locator,
            userService, securityManager, documentService)
        {
            //
        }

        public override void LoadDataSource()
        {
            TheorySections = new ObservableCollection<TheorySectionViewModel>();
            LoadPractice();
        }

        public override ContentDataTemplateSelector ContentSelector => new PracticeDataTemplateSelector();

        private TheorySectionViewModel CreatePracticeSection()
        {
            return new TheorySectionViewModel
            {
                CurrentSectionId = -1,
                Name = "Контрольные работы",
                Seq = -1,
                IsVisible = true
            };
        }

        public override string Title
        {
            get { return "Система адаптивного обучения. Практика"; }
        }

        #region Practice

        private void LoadPractice()
        {
            TheorySections.Clear();
            TheorySections.Add(CreatePracticeSection());
            var theoryViewModel = new TheoryViewModel
            {
                CurrentTheoryId = -1,
                Name = "Контрольные работы",
                Seq = -1,
                TestTime = 40,
                IsVisible = true
            };

            var wordPracticeViewModel = CreateWordPracticeViewModel();
            var excelPracticeViewModel = CreateExcelPracticeViewModel();
            wordPracticeViewModel.CurrentTheory = theoryViewModel;
            excelPracticeViewModel.CurrentTheory = theoryViewModel;
            theoryViewModel.TheoryPartElements =
                new ObservableCollection<TheoryPartElementViewModel> {wordPracticeViewModel, excelPracticeViewModel};
            TheoryParts.Insert(0, theoryViewModel);
            TheorySections.First().TheoryParts.Add(theoryViewModel);
        }

        private WordPracticeViewModel CreateWordPracticeViewModel(bool withDocs = false)
        {
            var practiceViewModel = new WordPracticeViewModel
            {
                Name = "Контрольная работа №1. Microsoft Word",
                DocumentPath = "1\\info",
                RulesDescription = "Правила оформления докладов",
                Description =
                    "В данной контрольной работе вам необходимо выполнить форматирование текста по правилам в документе Microsoft Word.",
                IsVisible = true
            };

            if (withDocs)
            {
                practiceViewModel.StartDocument = Resources.start;
                practiceViewModel.ResultDocument = Resources.result;
            }

            return practiceViewModel;
        }

        private PracticeViewModel CreateExcelPracticeViewModel()
        {
            var practiceViewModel = new ExcelPracticeViewModel
            {
                Name = "Контрольная работа №2. Microsoft Excel",
                Description =
                    "В данной контрольной работе вам необходимо выполнить расчет по формулам в Microsoft Office Excel. Выберете вариант и приступайте к решению",
                IsVisible = true
            };
            AddExcelOptions(practiceViewModel);
            return practiceViewModel;
        }

        private void AddExcelOptions(ExcelPracticeViewModel practiceViewModel)
        {
            practiceViewModel.Options = new ObservableCollection<AnswerOptionViewModel>
            {
                new AnswerOptionViewModel("Задание 1", 0,
                    "1. Чему равна наибольшая сумма баллов по двум предметам среди учащихся Подгорного района? \n \t" +
                    "Ответ на этот вопрос запишите в ячейку G1 таблицы. \n" +
                    "Для промежуточного подсчета использовать ячейки E2-E264. \n " +
                    "2. Сколько процентов от общего числа участников составили ученики Подгорного района?  \n \t" +
                    "Ответ на этот вопрос с точностью до одного знака после запятой запишите в ячейку G2 таблицы. \n" +
                    "Для промежуточного расчета использовать ячейки F2-F264. Использовать ячейки E2-E264 нельзя."),
                new AnswerOptionViewModel("Задание 2", 1,
                    "1. Какой была средняя температура воздуха в весенние месяцы (март, апрель, май)?  \n \t" +
                    "Ответ на этот вопрос запишите в ячейку H2 таблицы. \n " +
                    "2. Какое среднее количество осадков выпадало за сутки в те дни года, когда дул южный (Ю) ветер? \n \t" +
                    "Ответ на этот вопрос запишите в ячейку H3 таблицы."),
                new AnswerOptionViewModel("Задание 3", 2,
                    "1. Какой был средний балл у учащихся, сдавших экзамен 14 июня? \n \t" +
                    "Ответ на этот вопрос запишите в ячейкуH2 таблицы. \n " +
                    "2. Какой был средний балл у учащихся, сдававших экзамен по информатике (в любой день)? \n \t" +
                    "Ответ на этот вопрос запишите в ячейку H3 таблицы с точностью не менее двух знаков после запятой."),
                new AnswerOptionViewModel("Задание 4", 3,
                    "1. Сколько продуктов в таблице имеют содержание углеводов вдвое больше, чем содержание белков, и при этом их калорийность не больше 400 Ккал?  \n\t" +
                    "Ответ на этот вопрос запишите в ячейку H2 таблицы. \n" +
                    "Для промежуточных расчетов используйте ячейки F2:F1001. \n " +
                    "2. Какова средняя калорийность продуктов, у которых содержание белков более 15 г, а содержание жиров меньше 10 г?  \n \t" +
                    "Ответ на этот вопрос запишите в ячейку H3 таблицы с точностью не менее двух знаков после запятой. \n" +
                    "Для промежуточных расчетов используйте ячейки G2:G1001."),
                new AnswerOptionViewModel("Задание 5", 4,
                    "1. Сколько участников тестирования получили по русскому языку, физике и информатике в сумме более 200 баллов? \n\t" +
                    "Ответ на этот вопрос запишите в ячейку H2 таблицы. \n " +
                    "2. Каков средний балл по математике у участников, которые набрали по информатике более 60 баллов? \n \t" +
                    "Ответ на этот вопрос запишите в ячейку H3 таблицы с точностью не менее двух знаков после запятой."),
                new AnswerOptionViewModel("Задание 6", 5,
                    "1. Какое количество учащихся получило хотя бы одну тройку? \n\t" +
                    "Ответ на этот вопрос запишите в ячейку I2 таблицы. \n " +
                    "2. Для группы учащихся, которые получили хотя бы одну тройку, посчитайте средний балл, полученный ими на экзамене по алгебре.  \n \t" +
                    "Ответ на этот вопрос запишите в ячейку I3 таблицы с точностью не менее двух знаков после запятой.\n" +
                    "Для промежуточных расчетов использовать ячейки H2:H1001 и G2:G1001."),
                new AnswerOptionViewModel("Задание 7", 6,
                    "1. Сколько продуктов в таблице содержат меньше 5 г жиров и меньше 5 г белков? \n \t" +
                    "Запишите число этих продуктов в ячейку H2 таблицы. \n " +
                    "2. Какова средняя калорийность продуктов с содержанием жиров 0 г? \n \t" +
                    "Ответ на этот вопрос запишите в ячейку H3 таблицы с точностью не менее двух знаков после запятой. \n " +
                    "Для промежуточных расчетов использовать ячейки F2:F1001."),
                new AnswerOptionViewModel("Задание 8", 7,
                    "1. Каково среднее арифметическое всех оценок, выставленных за экзамены 12 июня? \n\t" +
                    "Ответ на этот вопрос запишите в ячейку H2 таблицы. \n " +
                    "2. Каково среднее арифметическое всех оценок, выставленных по географии во все дни? \n\t" +
                    "Ответ на этот вопрос запишите в ячейку H3 таблицы . \n"),
                new AnswerOptionViewModel("Задание 9", 8,
                    "1. Какое среднее количество осадков выпало за сутки в весенние месяцы (март, апрель, май)? \n \t" +
                    "Ответ на этот вопрос запишите в ячейку H2 таблицы. \n " +
                    "2. Какая средняя скорость ветра была в те дни года, когда дул юго- западный (ЮГ) ветер? \n \t" +
                    "Ответ на этот вопрос запишите в ячейку H3 таблицы .")
            };
        }

        #endregion

        public override void ShowPractice(PracticeViewModel practice)
        {
            PracticeViewModel practiceViewModel;
            switch (practice.PracticeType)
            {
                case PracticeType.Excel:
                    practiceViewModel = CreateExcelPracticeViewModel();
                    practiceViewModel.SetTestTime(TimeSpan.Zero);
                    break;
                case PracticeType.Word:
                    practiceViewModel = CreateWordPracticeViewModel();
                    practiceViewModel.SetTestTime(TimeSpan.FromMinutes(40));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            practice.TestCompleted += OnPracticeCompleted;
            practiceViewModel.Name = practice.Name;
            SelectedElement = practice;
        }

        public override void ShowWordPractice(PracticeViewModel wordPracticeListItem)
        {
            var practice = CreateWordPracticeViewModel();
            practice.TestCompleted += OnPracticeCompleted;
            practice.Name = wordPracticeListItem.Name;
            practice.SetTestTime(TimeSpan.FromMinutes(40));
            SelectedElement = practice;
        }

        public override void ShowExcelPractice(PracticeViewModel practiceListItem)
        {
            var practice = CreateExcelPracticeViewModel();
            practice.TestCompleted += OnPracticeCompleted;
            practice.Name = practiceListItem.Name;
            practice.SetTestTime(TimeSpan.Zero);
            SelectedElement = practice;
        }

        private void OnPracticeCompleted(object sender, TestCompletedEventArgs args)
        {
            if (!(SelectedElement is PracticeViewModel practice))
            {
                return;
            }

            ShowPracticeMessage(args);
            practice.TestCompleted -= OnPracticeCompleted;
            practice.Dispose();
        }

        private void DoSyncAction(Action action)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(action));
        }

        private void ShowPracticeMessage(TestCompletedEventArgs args)
        {
            if (args.Complete)
                ShowMessage(args.Message);
            else
                ShowErrorPracticeMessage(args.Error);
        }

        private void ShowErrorPracticeMessage(string error)
        {
            ShowMessage($"При проверке работы возникла ошибка: {error}");
        }
    }
}