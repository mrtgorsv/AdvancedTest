using System.Collections.ObjectModel;
using AdvancedTest.Common.ViewModels.Base;
using AdvancedTest.Common.ViewModels.TestPart;

namespace AdvancedTest.Common.ViewModels.Answer
{
    /// <summary>
    /// Модель представления для варианта ответа тестового задания
    /// </summary>
    public class AnswerViewModel : ViewModelBase
    {
        // Флаг, указывающий, что вариант выбран
        private bool _isSelected;

        // Порядок отображение в списке ответов
        public int Seq { get; set; }
        // Ид ответа в базе данных
        public int AnswerId { get; set; }
        // Флаг, указывающий на режим отображения ответа(текстовый или изображение)
        public bool TextMode { get; set; }

        public bool ImageMode
        {
            get { return !TextMode; }
        }

        // Выбранный вариант при сопоставлении
        private int _selectedOption;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        // Текущее тестовое задание
        public TestPartViewModelBase CurrentTestPart { get; set; }

        // Варианты ответов при сопоставлении
        public ObservableCollection<AnswerOptionViewModel> Options { get; set; }
        public int SelectedOption
        {
            get => _selectedOption;
            set
            {
                _selectedOption = value;
                OnPropertyChanged(nameof(SelectedOption));
            }
        }
    }
}
