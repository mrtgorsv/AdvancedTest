using System;
using AdvancedTest.Utils;

namespace AdvancedTest.ViewModels.Login
{
    /// <summary>
    /// Модель представления для формы логина пользователя
    /// </summary>
    public partial class RegisterViewModel
    {
        public DelegateCommand CreateCommand { get; set; }

        public void InitializeCommands()
        {
            CreateCommand = new DelegateCommand(Create);
        }

        // Функция логина пользователя в систему
        private void Create()
        {

            if (RepeatPassword != Password)
            {
                ErrorText = "Пароли не совпадают";
                return;
            }

            try
            {
                var user = _userService.Create(_login, _password);
                _securityManager.CurrentUser = user;
                OnUserCreate();
            }
            catch (Exception e)
            {
                ErrorText = e.Message;
            }
        }
    }
}

