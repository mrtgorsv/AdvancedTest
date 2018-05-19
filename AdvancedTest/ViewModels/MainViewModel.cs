using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AdvancedTest.Data.Model;
using AdvancedTest.EventArgs;
using AdvancedTest.Extensions;
using AdvancedTest.Properties;
using AdvancedTest.Service.Services.Interface;
using AdvancedTest.Utils;
using AdvancedTest.ViewModels.Base;
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

        public int CurrentUserId
        {
            get { return _securityManager.CurrentUser.Id; }
        }

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
                {
                    ShowTotalResult();
                }
                else
                {
                    ShowMessage(Resources.ContinueMessage, Resources.Logo);
                }
            }

            TheoryParts = new ObservableCollection<TheoryViewModel>(theoryList.Select(CreateTheory));
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
            List<TheoryPartElementViewModel> elements = new List<TheoryPartElementViewModel>();
            List<DocumentViewModel> documents = new List<DocumentViewModel>();
            foreach (TheoryDocument document in theory.TheoryDocuments)
            {
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
            }

            elements.AddRange(documents);
            var testViewModel = CreateTest(theoryViewModel);
            testViewModel.IsInitial = theory.IsInitial;
            testViewModel.IsLast = theory.IsLast;
            if (theoryViewModel.IsVisible
                && (_openedUserDocList.Any(td =>
                        td.TheoryPartId.Equals(theoryViewModel.CurrentTheoryId) && td.DocumentId.HasValue) ||
                    elements.Count == 0)
                && documents.Where(d => d.IsPractice).All(d => d.IsOpened))
            {
                testViewModel.IsVisible = true;
            }

            if (elements.Count > 1 && documents.All(d => !d.IsPractice))
            {
                elements.Insert(1, testViewModel);
            }
            else
            {
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

        private void OnTestCompleted(object sender, TestCompletedEventArgs args)
        {
            var test = ((TestViewModel) SelectedElement);
            test.TestCompleted -= OnTestCompleted;
            if (args.TestResult > 70 || args.TestAttempt >= 3 || test.IsInitial)
            {
                OpentNextTheory(args.TheoryId);
            }
            else
            {
                OpenNextDocument(args.TheoryId);
            }

            if (test.IsLast)
            {
                ShowTotalResult();
            }
            else
            {
                ShowCompleteTestMessage(args.TestResult, args.TestAttempt, test.IsInitial);
            }
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
            if (nextDoc != null)
            {
                nextDoc.IsVisible = true;
            }
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

            if (IsTheoryComplete())
            {
                ShowTotalResult();
            }
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
                if (test != null)
                {
                    test.IsVisible = true;
                }
            }
        }

        private void ShowCompleteTestMessage(double testResult, int testAttempt, bool ignoreResult)
        {
            string testResultString = testResult.ToString("F1");
            string message;
            if (ignoreResult)
            {
                message = $"{string.Format(Resources.TestCompleteTemplateMessage, testResultString)}";
            }
            else if (testResult > 70)
            {
                message =
                    $"{string.Format(Resources.TestCompleteTemplateMessage, testResultString)} {Resources.GoToNextTheoryMessage}";
            }
            else if (testAttempt >= 3)
            {
                message =
                    $"{string.Format(Resources.TestCompleteTemplateMessage, testResultString)} {Resources.GoToNextTheoryWithWarningMessage}";
            }
            else
            {
                message =
                    $"{string.Format(Resources.TestCompleteTemplateMessage, testResultString)} {Resources.TestFailedMessage}";
            }

            ShowMessage(message);
        }

        private void ShowWelcomeMessage()
        {
            ShowMessage(Resources.WelcomeMessage, Resources.Logo);
        }

        private void ShowMessage(string message, string imagePath = null)
        {
            var messageViewModel = new MessageViewModel(message);
            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                messageViewModel.Image = ImageResolver.LoadImage(imagePath);
            }

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