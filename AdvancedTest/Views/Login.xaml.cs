using System;
using System.Windows;
using System.Windows.Controls;
using AdvancedTest.Utils;
using LoginViewModel = AdvancedTest.ViewModels.Login.LoginViewModel;

namespace AdvancedTest.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly ViewModelLocator _locator = new ViewModelLocator();

        private LoginViewModel _viewModel;

        private MainWindow _mainWindow;

        public Login()
        {
            InitializeComponent();
            ShowLoginWindow();
        }

        private void OnUserLogined(object sender, System.EventArgs args)
        {
            _mainWindow = new MainWindow();
            Hide();
            _mainWindow.Show();
            _mainWindow.UserLogout += OnUserLogout;
        }

        private void OnUserLogout(object sender, System.EventArgs args)
        {
            _mainWindow.UserLogout -= OnUserLogout;
            _mainWindow.Close();
            ShowLoginWindow();
        }

        private void ShowLoginWindow()
        {
            _viewModel = _locator.LoginViewModel;
            DataContext = _viewModel;
            _viewModel.UserLogin += OnUserLogined;
            Show();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var box = sender as PasswordBox;
            _viewModel.Password = box?.Password;
        }
    }
}