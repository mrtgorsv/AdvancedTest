using AdvancedTest.Common.Utils;

namespace AdvancedTest.Common.ViewModels
{
    /// <summary>
    /// Модель представления для главного окна приложения
    /// </summary>
    public partial class MainViewModel
    {
        // Команда отображения формы результатов пользователя
        public DelegateCommand ShowUserResultCommand { get; set; }

        // Команда выхода из приложения
        public DelegateCommand ExitCommand { get; set; }

        // Функция инициализации команд
        private void InitializeCommands()
        {
            ShowUserResultCommand = new DelegateCommand(ShowUserResult);
            ExitCommand = new DelegateCommand(Exit);
        }

        // Функция выхода из приложения
        private void Exit()
        {
            Logout();
        }
		
        private void ShowUserResult()
        {
            ShowTotalResult();
        }
    }
}
