using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using AdvancedTest.Common.ViewModels.Answer;
using AdvancedTest.Common.ViewModels.Base;
using AdvancedTest.Common.ViewModels.Test;
using AdvancedTest.Data.Enum;

namespace AdvancedTest.Common.ViewModels.TestPart
{
    /// <summary>
    /// Базовая модель представления для формы задания
    /// </summary>
    [Serializable]
    public class TestPartViewModelBase : ViewModelBase
    {
        // Изображение описания задания
        private BitmapImage _testText;
        // Текущий выбранный ответ
        private string _currentAnswer;
        private string _description;

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(TextMode));
            }
        }
        public bool TextMode => !string.IsNullOrWhiteSpace(_description);
        public bool ImageMode => _testText != null;


        // Проверка на правильность ответа
        public bool IsValid
        {
            get { return string.Compare(CorrectAnswer, GetUserAnswer(), StringComparison.InvariantCulture) == 0; }
        }

        // Изображение описания задания
        public BitmapImage TestText
        {
            get => _testText;
            set
            {
                _testText = value;
                OnPropertyChanged(nameof(TestText));
                OnPropertyChanged(nameof(ImageWidth));
                OnPropertyChanged(nameof(ImageHeight));
                OnPropertyChanged(nameof(ImageMode));
            }
        }

        // Размер изображения по ширине
        public int ImageWidth => Convert.ToInt32(TestText?.Width ?? 0);
        // Размер изображения по высоте
        public int ImageHeight => Convert.ToInt32(TestText?.Height ?? 0);

        // Текущий тест
        public TestViewModel CurrentTest { get; set; }

        // Порядок задания в тесте
        public int Seq { get; set; }

        // Тип задания
        public TestPartType TestPartType { get; set; }

        // Правильный ответ
        public string CorrectAnswer { get; set; }

        // Коллекция ответов задания
        public ObservableCollection<AnswerViewModel> Answers { get; set; }

        // Функция получения ответа от пользователя
        public virtual string GetUserAnswer()
        {
            throw new NotSupportedException();
        }
    }
}