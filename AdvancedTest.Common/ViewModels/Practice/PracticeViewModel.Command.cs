using System;
using AdvancedTest.Common.Event;
using AdvancedTest.Common.Utils;
using AdvancedTest.Data.Enum;

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

        protected virtual Grade GetGrade()
        {
            return Grade.E;
        }

        protected virtual TestCompletedEventArgs GetError(Exception ex)
        {
            throw new NotSupportedException();
        }

        protected virtual TestCompletedEventArgs GetSuccessResult()
        {
            throw new NotSupportedException();
        }

        protected virtual string GetResultMessage()
        {
            return string.Empty;
        }
    }
}