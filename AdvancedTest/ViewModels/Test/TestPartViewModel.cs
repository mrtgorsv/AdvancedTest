using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using AdvancedTest.Data.Enum;
using AdvancedTest.ViewModel;
using AdvancedTest.ViewModels.Answer;

namespace AdvancedTest.ViewModels.Test
{
    public class TestPartViewModel : ViewModelBase
    {
        private BitmapImage _testText;
        private string _testPath;
        private string _currentAnswer;

        public TestPartViewModel()
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
            }
        }

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
    }
}