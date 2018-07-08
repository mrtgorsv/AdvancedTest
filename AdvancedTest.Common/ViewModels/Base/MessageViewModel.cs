using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AdvancedTest.Common.ViewModels.Base
{
    /// <summary>
    /// Модель представления для сообщений приложения
    /// </summary>
    public class MessageViewModel : ViewModelBase
    {
        private string _message;
        private BitmapImage _image;
        private Brush _textColor = Brushes.Black;

        public MessageViewModel()
        {
        }
        public MessageViewModel(string message)
        {
            Message = message;
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
        public BitmapImage Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public Brush TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                OnPropertyChanged(nameof(TextColor));
            }
        }
    }
}