using System.Windows.Media.Imaging;

namespace AdvancedTest.ViewModels.Base
{
    public class MessageViewModel : ViewModelBase
    {
        private string _message;
        private BitmapImage _image;

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
    }
}