using System;
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
    [Serializable]
    public partial class PracticeViewModel : TimerTheoryPartElementViewModel
    {
        public int DocumentId { get; set; }
        public string Description { get; set; }

        public virtual PracticeType PracticeType { get; set; }

        public virtual string DocumentPath { get; set; }
        public virtual string ButtonText { get; set; }
        public string SelectionDescription => Options[_selectedOption].Description;

        private int _selectedOption;

        public virtual int SelectedOption
        {
            get => _selectedOption;
            set
            {
                _selectedOption = value;
                OnPropertyChanged(nameof(SelectedOption));
                OnPropertyChanged(nameof(SelectionDescription));
            }
        }

        public PracticeViewModel()
        {
            SetEndless(true);
            InitializeCommands();
        }

        public TestPartViewModelBase CurrentTestPart { get; set; }
        public ObservableCollection<AnswerOptionViewModel> Options { get; set; }

    }
}