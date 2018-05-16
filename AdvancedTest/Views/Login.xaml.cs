using System;
using System.Windows;
using System.Windows.Controls;
using AdvancedTest.Utils;
using AdvancedTest.ViewModel.Login;

namespace AdvancedTest.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly ViewModelLocator _locator = new ViewModelLocator();


        private readonly LoginViewModel _viewModel;

        public Login()
        {
            InitializeComponent();
            _viewModel = _locator.LoginViewModel;
            DataContext = _viewModel;
            _viewModel.UserLogin += OnUserLogined;
        }

        private void OnUserLogined(object sender, System.EventArgs args)
        {
            MainWindow mainWindow = new MainWindow();
            Hide();
            mainWindow.Show();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var box = sender as PasswordBox;
            _viewModel.Password = box?.Password;
        }
    }
}