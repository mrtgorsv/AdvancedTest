using System;
using AdvancedTest.Utils;

namespace AdvancedTest.ViewModels.Login
{
    public partial class LoginViewModel
    {
        public DelegateCommand LoginCommand { get; set; }

        public void InitializeCommands()
        {
            LoginCommand = new DelegateCommand(TryLogin);
        }

        private void TryLogin()
        {
            try
            {
                var user = _userService.LogIn(_login, _password);
                if (user == null)
                {
                    throw new ArgumentNullException("Пользователь не найден");
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

