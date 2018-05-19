using System.Collections.ObjectModel;
using AdvancedTest.Properties;
using AdvancedTest.Service.Services.Interface;

namespace AdvancedTest.ViewModels.Base
{
    public class UserResultViewModel : MessageViewModel
    {
        private int _userId;

        private readonly ITheoryService _theoryService;

        public UserResultViewModel(ITheoryService theoryService)
        {
            _theoryService = theoryService;
        }

        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                LoadResults();
            }
        }
        public ObservableCollection<string> UserResults { get; set; }

        private void LoadResults()
        {
            UserResults = new ObservableCollection<string>(_theoryService.GetUserResults(_userId , out var complete));
            Message = complete ? Resources.UserCompleteTheoryMessage: Resources.UserTheoryResultMessage;
        }
    }
}