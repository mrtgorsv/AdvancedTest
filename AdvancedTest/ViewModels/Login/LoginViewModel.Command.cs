using System;
using AdvancedTest.Utils;

namespace AdvancedTest.ViewModels.Login
{
    /// <summary>
    /// Модель представления для формы логина пользователя
    /// </summary>
    public partial class LoginViewModel
    {
        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand CreateCommand { get; set; }

        public void InitializeCommands()
        {
            LoginCommand = new DelegateCommand(TryLogin);
            CreateCommand = new DelegateCommand(Create);
        }

        // Функция логина пользователя в систему
        private void TryLogin()
        {
            try
            {
                var user = _userService.LogIn(_login, _password);
                if (user == null)
                {
                    throw new InvalidOperationException("Пользователь не найден");
                }
                _securityManager.CurrentUser = user;
                OnSuccessLogin();
            }
            catch (Exception e)
            {
                ErrorText = e.Message;
            }
        }
        // Функция логина пользователя в систему
        private void Create()
        {
            try
            {
                var user = _userService.LogIn(_login, _password);
                if (user == null)
                {
                    throw new InvalidOperationException("Пользователь не найден");
                }
                _securityManager.CurrentUser = user;
                OnSuccessLogin();
            }
            catch (Exception e)
            {
                ErrorText = e.Message;
            }
        }
    }
}

