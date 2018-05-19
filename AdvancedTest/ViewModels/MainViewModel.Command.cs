using System;
using AdvancedTest.Utils;

namespace AdvancedTest.ViewModels
{
    public partial class MainViewModel
    {
        public DelegateCommand ShowUserResultCommand { get; set; }
        public DelegateCommand ExitCommand { get; set; }

        private void InitializeCommands()
        {
            ShowUserResultCommand = new DelegateCommand(ShowUserResult);
            ExitCommand = new DelegateCommand(Exit);
        }

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
