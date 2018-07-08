using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Imaging;
using AdvancedTest.Common.Utils;
using AdvancedTest.Common.ViewModels.Answer;
using AdvancedTest.Common.ViewModels.TestPart;
using AdvancedTest.Common.ViewModels.Theory;
using AdvancedTest.Data.Enum;
using AdvancedTest.Data.Model;
using AdvancedTest.Service.Services.Interface;

namespace AdvancedTest.Common.ViewModels.Test
{
    /// <summary>
    /// Модель представления для формы теста
    /// </summary>
    public partial class TestViewModel : TimerTheoryPartElementViewModel
    {
        private readonly ITestService _testService;
        private readonly IUserService _userService;
        private readonly ISecurityManager _securityManager;
        private int _theoryId;
        private UserTheoryTestMark _userTest;
        private string _nextButtonText;
        private TestPartViewModelBase _testPartViewModelBase;

        // Коллекция заданий теста
        private List<TestPartViewModelBase> _testParts;

        public TestViewModel(ITestService testService, IUserService userService, ISecurityManager securityManager)
        {
            _testService = testService;
            _userService = userService;
            _securityManager = securityManager;
            InitializeCommands();
        }

        // Текущее задание теста
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


        // Ид текущей главы теории
        public override int CurrentTheoryId
        {
            get => _theoryId;
            set
            {
                _theoryId = value;
                LoadTestParts();
            }
        }
        // Текст кнопки "Следующее задание"
        public string NextButtonText
        {
            get => _nextButtonText;
            set
            {
                _nextButtonText = value;
                OnPropertyChanged(nameof(NextButtonText));
            }
        }

        // Флаг, указывающий, что тест входной
        public bool IsInitial { get; set; }
        // Флаг, указывающий, что тест итоговый
        public bool IsLast { get; set; }

        // Флаг, указывающий, что можно вернутся к предыдущему заданию
        public bool CanBack => false;

        // Флаг, указывающий, что можно перейти к следующему заданию
        public bool CanNext
        {
            get { return CurrentTestPart != null; }
        }

        // Текущая попытка
        public int Attempt { get; set; }

        // Функция загрузки заданий теста
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

        // Функция создания задания теста
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

        // Функция создания задания, требующего ввода текста
        private TestPartViewModelBase CreateCustomTextTestPart(TheoryTestPart testPart)
        {
            CustomTextTestPartViewModel testPartViewModelBase = new CustomTextTestPartViewModel
            {
                CurrentTest = this,
                TestText = string.IsNullOrWhiteSpace(testPart.Description) ?  LoadTestImage(testPart.TheoryPart.Seq, testPart.Seq) : null,
                Description = testPart.Description,
                TestPartType = testPart.TestType,
                CorrectAnswer = testPart.CorrectAnswer,
                Seq = testPart.Seq
            };

            return testPartViewModelBase;
        }

        // Функция создания задания сопоставления
        private TestPartViewModelBase CreateCompareTestPart(TheoryTestPart testPart)
        {
            CompareTestPartViewModel testPartViewModelBase = new CompareTestPartViewModel
            {
                CurrentTest = this,
                TestText = string.IsNullOrWhiteSpace(testPart.Description) ? LoadTestImage(testPart.TheoryPart.Seq, testPart.Seq) : null,
                Description = testPart.Description,
                TestPartType = testPart.TestType,
                CorrectAnswer = testPart.CorrectAnswer,
                Seq = testPart.Seq
            };
            testPartViewModelBase.Answers = CreateAnswers(testPart.Answers, testPartViewModelBase);

            return testPartViewModelBase;
        }

        // Функция создания задания с множественным выбором
        private TestPartViewModelBase CreateMultiplyChoiceTestPart(TheoryTestPart testPart)
        {
            SelectManyTestPartViewModel testPartViewModelBase = new SelectManyTestPartViewModel
            {
                CurrentTest = this,
                TestText = string.IsNullOrWhiteSpace(testPart.Description) ? LoadTestImage(testPart.TheoryPart.Seq, testPart.Seq) : null,
                Description = testPart.Description,
                TestPartType = testPart.TestType,
                CorrectAnswer = testPart.CorrectAnswer,
                Seq = testPart.Seq
            };
            testPartViewModelBase.Answers = CreateAnswers(testPart.Answers, testPartViewModelBase);

            return testPartViewModelBase;
        }

        // Функция создания теста с единственным выбором
        private TestPartViewModelBase CreateSingeChoiceTestPart(TheoryTestPart testPart)
        {
            SelectOneTestPartViewModel testPartViewModelBase = new SelectOneTestPartViewModel
            {
                CurrentTest = this,
                TestText = string.IsNullOrWhiteSpace(testPart.Description) ? LoadTestImage(testPart.TheoryPart.Seq, testPart.Seq) : null,
                Description = testPart.Description,
                TestPartType = testPart.TestType,
                CorrectAnswer = testPart.CorrectAnswer,
                Seq = testPart.Seq
            };
            testPartViewModelBase.Answers = CreateAnswers(testPart.Answers, testPartViewModelBase);

            return testPartViewModelBase;
        }

        // Функция загрузки изображения описания задания
        private BitmapImage LoadTestImage(int parentFolder, int fileName)
        {
            string path = PathResolver.GenerateTestImagePath(parentFolder.ToString(), fileName.ToString());
            return ImageResolver.LoadImage(path);
        }

        // Функция создания ответов задания
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

        // Функция создания ответа, для ручного ввода ответа
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

        // Функция создания ответа с сопоставлением
        private AnswerViewModel CreateCompareAnswerViewModel(TheoryTestPartAnswer answer,
            TestPartViewModelBase testPartViewModelBase)
        {
            var result = string.IsNullOrWhiteSpace(answer.Text)
                ? CreateImageAnswer(answer, testPartViewModelBase)
                : CreateTextAnswer(answer, testPartViewModelBase);

            result.Options = new ObservableCollection<AnswerOptionViewModel>(GetAnswerOptions(answer.Options));
            return result;
        }

        // Функция создания вариантов ответа для задания с сопоставлением
        private List<AnswerOptionViewModel> GetAnswerOptions(string answerOptions)
        {
            return answerOptions.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)
                .Select((answer , index ) => new AnswerOptionViewModel(answer, index)).ToList();
        }

        // Функция создания ответа с выбором одного значения
        private AnswerViewModel CreateSelectAnswerViewModel(TheoryTestPartAnswer answer,
            TestPartViewModelBase testPartViewModelBase)
        {
            return string.IsNullOrWhiteSpace(answer.Text)
                ? CreateImageAnswer(answer, testPartViewModelBase)
                : CreateTextAnswer(answer, testPartViewModelBase);
        }

        // Функция создания текстового ответа
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

        // Функция создания ответа с изображением
        private AnswerViewModel CreateImageAnswer(TheoryTestPartAnswer answer,
            TestPartViewModelBase testPartViewModelBase)
        {
            return new ImageAnswerViewModel
            {
                Seq = answer.AnswerNumber,
                CurrentTestPart = testPartViewModelBase,
                AnswerId = answer.Id,
                ImagePath = PathResolver.GenerateAnswerImagePath(answer.TheoryTestPart.TheoryPart.Seq.ToString(),
                    answer.TheoryTestPart.Seq.ToString(), answer.AnswerNumber.ToString())
            };
        }

        // Функция получения результата теста
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

    }
}