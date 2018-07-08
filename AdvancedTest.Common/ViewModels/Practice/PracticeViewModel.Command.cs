using System;
using AdvancedTest.Common.Utils;

namespace AdvancedTest.Common.ViewModels.Practice
{
    /// <summary>
    /// Модель представления для документов теории
    /// </summary>
    public partial class PracticeViewModel : IDisposable
    {
        // Команда отображения формы результатов пользователя
        public DelegateCommand StartCommand { get; set; }

        // Команда выхода из приложения
        public DelegateCommand RulesCommand { get; set; }

        // Функция инициализации команд
        private void InitializeCommands()
        {
            StartCommand = new DelegateCommand(Start);
        }

        public virtual void Dispose()
        {
            //

        }
    }
}