using System;
using AdvancedTest.Service.Services.Interface;
using AdvancedTest.Utils;

namespace AdvancedTest.ViewModel.Login
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly ISecurityManager _securityManager;
        private string _login;
        private string _password;
        private string _errorText;

        public event UserLoginEventHandler UserLogin;
        public delegate void UserLoginEventHandler(object sender, EventArgs args);

        public LoginViewModel(IUserService userService, ISecurityManager securityManager)
        {
            _userService = userService;
            _securityManager = securityManager;
            InitializeCommands();
        }

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                RaisePropertyChangedEvent(nameof(Login));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChangedEvent(nameof(Password));
            }
        }
        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                RaisePropertyChangedEvent(nameof(ErrorText));
            }
        }

        private void OnSuccessLogin()
        {
            RaiseUserLoginEvent();
        }

        protected void RaiseUserLoginEvent()
        {
            UserLogin?.Invoke(this, new EventArgs());
        }
    }
}
