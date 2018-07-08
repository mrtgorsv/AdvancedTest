using System;
using System.Windows.Media.Imaging;

namespace AdvancedTest.Common.ViewModels.Answer
{
    /// <summary>
    /// Модель представления для вариантов ответа тестового задания типа изображение
    /// </summary>
    public class ImageAnswerViewModel : AnswerViewModel
    {
        private BitmapImage _answerImage;
        private string _imagePath;

        public ImageAnswerViewModel()
        {
            TextMode = false;
        }

        // Относительный путь к изображению
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                LoadTestTextImage();
            }
        }
        // Изображение ответа
        public BitmapImage AnswerImage
        {
            get => _answerImage;
            set
            {
                _answerImage = value;
                OnPropertyChanged(nameof(AnswerImage));
            }
        }

        // Загрузка изображения в память
        private void LoadTestTextImage()
        {
            Uri.TryCreate(_imagePath, UriKind.Relative, out var uri);
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = uri;
            image.EndInit();
            AnswerImage = image;
        }
    }
}