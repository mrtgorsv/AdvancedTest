using System.Windows;
using AdvancedTest.Data.Model;
using AdvancedTest.Utils;

namespace AdvancedTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewModelLocator _locator = new ViewModelLocator();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _locator.MainViewModel;
        }

        private void OnSelectedTheoryPartChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is TheoryPart theoryPart)
            {

            }
            else
            {
                var item = e.NewValue as TheoryDocument;
            }
        }
    }
}
