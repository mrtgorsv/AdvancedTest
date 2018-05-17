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
        private string _currentAnswer;

        public bool IsValid
        {
            get { return string.Compare(CorrectAnswer, GetUserAnswer(), StringComparison.InvariantCulture) == 0; }
        }

        public BitmapImage TestText
        {
            get => _testText;
            set
            {
                _testText = value;
                OnPropertyChanged(nameof(TestText));
                OnPropertyChanged(nameof(ImageWidth));
                OnPropertyChanged(nameof(ImageHeight));
            }
        }

        public int ImageWidth => (int)TestText.Width;
        public int ImageHeight => (int)TestText.Height;

        public TestViewModel CurrentTest { get; set; }
        public int Seq { get; set; }

        public TestPartType TestPartType { get; set; }

        public string CorrectAnswer { get; set; }

        public ObservableCollection<AnswerViewModel> Answers { get; set; }

        public virtual string GetUserAnswer()
        {
            throw new NotSupportedException();
        }
    }
}