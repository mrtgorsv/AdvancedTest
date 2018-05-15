using AdvancedTest.Utils.IoC;
using AdvancedTest.ViewModel;
using AdvancedTest.ViewModel.Login;
using AdvancedTest.ViewModels;
using AdvancedTest.ViewModels.Test;

namespace AdvancedTest.Utils
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => IocKernel.Get<MainViewModel>();
        public LoginViewModel LoginViewModel => IocKernel.Get<LoginViewModel>();
        public TestViewModel TestViewModel => IocKernel.Get<TestViewModel>();
    }
}