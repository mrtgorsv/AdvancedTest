using System.Windows;
using AdvancedTest.Data.Model;

namespace AdvancedTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
