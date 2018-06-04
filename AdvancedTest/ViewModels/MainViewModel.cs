using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using AdvancedTest.Data.Model;
using AdvancedTest.EventArgs;
using AdvancedTest.Extensions;
using AdvancedTest.Properties;
using AdvancedTest.Service.Services.Interface;
using AdvancedTest.Utils;
using AdvancedTest.ViewModels.Answer;
using AdvancedTest.ViewModels.Base;
using AdvancedTest.ViewModels.Practice;
using AdvancedTest.ViewModels.Test;
using AdvancedTest.ViewModels.Theory;

namespace AdvancedTest.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly ISecurityManager _securityManager;
        private readonly ITheoryService _theoryService;
        private readonly IUserService _userService;
        private readonly IDocumentService _documentService;
        private readonly ViewModelLocator _locator;
        private ViewModelBase _selectedElement;
        private List<UserTheoryDocumentMark> _openedUserDocList;

        public event UserLogoutEventHandler UserLogout;

        public delegate void UserLogoutEventHandler(object sender, System.EventArgs args);

        public MainViewModel(ITheoryService theoryService, ViewModelLocator locator, IUserService userService,
            ISecurityManager securityManager, IDocumentService documentService)
        {
            _theoryService = theoryService;
            _locator = locator;
            _userService = userService;
            _securityManager = securityManager;
            _documentService = documentService;
            LoadTheory();
            InitializeCommands();
        }

        public ViewModelBase SelectedElement
        {
            get => _selectedElement;
            set
            {
                _selectedElement = value;
                OnPropertyChanged(nameof(SelectedElement));
            }
        }

        public int CurrentUserId => _securityManager.CurrentUser.Id;

        public ObservableCollection<TheoryViewModel> TheoryParts { get; set; }

        private void LoadTheory()
        {
            UpdateUserDocs();
            var theoryList = _theoryService.GetTheoryList();
            if (_openedUserDocList.Count == 0 && theoryList.Count > 0)
            {
                var firstTheory = theoryList.First();
                _theoryService.OpenTheory(firstTheory.Id, CurrentUserId);
                UpdateUserDocs();
                ShowWelcomeMessage();
            }
            else
            {
                if (IsTheoryComplete())
                    ShowTotalResult();
                else
                    ShowMessage(Resources.ContinueMessage, Resources.Logo, Brushes.White);
            }

            TheoryParts = new ObservableCollection<TheoryViewModel>(theoryList.Select(CreateTheory));

            InsertPractice();
        }

        private void InsertPractice()
        {
            var theoryViewModel = new TheoryViewModel()
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

        private ExcelPracticeViewModel CreateExcelPracticeViewModel()
        {
            var practiceViewModel = new ExcelPracticeViewModel
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

        private bool IsTheoryComplete()
        {
            return _theoryService.IsTheoryComplete(_securityManager.CurrentUser.Id);
        }

        private TheoryViewModel CreateTheory(TheoryPart theory)
        {
            var theoryViewModel = new TheoryViewModel
            {
                CurrentTheoryId = theory.Id,
                Seq = theory.Seq,
                Name = theory.Name,
                TestTime = theory.TestTime,
                IsVisible = _openedUserDocList.Any(td => !td.DocumentId.HasValue && td.TheoryPartId.Equals(theory.Id))
            };
            theoryViewModel.TheoryPartElements = CreateElements(theory, theoryViewModel);
            return theoryViewModel;
        }

        private void UpdateUserDocs()
        {
            _openedUserDocList = _userService.GetUserDocProgress(CurrentUserId)
                .ToList();
        }

        private ObservableCollection<TheoryPartElementViewModel> CreateElements(TheoryPart theory,
            TheoryViewModel theoryViewModel)
        {
            var elements = new List<TheoryPartElementViewModel>();
            var documents = new List<DocumentViewModel>();
            foreach (var document in theory.TheoryDocuments)
                documents.Add(new DocumentViewModel
                {
                    CurrentTheory = theoryViewModel,
                    Seq = document.Seq > 1 ? document.Seq + 1 : document.Seq,
                    DocumentPath = document.DocumentPath,
                    DocumentId = document.Id,
                    Name = document.Name,
                    IsPractice = document.IsPractice,
                    IsOpened = _openedUserDocList.Any(
                        td => td.DocumentId == document.Id && td.TheoryPartId.Equals(theory.Id)),
                    IsVisible = _openedUserDocList.Any(
                        td => td.DocumentId == document.Id && td.TheoryPartId.Equals(theory.Id))
                });

            elements.AddRange(documents);
            var testViewModel = CreateTest(theoryViewModel);
            testViewModel.IsInitial = theory.IsInitial;
            testViewModel.IsLast = theory.IsLast;
            if (theoryViewModel.IsVisible
                && (_openedUserDocList.Any(td =>
                        td.TheoryPartId.Equals(theoryViewModel.CurrentTheoryId) && td.DocumentId.HasValue) ||
                    elements.Count == 0)
                && documents.Where(d => d.IsPractice).All(d => d.IsOpened))
                testViewModel.IsVisible = true;

            if (elements.Count > 1 && documents.All(d => !d.IsPractice))
                elements.Insert(1, testViewModel);
            else
                elements.Add(testViewModel);

            return new ObservableCollection<TheoryPartElementViewModel>(elements);
        }

        private TestViewModel CreateTest(TheoryViewModel theoryViewModel)
        {
            var testViewModel = _locator.TestViewModel;
            testViewModel.CurrentTheory = theoryViewModel;
            testViewModel.Seq = 2;
            testViewModel.Name = "Тест";
            testViewModel.SetTestTime(TimeSpan.FromMinutes(theoryViewModel.TestTime));
            return testViewModel;
        }

        public void ShowTheoryTest(TestViewModel testListItem)
        {
            var test = CreateTest(testListItem.CurrentTheory);
            test.Name = $" Тест - {testListItem.CurrentTheory.Name}";
            test.CurrentTheoryId = testListItem.CurrentTheory.CurrentTheoryId;
            test.IsInitial = testListItem.IsInitial;
            test.IsLast = testListItem.IsLast;
            test.TestCompleted += OnTestCompleted;
            SelectedElement = test;
        }

        internal void ShowWordPractice(WordPracticeViewModel wordPracticeListItem)
        {
            var practice = CreateWordPracticeViewModel();
            practice.TestCompleted += OnWordPracticeCompleted;
            practice.Name = wordPracticeListItem.Name;
            practice.SetTestTime(TimeSpan.FromMinutes(40));
            SelectedElement = practice;
        }

        internal void ShowExcelPractice(ExcelPracticeViewModel excelPracticeListItem)
        {
            var practice = CreateExcelPracticeViewModel();
            practice.TestCompleted += OnExcelPracticeCompleted;
            practice.Name = excelPracticeListItem.Name;
            practice.SetTestTime(TimeSpan.Zero);
            SelectedElement = practice;
        }

        private void OnWordPracticeCompleted(object sender, TestCompletedEventArgs args)
        {
            var practice = (WordPracticeViewModel) SelectedElement;
            practice.TestCompleted -= OnWordPracticeCompleted;
            if (args.Success)
                ShowCompletePracticeMessage(Convert.ToInt32(args.TestResult), args.ElapsedTimeSpan);
            else
                ShowErrorPracticeMessage(args.Error);

            practice.Dispose();
        }

        private void OnExcelPracticeCompleted(object sender, TestCompletedEventArgs args)
        {
            var practice = (ExcelPracticeViewModel) SelectedElement;
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
                        ShowMessage("Задания выполнены верно. Превышен лимит времени на выполнение заданий, попробуйте выполнить задания еще раз");
                        break;
                    }
                }
            }
        }

        private void OnTestCompleted(object sender, TestCompletedEventArgs args)
        {
            var test = (TestViewModel) SelectedElement;
            test.TestCompleted -= OnTestCompleted;
            if (args.TestResult > 70 || args.TestAttempt >= 3 || test.IsInitial)
                OpentNextTheory(args.TheoryId);
            else
                OpenNextDocument(args.TheoryId);

            if (test.IsLast)
                ShowTotalResult();
            else
                ShowCompleteTestMessage(args.TestResult, args.TestAttempt, test.IsInitial);
        }

        private void ShowTotalResult()
        {
            var viewModel = _locator.UserResultViewModel;
            viewModel.UserId = _securityManager.CurrentUser.Id;
            SelectedElement = viewModel;
        }

        private void OpenNextDocument(int theoryId)
        {
            var nextDocId = _documentService.OpenNextDocument(theoryId, CurrentUserId);

            if (nextDocId <= 0) return;
            var theory = TheoryParts.FirstOrDefault(tp => tp.CurrentTheoryId.Equals(theoryId));
            var nextDoc = theory?.TheoryPartElements.OfType<DocumentViewModel>()
                .FirstOrDefault(d => d.DocumentId.Equals(nextDocId));
            if (nextDoc != null) nextDoc.IsVisible = true;
        }

        private void OpentNextTheory(int theoryId)
        {
            var currentTheory = TheoryParts.FirstOrDefault(tp => tp.CurrentTheoryId.Equals(theoryId));

            if (currentTheory == null) return;
            var nextTheory = TheoryParts.GetNext(currentTheory);
            if (nextTheory == null) return;
            _theoryService.OpenTheory(nextTheory.CurrentTheoryId, CurrentUserId);
            nextTheory.IsVisible = true;
            nextTheory.TheoryPartElements[0].IsVisible = true;
        }

        public void ShowDocument(DocumentViewModel theoryDoc)
        {
            _documentService.ViewDocument(theoryDoc.DocumentId, _securityManager.CurrentUser.Id);
            theoryDoc.IsOpened = true;
            OpenTest(theoryDoc);
            System.Diagnostics.Process.Start(PathResolver.GenerateDocumentPath(theoryDoc.DocumentPath));

            if (IsTheoryComplete()) ShowTotalResult();
        }

        private void OpenTest(DocumentViewModel theoryDoc)
        {
            if (!theoryDoc.CurrentTheory.TheoryPartElements.OfType<DocumentViewModel>().Where(d => d.IsPractice)
                .All(d => d.IsOpened))
            {
                OpenNextDocument(theoryDoc.CurrentTheory.CurrentTheoryId);
            }
            else
            {
                var test = theoryDoc.CurrentTheory.TheoryPartElements.OfType<TestViewModel>()
                    .FirstOrDefault(t => t.Seq > theoryDoc.Seq);
                if (test != null) test.IsVisible = true;
            }
        }

        private void ShowCompleteTestMessage(double testResult, int testAttempt, bool ignoreResult)
        {
            var testResultString = testResult.ToString("F1");
            string message;
            if (ignoreResult)
                message = $"{string.Format(Resources.TestCompleteTemplateMessage, testResultString)}";
            else if (testResult > 70)
                message =
                    $"{string.Format(Resources.TestCompleteTemplateMessage, testResultString)} {Resources.GoToNextTheoryMessage}";
            else if (testAttempt >= 3)
                message =
                    $"{string.Format(Resources.TestCompleteTemplateMessage, testResultString)} {Resources.GoToNextTheoryWithWarningMessage}";
            else
                message =
                    $"{string.Format(Resources.TestCompleteTemplateMessage, testResultString)} {Resources.TestFailedMessage}";

            ShowMessage(message);
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

        private void ShowWelcomeMessage()
        {
            ShowMessage(Resources.WelcomeMessage, Resources.Logo, Brushes.White);
        }

        private void ShowMessage(string message, string imagePath = null, Brush textColor = null)
        {
            var messageViewModel = new MessageViewModel(message);
            if (!string.IsNullOrWhiteSpace(imagePath)) messageViewModel.Image = ImageResolver.LoadImage(imagePath);

            if (textColor != null) messageViewModel.TextColor = textColor;

            SelectedElement = messageViewModel;
        }

        public void ClearSelection()
        {
            SelectedElement = null;
        }

        private void Logout()
        {
            UserLogout?.Invoke(this, new System.EventArgs());
        }
    }
}