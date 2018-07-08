using System;
using System.Collections.ObjectModel;
using AdvancedTest.Common.EventArgs;
using AdvancedTest.Common.Utils;
using AdvancedTest.Common.ViewModels;
using AdvancedTest.Common.ViewModels.Answer;
using AdvancedTest.Common.ViewModels.Practice;
using AdvancedTest.Common.ViewModels.Theory;
using AdvancedTest.Practice.Client.ViewModels.Practice;
using AdvancedTest.Properties;
using AdvancedTest.Service.Services.Interface;

namespace AdvancedTest.Practice.Client.ViewModels
{
    public partial class PractiveMainViewModel : MainViewModel
    {
        public PractiveMainViewModel(ITheoryService theoryService, ViewModelLocator locator, IUserService userService,
            ISecurityManager securityManager, IDocumentService documentService) : base(theoryService, locator, userService, securityManager, documentService)
        {
            //
        }

        public override void LoadTheory()
        {
            InsertPractice();
        }

        #region Practice

        private void InsertPractice()
        {
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
            TheorySections[1].TheoryParts.Add(theoryViewModel);
        }

        private WordPracticeViewModel CreateWordPracticeViewModel(bool withDocs = false)
        {
            var practiceViewModel = new WordPracticeViewModel
            {
                Name = "Контрольная работа №1",
                DocumentPath = "1\\info",
                RulesDescription = "Правила оформления докладов",
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
            var practiceViewModel = new PracticeViewModel
            {
                Name = "Контрольная работа №2",
                Description =
                    "В данной контрольной работе вам необходимо выполнить расчет по формулам в Microsoft Office Excel. Выберете вариант и приступайте к решению",
                IsVisible = true,
                Options = new ObservableCollection<AnswerOptionViewModel>
                {
                    new AnswerOptionViewModel("Задание 1", 0,
                        "Выполните задание.\n 1. Чему равна наибольшая сумма баллов по двум предметам среди учащихся Подгорного района? Ответ на этот вопрос запишите в ячейку G1 таблицы. Для промежуточного подсчета использовать ячейки E2-E264. \n 2. Сколько процентов от общего числа участников составили ученики Подгорного района? Ответ на этот вопрос с точностью до одного знака после запятой запишите в ячейку G2 таблицы. Для промежуточного расчета использовать ячейки F2-F264. Использовать ячейки E2-E264 нельзя."),
                    new AnswerOptionViewModel("Задание 2", 1,
                        "Выполните задание:\n 1. Какой была средняя температура воздуха в весенние месяцы (март, апрель, май)? Ответ на этот вопрос запишите в ячейку H2 таблицы. \n 2. Какое среднее количество осадков выпадало за сутки в те дни года, когда дул южный (Ю) ветер? Ответ на этот вопрос запишите в ячейку H3 таблицы."),
                    new AnswerOptionViewModel("Задание 3", 2,
                        "Выполните задание.\n 1. Какой был средний балл у учащихся, сдавших экзамен 14 июня? Ответ на этот вопрос запишите в ячейкуH2 таблицы. \n 2. Какой был средний балл у учащихся, сдававших экзамен по информатике (в любой день)? Ответ на этот вопрос запишите в ячейку H3 таблицы с точностью не менее двух знаков после запятой.")
                }
            };
            return practiceViewModel;
        }

        #endregion

        public override void ShowWordPractice(PracticeViewModel wordPracticeListItem)
        {
            var practice = CreateWordPracticeViewModel();
            practice.TestCompleted += OnWordPracticeCompleted;
            practice.Name = wordPracticeListItem.Name;
            practice.SetTestTime(TimeSpan.FromMinutes(40));
            SelectedElement = practice;
        }

        public override void ShowExcelPractice(PracticeViewModel practiceListItem)
        {
            var practice = CreateExcelPracticeViewModel();
            practice.TestCompleted += OnExcelPracticeCompleted;
            practice.Name = practiceListItem.Name;
            practice.SetTestTime(TimeSpan.Zero);
            SelectedElement = practice;
        }

        public override void OnWordPracticeCompleted(object sender, TestCompletedEventArgs args)
        {
            var practice = (WordPracticeViewModel) SelectedElement;
            practice.TestCompleted -= OnWordPracticeCompleted;
            if (args.Success)
                ShowCompletePracticeMessage(Convert.ToInt32(args.TestResult), args.ElapsedTimeSpan);
            else
                ShowErrorPracticeMessage(args.Error);

            practice.Dispose();
        }

        public override void OnExcelPracticeCompleted(object sender, TestCompletedEventArgs args)
        {
            var practice = (PracticeViewModel) SelectedElement;
            practice.TestCompleted -= OnExcelPracticeCompleted;

            ShowExcelPracticeMessage(args.ElapsedTimeSpan, args.Success);


            practice.Dispose();
        }

        private void ShowExcelPracticeMessage(TimeSpan elapsedTime, bool passed)
        {
            if (!passed)
            {
                ShowMessage("В решение допущена ошибка, попробуйте выполнить задания еще раз");
            }
            else
            {
                var minutes = Convert.ToInt32(elapsedTime.TotalMinutes);
                switch (minutes)
                {
                    case int value when value <= 12:
                    {
                        ShowMessage($"Задания выполнены верно. Время выполнения: {value} минут. Ваша оценка {5}");
                        break;
                    }
                    case int value when value <= 15:
                    {
                        ShowMessage($"Задания выполнены верно. Время выполнения: {value} минут. Ваша оценка {4}");
                        break;
                    }
                    case int value when value <= 18:
                    {
                        ShowMessage($"Задания выполнены верно. Время выполнения: {value} минут. Ваша оценка {3}");
                        break;
                    }
                    case int value when value > 18:
                    {
                        ShowMessage(
                            "Задания выполнены верно. Превышен лимит времени на выполнение заданий, попробуйте выполнить задания еще раз");
                        break;
                    }
                }
            }
        }

        private void ShowCompletePracticeMessage(int result, TimeSpan elapsedTime)
        {
            var testResultString = result.ToString();
            var minutesPassed = true;
            int point;
            var baseMessage = string.Format(Resources.PracticeCompleteTemplateMessage, elapsedTime.ToString("T"));
            if (result == 0)
            {
                point = 5;
                minutesPassed = elapsedTime.TotalMinutes < 20;
            }
            else if (result <= 2)
            {
                point = 4;
                minutesPassed = elapsedTime.TotalMinutes < 23;
            }
            else if (result <= 3)
            {
                point = 3;
                minutesPassed = elapsedTime.TotalMinutes < 25;
            }
            else
            {
                point = 2;
            }

            var pointMessage = string.Format(Resources.PointMessage, minutesPassed ? point : point - 1);
            ShowMessage(
                $"{baseMessage} {(result != 0 ? string.Format(Resources.PracticeFailedTemplateMessage, testResultString) : Resources.PracticeSuccessTemplateMessage)} {pointMessage}");
        }

        private void ShowErrorPracticeMessage(string error)
        {
            ShowMessage($"При проверке работы возникла ошибка: {error}");
        }

    }
}