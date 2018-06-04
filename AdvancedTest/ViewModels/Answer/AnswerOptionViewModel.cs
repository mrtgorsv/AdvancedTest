using AdvancedTest.ViewModels.Base;

namespace AdvancedTest.ViewModels.Answer
{
    /// <summary>
    /// Модель представления для вариантов ответа у ответа на тестовое задание
    /// </summary>
    public class AnswerOptionViewModel : ViewModelBase
    {
        // Флаг, указывающий, что этот вариант выбран
        private bool _isSelected;
        // Значение варианта ответа
        public int Value { get; set; }
        // Название
        public string Name { get; set; }
        // Название
        public string Description { get; set; }

        public AnswerOptionViewModel(string answer , int index)
        {
            Name = answer;
            Value = index;
        }
        public AnswerOptionViewModel(string answer, int index ,string description)
        {
            Name = answer;
            Value = index;
            Description = description;
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
    }
}