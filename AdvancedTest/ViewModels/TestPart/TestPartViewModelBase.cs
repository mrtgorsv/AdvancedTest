using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using AdvancedTest.Data.Enum;
using AdvancedTest.ViewModels.Answer;
using AdvancedTest.ViewModels.Base;
using AdvancedTest.ViewModels.Test;

namespace AdvancedTest.ViewModels.TestPart
{
    /// <summary>
    /// Базовая модель представления для формы задания
    /// </summary>
    public class TestPartViewModelBase : ViewModelBase
    {
        // Изображение описания задания
        private BitmapImage _testText;
        // Текущий выбранный ответ
        private string _currentAnswer;

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
            }
        }

        // Размер изображения по ширине
        public int ImageWidth => (int)TestText.Width;
        // Размер изображения по высоте
        public int ImageHeight => (int)TestText.Height;

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