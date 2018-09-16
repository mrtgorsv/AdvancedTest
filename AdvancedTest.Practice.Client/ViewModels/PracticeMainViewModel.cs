using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using AdvancedTest.Common.Event;
using AdvancedTest.Common.Utils;
using AdvancedTest.Common.ViewModels;
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
        protected override bool CanEditPractice => true;

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

        public override string Title => "Система адаптивного обучения. Практика";

        #region Practice

        protected override void LoadPractice()
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
                IsVisible = true,
                CurrentTheoryId = -1
            };

            StaticTheoryBuilder.AddWordOptions(practiceViewModel);
            UpdateSelection(practiceViewModel);

            if (withDocs)
            {
                LoadDocuments(practiceViewModel);
            }

            return practiceViewModel;
        }

        private void LoadDocuments(WordPracticeViewModel practiceViewModel)
        {
            int filePrefix = practiceViewModel.SelectedOption + 1;
            practiceViewModel.StartDocument = (byte[]) Resources.ResourceManager.GetObject($"_{filePrefix}");
            practiceViewModel.ResultDocument = (byte[]) Resources.ResourceManager.GetObject($"_{filePrefix}_result");
        }

        private PracticeViewModel CreateExcelPracticeViewModel()
        {
            var practiceViewModel = new ExcelPracticeViewModel
            {
                Name = "Контрольная работа №2. Microsoft Excel",
                Description =
                    "В данной контрольной работе вам необходимо выполнить расчет по формулам в Microsoft Office Excel. Выберете вариант и приступайте к решению",
                IsVisible = true,
                CurrentTheoryId = -2
            };
            StaticTheoryBuilder.AddExcelOptions(practiceViewModel);

            UpdateSelection(practiceViewModel);
            return practiceViewModel;
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
            UpdateSelection(practiceViewModel);
            if (practiceViewModel.PracticeType.Equals(PracticeType.Word))
            {
                LoadDocuments(practiceViewModel as WordPracticeViewModel);
            }
            practiceViewModel.TestCompleted += OnPracticeCompleted;
            practiceViewModel.Name = practice.Name;
            SelectedElement = practiceViewModel;
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