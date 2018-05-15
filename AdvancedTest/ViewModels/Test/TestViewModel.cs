using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using AdvancedTest.Data.Enum;
using AdvancedTest.Data.Model;
using AdvancedTest.Extensions;
using AdvancedTest.Service.Services.Interface;
using AdvancedTest.Utils;
using AdvancedTest.ViewModels.Answer;
using AdvancedTest.ViewModels.Theory;

namespace AdvancedTest.ViewModels.Test
{
    public partial class TestViewModel : TheoryPartElementViewModel
    {
        private readonly ITestService _testService;
        private int _theoryId;
        private string _nextButtonText;
        private bool _isStarted;
        private TestPartViewModel _testPartViewModel;

        private List<TestPartViewModel> _testParts;

        public TestViewModel(ITestService testService)
        {
            _testService = testService;
            InitializeCommands();
        }

        public TestPartViewModel CurrentTestPart
        {
            get => _testPartViewModel;
            set
            {
                _testPartViewModel = value;
                OnPropertyChanged(nameof(CurrentTestPart));
                OnPropertyChanged(nameof(CanNext));
            }
        }

        public int CurrentTheoryId
        {
            get => _theoryId;
            set
            {
                _theoryId = value;
                LoadTestParts();
            }
        }

        public string NextButtonText
        {
            get => _nextButtonText;
            set
            {
                _nextButtonText = value;
                OnPropertyChanged(nameof(NextButtonText));
            }
        }

        public bool IsStarted
        {
            get => _isStarted;
            set
            {
                _isStarted = value;
                OnPropertyChanged(nameof(IsStarted));
                OnPropertyChanged(nameof(CanStart));
            }
        }
        public bool CanStart => !_isStarted;

        public bool CanBack => false;

        public bool CanNext
        {
            get { return _testParts.GetNext(CurrentTestPart) != null; }
        }


        private void LoadTestParts()
        {
            List<TestPartViewModel> result = new List<TestPartViewModel>();

            List<TheoryTestPart> testParts = _testService.GetParts(_theoryId);

            foreach (TheoryTestPart testPart in testParts)
            {
                TestPartViewModel testPartViewModel = new TestPartViewModel
                {
                    CurrentTest = this,
                    TestText = LoadTestTextImage(testPart.TheoryPart.Seq , testPart.Seq),
                    TestPartType = testPart.TestType
                };
                testPartViewModel.Answers = CreateAnswers(testPart.Answers, testPartViewModel);
                result.Add(testPartViewModel);
            }
            _testParts = result;
        }

        private BitmapImage LoadTestTextImage(int parentFolder, int fileName)
        {
            string path = PathResolver.GenerateTestDescriptionPath(parentFolder.ToString(), fileName.ToString());
            Uri.TryCreate(path, UriKind.Relative, out var uri);
            var image =  new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = uri;
            image.EndInit();
            return image;
        }

        private ObservableCollection<AnswerViewModel> CreateAnswers(IEnumerable<TheoryTestPartAnswer> testPartAnswers,
            TestPartViewModel testPartViewModel)
        {
            List<AnswerViewModel> answers = new List<AnswerViewModel>();
            switch (testPartViewModel.TestPartType)
            {
                case TestPartType.SingleChoice:
                case TestPartType.MultiplyChoice:
                    answers.AddRange(testPartAnswers.Select(a => CreateSelectAnswerViewModel(a, testPartViewModel)));
                    break;
                case TestPartType.Compare:
                    answers.AddRange(testPartAnswers.Select(a => CreateComareAnswerViewModel(a, testPartViewModel)));
                    break;
                case TestPartType.Manual:
                    answers.AddRange(testPartAnswers.Select(a => CreateInputAnswerViewModel(a, testPartViewModel)));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new ObservableCollection<AnswerViewModel>(answers);
        }

        private AnswerViewModel CreateInputAnswerViewModel(TheoryTestPartAnswer answer,
            TestPartViewModel testPartViewModel)
        {
            return new InputAnswerViewModel
            {
                Seq = answer.Seq,
                InputResult = string.Empty,
                CurrentTestPart = testPartViewModel,
                AnswerId = answer.Id
            };
        }

        private AnswerViewModel CreateComareAnswerViewModel(TheoryTestPartAnswer answer,
            TestPartViewModel testPartViewModel)
        {
            return new CompareAnswerViewModel
            {
                Seq = answer.Seq,
                CurrentTestPart = testPartViewModel,
                AnswerId = answer.Id
            };
        }

        private AnswerViewModel CreateSelectAnswerViewModel(TheoryTestPartAnswer answer,
            TestPartViewModel testPartViewModel)
        {
            return new SelectAnswerViewModel
            {
                Seq = answer.Seq,
                CurrentTestPart = testPartViewModel,
                AnswerId = answer.Id,
                Text = answer.Text,
                ImagePath = answer.ImagePath
            };
        }
    }
}
