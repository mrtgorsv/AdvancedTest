using System;
using System.Windows;
using AdvancedTest.Data.Model;
using AdvancedTest.Utils;
using AdvancedTest.ViewModels;
using AdvancedTest.ViewModels.Test;
using AdvancedTest.ViewModels.Theory;

namespace AdvancedTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly ViewModelLocator _locator = new ViewModelLocator();
        private readonly MainViewModel _currentModel;
        public MainWindow()
        {
            InitializeComponent();
            _currentModel = _locator.MainViewModel;
            DataContext = _currentModel;
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
        }

        protected override void OnClosed(System.EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();
        }
    }
}
