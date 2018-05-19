using AdvancedTest.Service.Services.Interface;
using AdvancedTest.Utils;
using AdvancedTest.ViewModels.Base;

namespace AdvancedTest.ViewModels.Login
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly ISecurityManager _securityManager;
        private string _login;
        private string _password;
        private string _errorText;

        public event UserLoginEventHandler UserLogin;
        public delegate void UserLoginEventHandler(object sender, System.EventArgs args);

        public LoginViewModel(IUserService userService, ISecurityManager securityManager)
        {
            _userService = userService;
            _securityManager = securityManager;
            InitializeCommands();
        }

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorText
        {
            get => _errorText;
            set
            {
                _errorText = value;
                OnPropertyChanged(nameof(ErrorText));
            }
        }

        private void OnSuccessLogin()
        {
            RaiseUserLoginEvent();
        }

        protected void RaiseUserLoginEvent()
        {
            UserLogin?.Invoke(this, new System.EventArgs());
        }
    }
}
