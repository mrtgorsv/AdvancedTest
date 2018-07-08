using System.Windows;
using AdvancedTest.Common.Utils;
using AdvancedTest.Common.ViewModels;
using AdvancedTest.Common.ViewModels.Test;
using AdvancedTest.Common.ViewModels.Theory;
using AdvancedTest.Utils;

namespace AdvancedTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public event UserLogoutEventHandler UserLogout;
        public delegate void UserLogoutEventHandler(object sender, System.EventArgs args);

        private readonly ViewModelLocator _locator = new ViewModelLocator();
        private readonly MainViewModel _currentModel;
        public MainWindow()
        {
            InitializeComponent();
            _currentModel = _locator.MainViewModel;
            DataContext = _currentModel;
            _currentModel.UserLogout += OnUserLogout;
        }

        private void OnUserLogout(object sender, System.EventArgs args)
        {
            UserLogout?.Invoke(sender , args);
        }

        private void OnSelectedTheoryPartChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            _currentModel.ClearSelection();
            if (e.NewValue is TestViewModel theoryPart)
            {
                _currentModel.ShowTheoryTest(theoryPart);
            }
            else if (e.NewValue is DocumentViewModel theoryDoc)
            {
                _currentModel.ShowDocument(theoryDoc);
            }
//            else if (e.NewValue is WordPracticeViewModel wordPracticeViewModel)
//            {
//                _currentModel.ShowWordPractice(wordPracticeViewModel);
//            }
//            else if (e.NewValue is ExcelPracticeViewModel excelPracticeViewModel)
//            {
//                _currentModel.ShowExcelPractice(excelPracticeViewModel);
//            }
        }

        protected override void OnClosed(System.EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }
    }
}
