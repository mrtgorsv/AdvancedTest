using System.Collections.ObjectModel;
using AdvancedTest.Common.ViewModels.Answer;
using AdvancedTest.Common.ViewModels.TestPart;
using AdvancedTest.Common.ViewModels.Theory;
using AdvancedTest.Data.Enum;

namespace AdvancedTest.Common.ViewModels.Practice
{
    /// <summary>
    /// Модель представления для документов теории
    /// </summary>
    public partial class PracticeViewModel : TimerTheoryPartElementViewModel
    {
        public int DocumentId { get; set; }
        public string Description { get; set; }

        public virtual PracticeType PracticeType { get;}

        public virtual string DocumentPath { get; set; }
        public virtual string ButtonText { get; set; }
        public virtual string SelectionDescription { get; }
        public virtual int SelectedOption { get; set; }

        public PracticeViewModel()
        {
            SetEndless(true);
            InitializeCommands();
        }

        public TestPartViewModelBase CurrentTestPart { get; set; }
        public ObservableCollection<AnswerOptionViewModel> Options { get; set; }

    }
}