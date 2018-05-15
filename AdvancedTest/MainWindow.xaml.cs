using System.Windows;
using AdvancedTest.Data.Model;
using AdvancedTest.Utils;

namespace AdvancedTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
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
                ShowTheoryTest(theoryPart.Id);
            }
            else if (e.NewValue is TheoryDocument theoryDoc)
            {
                ShowTheoryDoc(theoryDoc.Id);
            }
        }

        private void ShowTheoryDoc(int theoryDocId)
        {
            throw new System.NotImplementedException();
        }

        private void ShowTheoryTest(int theoryPartId)
        {
            throw new System.NotImplementedException();
        }
    }
}
