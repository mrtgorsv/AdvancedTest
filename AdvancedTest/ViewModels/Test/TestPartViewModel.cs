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
        public TestPartViewModel()
        {
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

        public ObservableCollection<AnswerViewModel> Answers { get; set; }
    }
}
