using AdvancedTest.ViewModel;

namespace AdvancedTest.ViewModels.Base
{
    public class MessageViewModel : ViewModelBase
    {
        private string _message;

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
    }
}