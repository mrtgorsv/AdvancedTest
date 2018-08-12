using System.Collections.Generic;
using AdvancedTest.Common.ViewModels.Base;
using AdvancedTest.Common.ViewModels.Practice;
using AdvancedTest.Common.ViewModels.Test;
using AdvancedTest.Common.ViewModels.Theory;

namespace AdvancedTest.Common.ViewModels.Interfaces
{
    public interface IMainWindowViewModel
    {
        event MainViewModel.UserLogoutEventHandler UserLogout;

        string Title { get; }
        List<TheoryViewModel> TheoryParts { get; }
        int CurrentUserId { get; }
        ViewModelBase SelectedElement { get; set; }

        void LoadDataSource();
        void ClearSelection();

        void ShowTest(TestViewModel selectedTest);

        void ShowDocument(DocumentViewModel document);
        void ShowPractice(PracticeViewModel practice);
    }
}