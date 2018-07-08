using System.Collections.ObjectModel;
using AdvancedTest.Common.Properties;
using AdvancedTest.Service.Services.Interface;

namespace AdvancedTest.Common.ViewModels.Base
{
    /// <summary>
    /// Модель представления для результатов прохождения тестов пользователя
    /// </summary>
    public class UserResultViewModel : MessageViewModel
    {
        private int _userId;

        private readonly ITheoryService _theoryService;

        public UserResultViewModel(ITheoryService theoryService)
        {
            _theoryService = theoryService;
        }

        // Ид пользователя
        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                LoadResults();
            }
        }

        // Коллекция результатов
        public ObservableCollection<string> UserResults { get; set; }

        // Функция загрузки результатов пользователя
        private void LoadResults()
        {
            UserResults = new ObservableCollection<string>(_theoryService.GetUserResults(_userId , out var complete));
            Message = complete ? Resources.UserCompleteTheoryMessage: Resources.UserTheoryResultMessage;
        }
    }
}