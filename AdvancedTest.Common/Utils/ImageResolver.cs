using System;
using System.Windows.Media.Imaging;

namespace AdvancedTest.Common.Utils
{
    public static class ImageResolver
    {
        public static BitmapImage LoadImage(string path)
        {
            Uri.TryCreate(path, UriKind.Relative, out var uri);
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = uri;
            image.EndInit();
            return image;
        }
    }
}
