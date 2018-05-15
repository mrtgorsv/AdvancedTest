using AdvancedTest.Utils.IoC;
using AdvancedTest.ViewModel;
using AdvancedTest.ViewModel.Login;

namespace AdvancedTest.Utils
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => IocKernel.Get<MainViewModel>();
        public LoginViewModel LoginViewModel => IocKernel.Get<LoginViewModel>();
    }
}