using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using AdvancedTest.Data.Enum;
using AdvancedTest.Data.Model;
using AdvancedTest.EventArgs;
using AdvancedTest.Service.Services.Interface;
using AdvancedTest.Utils;
using AdvancedTest.ViewModels.Answer;
using AdvancedTest.ViewModels.TestPart;
using AdvancedTest.ViewModels.Theory;

namespace AdvancedTest.ViewModels.Test
{
    public partial class TestViewModel : TheoryPartElementViewModel
    {
        private readonly ITestService _testService;
        private readonly IUserService _userService;
        private readonly ISecurityManager _securityManager;
        private int _theoryId;
        private UserTheoryTestMark _userTest;
        private string _nextButtonText;
        private bool _isStarted;
        private TestPartViewModelBase _testPartViewModelBase;
        private DispatcherTimer _timer = new DispatcherTimer(DispatcherPriority.Render, Application.Current.Dispatcher);
        private TimeSpan _testTime;


        public event TestCompletedEventHandler TestCompleted;

        public delegate void TestCompletedEventHandler(object sender, TestCompletedEventArgs args);

        private List<TestPartViewModelBase> _testParts;

        public TestViewModel(ITestService testService, IUserService userService, ISecurityManager securityManager)
        {
            _testService = testService;
            _userService = userService;
            _securityManager = securityManager;
            InitializeCommands();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTimerTick;
        }

        public TestPartViewModelBase CurrentTestPart
        {
            get => _testPartViewModelBase;
            set
            {
                _testPartViewModelBase = value;
                OnPropertyChanged(nameof(CurrentTestPart));
                OnPropertyChanged(nameof(CanNext));
            }
        }

        public string TestTime { get; set; }

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
            get { return CurrentTestPart != null; }
        }

        public int Attempt { get; set; }

        private void LoadTestParts()
        {
            List<TestPartViewModelBase> result = new List<TestPartViewModelBase>();

            List<TheoryTestPart> testParts = _testService.GetParts(_theoryId);

            foreach (TheoryTestPart testPart in testParts)
            {
                result.Add(CreateTestPart(testPart));
            }

            _testParts = result;
        }

        private TestPartViewModelBase CreateTestPart(TheoryTestPart testPart)
        {
            switch (testPart.TestType)
            {
                case TestPartType.SingleChoice:
                    return CreateSingeChoiceTestPart(testPart);
                case TestPartType.MultiplyChoice:
                    return CreateMultiplyChoiceTestPart(testPart);
                case TestPartType.Compare:
                    return CreateCompareTestPart(testPart);
                case TestPartType.Manual:
                    return CreateCustomTextTestPart(testPart);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private TestPartViewModelBase CreateCustomTextTestPart(TheoryTestPart testPart)
        {
            CustomTextTestPartViewModel testPartViewModelBase = new CustomTextTestPartViewModel
            {
                CurrentTest = this,
                TestText = LoadTestTextImage(testPart.TheoryPart.Seq, testPart.Seq),
                TestPartType = testPart.TestType,
                CorrectAnswer = testPart.CorrectAnswer,
                Seq = testPart.Seq
            };

            return testPartViewModelBase;
        }

        private TestPartViewModelBase CreateCompareTestPart(TheoryTestPart testPart)
        {
            CompareTestPartViewModel testPartViewModelBase = new CompareTestPartViewModel
            {
                CurrentTest = this,
                TestText = LoadTestTextImage(testPart.TheoryPart.Seq, testPart.Seq),
                TestPartType = testPart.TestType,
                CorrectAnswer = testPart.CorrectAnswer,
                Seq = testPart.Seq
            };
            testPartViewModelBase.Answers = CreateAnswers(testPart.Answers, testPartViewModelBase);

            return testPartViewModelBase;
        }

        private TestPartViewModelBase CreateMultiplyChoiceTestPart(TheoryTestPart testPart)
        {
            SelectManyTestPartViewModel testPartViewModelBase = new SelectManyTestPartViewModel
            {
                CurrentTest = this,
                TestText = LoadTestTextImage(testPart.TheoryPart.Seq, testPart.Seq),
                TestPartType = testPart.TestType,
                CorrectAnswer = testPart.CorrectAnswer,
                Seq = testPart.Seq
            };
            testPartViewModelBase.Answers = CreateAnswers(testPart.Answers, testPartViewModelBase);

            return testPartViewModelBase;
        }

        private TestPartViewModelBase CreateSingeChoiceTestPart(TheoryTestPart testPart)
        {
            SelectOneTestPartViewModel testPartViewModelBase = new SelectOneTestPartViewModel
            {
                CurrentTest = this,
                TestText = LoadTestTextImage(testPart.TheoryPart.Seq, testPart.Seq),
                TestPartType = testPart.TestType,
                CorrectAnswer = testPart.CorrectAnswer,
                Seq = testPart.Seq
            };
            testPartViewModelBase.Answers = CreateAnswers(testPart.Answers, testPartViewModelBase);

            return testPartViewModelBase;
        }

        private BitmapImage LoadTestTextImage(int parentFolder, int fileName)
        {
            string path = PathResolver.GenerateTestDescriptionPath(parentFolder.ToString(), fileName.ToString());
            Uri.TryCreate(path, UriKind.Relative, out var uri);
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = uri;
            image.EndInit();
            return image;
        }

        private ObservableCollection<AnswerViewModel> CreateAnswers(IEnumerable<TheoryTestPartAnswer> testPartAnswers,
            TestPartViewModelBase testPartViewModelBase)
        {
            List<AnswerViewModel> answers = new List<AnswerViewModel>();
            switch (testPartViewModelBase.TestPartType)
            {
                case TestPartType.SingleChoice:
                case TestPartType.MultiplyChoice:
                    answers.AddRange(testPartAnswers.Select(a =>
                        CreateSelectAnswerViewModel(a, testPartViewModelBase)));
                    break;
                case TestPartType.Compare:
                    answers.AddRange(
                        testPartAnswers.Select(a => CreateCompareAnswerViewModel(a, testPartViewModelBase)));
                    break;
                case TestPartType.Manual:
                    answers.AddRange(testPartAnswers.Select(a => CreateInputAnswerViewModel(a, testPartViewModelBase)));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new ObservableCollection<AnswerViewModel>(answers);
        }

        private AnswerViewModel CreateInputAnswerViewModel(TheoryTestPartAnswer answer,
            TestPartViewModelBase testPartViewModelBase)
        {
            return new TextAnswerViewModel
            {
                Seq = answer.AnswerNumber,
                Text = string.Empty,
                CurrentTestPart = testPartViewModelBase,
                AnswerId = answer.Id
            };
        }

        private AnswerViewModel CreateCompareAnswerViewModel(TheoryTestPartAnswer answer,
            TestPartViewModelBase testPartViewModelBase)
        {
            var result = string.IsNullOrWhiteSpace(answer.Text)
                ? CreateImageAnswer(answer, testPartViewModelBase)
                : CreateTextAnswer(answer, testPartViewModelBase);

            result.Options = new ObservableCollection<AnswerOptionViewModel>(GetAnswerOptions(answer.Options));
            return result;
        }

        private List<AnswerOptionViewModel> GetAnswerOptions(string answerOptions)
        {
            return answerOptions.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)
                .Select((answer , index ) => new AnswerOptionViewModel(answer, index)).ToList();
        }


        private AnswerViewModel CreateSelectAnswerViewModel(TheoryTestPartAnswer answer,
            TestPartViewModelBase testPartViewModelBase)
        {
            return string.IsNullOrWhiteSpace(answer.Text)
                ? CreateImageAnswer(answer, testPartViewModelBase)
                : CreateTextAnswer(answer, testPartViewModelBase);
        }

        private AnswerViewModel CreateTextAnswer(TheoryTestPartAnswer answer,
            TestPartViewModelBase testPartViewModelBase)
        {
            return new TextAnswerViewModel
            {
                Seq = answer.AnswerNumber,
                CurrentTestPart = testPartViewModelBase,
                AnswerId = answer.Id,
                Text = answer.Text
            };
        }

        private AnswerViewModel CreateImageAnswer(TheoryTestPartAnswer answer,
            TestPartViewModelBase testPartViewModelBase)
        {
            return new ImageAnswerViewModel
            {
                Seq = answer.AnswerNumber,
                CurrentTestPart = testPartViewModelBase,
                AnswerId = answer.Id,
                ImagePath = PathResolver.GenerateAnswerPath(answer.TheoryTestPart.TheoryPart.Seq.ToString(),
                    answer.TheoryTestPart.Seq.ToString(), answer.AnswerNumber.ToString())
            };
        }

        private double GetTestResult()
        {
            int total = _testParts.Count;
            int validAnswers = 0;
            foreach (TestPartViewModelBase testPart in _testParts)
            {
                if (testPart.IsValid)
                {
                    validAnswers++;
                }
            }

            if (validAnswers == 0)
            {
                return 0;
            }

            return ((double) validAnswers / total) * 100;
        }


        private void OnTimerTick(object sender, System.EventArgs e)
        {
            if (_testTime == TimeSpan.Zero)
            {
                CompleteTest();
            }

            _testTime = _testTime.Add(TimeSpan.FromSeconds(-1));
            TestTime = _testTime.ToString("T");
            OnPropertyChanged(nameof(TestTime));
        }

        protected virtual void OnTestCompleted(double result)
        {
            TestCompleted?.Invoke(this, new TestCompletedEventArgs(result, CurrentTheoryId, _userTest.Attempt));
        }

        public void SetTestTime(TimeSpan testTime)
        {
            _testTime = testTime;
        }
    }
}