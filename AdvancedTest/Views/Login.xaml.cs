using System;
using System.Windows;
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

        private void OnUserLogined(object sender, EventArgs args)
        {
            MainWindow mainWindow = new MainWindow();
            Hide();
            mainWindow.Show();
        }
    }
}