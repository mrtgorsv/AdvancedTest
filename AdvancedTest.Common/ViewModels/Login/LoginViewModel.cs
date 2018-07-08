using AdvancedTest.Common.Utils;
using AdvancedTest.Common.ViewModels.Base;
using AdvancedTest.Service.Services.Interface;

namespace AdvancedTest.Common.ViewModels.Login
{
    /// <summary>
    /// Модель представления для формы логина пользователя
    /// </summary>
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

        // Логин
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        // Пароль
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        // Текст ошибки
        public string ErrorText
        {
            get => _errorText;
            set
            {
                _errorText = value;
                OnPropertyChanged(nameof(ErrorText));
            }
        }

        // Вызывает событие успешного логина в систему
        private void OnSuccessLogin()
        {
            RaiseUserLoginEvent();
        }

        // Оповещает всех подписчиков о событии входа в систему
        protected void RaiseUserLoginEvent()
        {
            UserLogin?.Invoke(this, new System.EventArgs());
        }
    }
}
