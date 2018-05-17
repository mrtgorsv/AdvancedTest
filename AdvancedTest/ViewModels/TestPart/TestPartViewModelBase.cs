using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using AdvancedTest.Data.Enum;
using AdvancedTest.ViewModels.Answer;
using AdvancedTest.ViewModels.Base;
using AdvancedTest.ViewModels.Test;

namespace AdvancedTest.ViewModels.TestPart
{
    public class TestPartViewModelBase : ViewModelBase
    {
        private BitmapImage _testText;
        private string _testPath;
        private string _currentAnswer;

        public TestPartViewModelBase()
        {
        }


        public bool IsValid
        {
            get { return string.Compare(CorrectAnswer, CurrentAnswer, StringComparison.InvariantCulture) == 0; }
        }

        public string TextPath
        {
            get => _testPath;
            set
            {
                _testPath = value;
                OnPropertyChanged(nameof(TextPath));
            }
        }

        public BitmapImage TestText
        {
            get => _testText;
            set
            {
                _testText = value;
                OnPropertyChanged(nameof(TestText));
                OnPropertyChanged(nameof(ImageWidth));
            }
        }

        public int ImageWidth => (int)TestText.Width;

        public TestViewModel CurrentTest { get; set; }

        public TestPartType TestPartType { get; set; }

        public string CurrentAnswer
        {
            get { return _currentAnswer; }
            set
            {
                _currentAnswer = value;
                OnPropertyChanged(nameof(CorrectAnswer));
            }
        }

        public string CorrectAnswer { get; set; }

        public ObservableCollection<AnswerViewModel> Answers { get; set; }

        public virtual string GetUserAnswer()
        {
            throw new NotSupportedException();
        }
    }
}