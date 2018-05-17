using System;
using System.Windows.Media.Imaging;

namespace AdvancedTest.ViewModels.Answer
{
    public class ImageAnswerViewModel : AnswerViewModel
    {
        private BitmapImage _answerImage;
        private string _imagePath;

        public ImageAnswerViewModel()
        {
            TextMode = false;
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                LoadTestTextImage();
            }
        }

        public BitmapImage AnswerImage
        {
            get => _answerImage;
            set
            {
                _answerImage = value;
                OnPropertyChanged(nameof(AnswerImage));
            }
        }

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