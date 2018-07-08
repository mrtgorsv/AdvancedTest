using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using AdvancedTest.Common.EventArgs;
using AdvancedTest.Common.Extensions;
using AdvancedTest.Common.Properties;
using AdvancedTest.Common.Utils;
using AdvancedTest.Common.ViewModels.Answer;
using AdvancedTest.Common.ViewModels.Base;
using AdvancedTest.Common.ViewModels.Practice;
using AdvancedTest.Common.ViewModels.Test;
using AdvancedTest.Common.ViewModels.Theory;
using AdvancedTest.Data.Model;
using AdvancedTest.Service.Services.Interface;

namespace AdvancedTest.Common.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        protected readonly ISecurityManager _securityManager;
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

        protected List<TheoryViewModel> TheoryParts
        {
            get { return TheorySections.SelectMany(ts => ts.TheoryParts).ToList(); }
        }

        public ObservableCollection<TheorySectionViewModel> TheorySections { get; set; }

        public virtual void LoadTheory()
        {
            UpdateUserDocs();
            var theorySectionList = _theoryService.GetTheorySectionList();
            if (_openedUserDocList.Count == 0 && theorySectionList.Count > 0)
            {
                var firstTheory = theorySectionList.First().TheoryParts.First();
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

            TheorySections =
                new ObservableCollection<TheorySectionViewModel>(theorySectionList.Select(CreateTheorySection));
        }

        private bool IsTheoryComplete()
        {
            return _theoryService.IsTheoryComplete(_securityManager.CurrentUser.Id);
        }

        private TheorySectionViewModel CreateTheorySection(TheorySection section)
        {
            var sectionViewModel = new TheorySectionViewModel
            {
                CurrentSectionId = section.Id,
                Seq = section.Seq,
                Name = section.Name
            };
            foreach (TheoryPart part in section.TheoryParts)
            {
                var theoryViewModel = CreateTheory(part);
                theoryViewModel.TheorySection = sectionViewModel;
                if (theoryViewModel.IsVisible)
                {
                    sectionViewModel.IsVisible = true;
                }

                sectionViewModel.TheoryParts.Add(theoryViewModel);
            }

            return sectionViewModel;
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
            if (theoryViewModel.TestTime > 0)
            {
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
            }

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

        public virtual void ShowWordPractice(PracticeViewModel wordPracticeListItem)
        {
        }

        public virtual void ShowExcelPractice(PracticeViewModel practiceListItem)
        {
        }

        public virtual void OnWordPracticeCompleted(object sender, TestCompletedEventArgs args)
        {
        }

        public virtual void OnExcelPracticeCompleted(object sender, TestCompletedEventArgs args)
        {
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

            if (nextDocId <= 0)
            {
                var currentTheory = TheoryParts.FirstOrDefault(tp => tp.CurrentTheoryId.Equals(theoryId));
                var test = currentTheory?.TheoryPartElements.OfType<TestViewModel>().FirstOrDefault();
                if (test != null)
                {
                    return;
                }

                OpentNextTheory(theoryId);
            }
            else
            {
                var theory = TheoryParts.FirstOrDefault(tp => tp.CurrentTheoryId.Equals(theoryId));
                var nextDoc = theory?.TheoryPartElements.OfType<DocumentViewModel>()
                    .FirstOrDefault(d => d.DocumentId.Equals(nextDocId));
                if (nextDoc != null) nextDoc.IsVisible = true;
            }
        }

        private void OpentNextTheory(int theoryId)
        {
            var currentTheory = TheoryParts.FirstOrDefault(tp => tp.CurrentTheoryId.Equals(theoryId));

            if (currentTheory == null) return;
            var nextTheory = TheoryParts.GetNext(currentTheory);
            if (nextTheory == null) return;
            if (nextTheory.IsVisible)
            {
                OpentNextTheory(nextTheory.CurrentTheoryId);
            }
            else
            {
                _theoryService.OpenTheory(nextTheory.CurrentTheoryId, CurrentUserId);
                nextTheory.IsVisible = true;
                nextTheory.TheorySection.IsVisible = true;
                nextTheory.TheoryPartElements[0].IsVisible = true;
                foreach (DocumentViewModel practice in nextTheory.TheoryPartElements.OfType<DocumentViewModel>()
                    .Where(d => d.IsPractice))
                {
                    practice.IsVisible = true;
                }
            }
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
            var test = theoryDoc.CurrentTheory.TheoryPartElements.OfType<TestViewModel>().FirstOrDefault();
            if (test != null)
            {
                if (!test.IsVisible && theoryDoc.CurrentTheory.TheoryPartElements.OfType<DocumentViewModel>()
                        .Where(tp => tp.IsPractice).All(tp => tp.IsOpened))
                {
                    test.IsVisible = true;
                }
            }
            else
            {
                OpenNextDocument(theoryDoc.CurrentTheory.CurrentTheoryId);
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

        private void ShowWelcomeMessage()
        {
            ShowMessage(Resources.WelcomeMessage, Resources.Logo, Brushes.White);
        }

        public void ShowMessage(string message, string imagePath = null, Brush textColor = null)
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