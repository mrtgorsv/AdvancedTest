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
    public class MainViewModel : ViewModelBase
    {
        private readonly ISecurityManager _securityManager;
        private readonly ITheoryService _theoryService;
        private readonly IUserService _userService;
        private readonly IDocumentService _documentService;
        private readonly ViewModelLocator _locator;
        private ViewModelBase _selectedElement;
        private List<UserTheoryDocumentMark> _openedUserDocList;

        public MainViewModel(ITheoryService theoryService, ViewModelLocator locator, IUserService userService,
            ISecurityManager securityManager, IDocumentService documentService)
        {
            _theoryService = theoryService;
            _locator = locator;
            _userService = userService;
            _securityManager = securityManager;
            _documentService = documentService;
            LoadTheory();
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

        private void LoadTheory()
        {
            UpdateUserDocs();
            var theoryList = _theoryService.GetTheoryList();
            if (_openedUserDocList.Count == 0 && theoryList.Count > 0)
            {
                var firstTheory = theoryList.First();
                _theoryService.OpenTheory(firstTheory.Id, CurrentUserId);
                UpdateUserDocs();
                ShowMessage(Resources.WelcomeMessage);
            }
            else
            {
                ShowMessage(Resources.ContinueMessage);
            }

            TheoryParts = new ObservableCollection<TheoryViewModel>(theoryList.Select(CreateTheory));
        }

        private void ShowMessage(string message)
        {
            SelectedElement = new MessageViewModel(message);
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
            foreach (TheoryDocument document in theory.TheoryDocuments)
            {
                DocumentViewModel documentViewModel = new DocumentViewModel
                {
                    CurrentTheory = theoryViewModel,
                    Seq = document.Seq > 1 ? document.Seq + 1 : document.Seq,
                    DocumentPath = document.DocumentPath,
                    DocumentId = document.Id,
                    Name = document.Name,
                    IsVisible = _openedUserDocList.Any(
                        td => td.DocumentId == document.Id && td.TheoryPartId.Equals(theory.Id))
                };
                elements.Add(documentViewModel);
            }

            var testViewModel = CreateTest(theoryViewModel);
            if (theoryViewModel.IsVisible && (_openedUserDocList.Any(td =>
                                                  td.TheoryPartId.Equals(theoryViewModel.CurrentTheoryId) &&
                                                  td.DocumentId.HasValue) || elements.Count == 0))
            {
                testViewModel.IsVisible = true;
            }

            if (elements.Count > 1)
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

        public ObservableCollection<TheoryViewModel> TheoryParts { get; set; }

        public void ShowTheoryTest(TestViewModel testListItem)
        {
            var test = CreateTest(testListItem.CurrentTheory);
            test.Name = $" Тест - {testListItem.CurrentTheory.Name}";
            test.CurrentTheoryId = testListItem.CurrentTheory.CurrentTheoryId;
            test.TestCompleted += OnTestCompleted;
            SelectedElement = test;
        }

        private void OnTestCompleted(object sender, TestCompletedEventArgs args)
        {
            ((TestViewModel) SelectedElement).TestCompleted -= OnTestCompleted;
            if (args.TestResult > 70 || args.TestAttempt >= 3)
            {
                OpentNextTheory(args.TheoryId);
            }
            else
            {
                OpenNextDocument(args.TheoryId);
            }

            ShowCompleteTestMessage(args.TestResult, args.TestAttempt);
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
            OpenTest(theoryDoc);
            System.Diagnostics.Process.Start(PathResolver.GenerateDocumentPath(theoryDoc.DocumentPath));
        }

        private void OpenTest(DocumentViewModel theoryDoc)
        {
            var test = theoryDoc.CurrentTheory.TheoryPartElements.OfType<TestViewModel>()
                .FirstOrDefault(t => t.Seq > theoryDoc.Seq);
            if (test != null)
            {
                test.IsVisible = true;
            }
        }

        private void ShowCompleteTestMessage(double testResult, int testAttempt)
        {
            string testResultString = testResult.ToString("F1");
            string message;
            if (testResult > 70)
            {
                message = $"{string.Format(Resources.TestCompleteTemplateMessage, testResultString)} {Resources.GoToNextTheoryMessage}";
            }
            else if (testAttempt >= 3)
            {
                message = $"{string.Format(Resources.TestCompleteTemplateMessage, testResultString)} {Resources.GoToNextTheoryWithWarningMessage}";
            }
            else
            {
                message = $"{string.Format(Resources.TestCompleteTemplateMessage, testResultString)} {Resources.TestFailedMessage}";
            }

            ShowMessage(message);
        }

        public void ClearSelection()
        {
            SelectedElement = null;
        }
    }
}