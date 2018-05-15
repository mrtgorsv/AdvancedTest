using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AdvancedTest.Data.Model;
using AdvancedTest.Service.Services.Interface;
using AdvancedTest.Utils;
using AdvancedTest.ViewModel;
using AdvancedTest.ViewModels.Test;
using AdvancedTest.ViewModels.Theory;

namespace AdvancedTest.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ISecurityManager _securityManager;
        private readonly ITheoryService _theoryService;
        private readonly IUserService _userService;
        private readonly ViewModelLocator _locator;
        private TheoryPartElementViewModel _selectedElement;

        public MainViewModel(ITheoryService theoryService, ViewModelLocator locator, IUserService userService, ISecurityManager securityManager)
        {
            _theoryService = theoryService;
            _locator = locator;
            _userService = userService;
            _securityManager = securityManager;
            LoadTheory();
        }

        public TheoryPartElementViewModel SelectedElement
        {
            get => _selectedElement;
            set
            {
                _selectedElement = value;
                OnPropertyChanged(nameof(SelectedElement));
            }
        }

        private void LoadTheory()
        {
            var theoryList = _theoryService.GetTheoryList();
            TheoryParts = new ObservableCollection<TheoryViewModel>(theoryList.Select(CreateTheory));
        }

        private TheoryViewModel CreateTheory(TheoryPart theory)
        {
            var theoryViewModel = new TheoryViewModel
            {
                CurrentTheoryId = theory.Id,
                Seq = theory.Seq,
                Name = theory.Name
            };
            theoryViewModel.TheoryPartElements = CreateElements(theory, theoryViewModel);
            return theoryViewModel;
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
                    IsVisible = true
                };
                elements.Add(documentViewModel);
            }

            var testViewModel = CreateTest(theoryViewModel);
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
            testViewModel.IsVisible = true;
            return testViewModel;
        }

        public ObservableCollection<TheoryViewModel> TheoryParts { get; set; }

        public void ShowTheoryTest(TestViewModel testListItem)
        {
            var test = CreateTest(testListItem.CurrentTheory);
            test.CurrentTheoryId = testListItem.CurrentTheory.CurrentTheoryId;
            SelectedElement = test;
        }

        public void ShowDocument(DocumentViewModel theoryDoc)
        {
            _userService.MarkDocumentAsViewed(theoryDoc.DocumentId , _securityManager.CurrentUser.Id);
            System.Diagnostics.Process.Start(PathResolver.GenerateDocumentPath(theoryDoc.CurrentTheory.Seq.ToString(),theoryDoc.Seq.ToString()));
        }
    }
}