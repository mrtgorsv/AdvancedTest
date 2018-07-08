using System;
using System.Windows;
using System.Windows.Controls;
using AdvancedTest.Common.Utils;
using AdvancedTest.Common.ViewModels.Login;

namespace AdvancedTest.Views
{
    /// <summary>
    /// Логика взаимодействия для RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        private readonly ViewModelLocator _locator = new ViewModelLocator();

        public event UserCreateEventHandler UserCreate;
        public delegate void UserCreateEventHandler(object sender, EventArgs args);

        private RegisterViewModel _viewModel;

        public RegisterView()
        {
            InitializeComponent();
            ShowRegisterWindow();
        }
        private void ShowRegisterWindow()
        {
            _viewModel = _locator.RegisterViewModel;
            DataContext = _viewModel;
            _viewModel.UserCreate += OnUserCreated;
            Show();
        }

        private void OnUserCreated(object sender, EventArgs args)
        {
            RaiseUserCreated();
        }

        private void RaiseUserCreated()
        {
            UserCreate?.Invoke(this, new EventArgs());
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var box = sender as PasswordBox;
            _viewModel.Password = box?.Password;
        }

        private void OnRepeatPasswordChanged(object sender, RoutedEventArgs e)
        {
            var box = sender as PasswordBox;
            _viewModel.RepeatPassword = box?.Password;
        }

    }
}
