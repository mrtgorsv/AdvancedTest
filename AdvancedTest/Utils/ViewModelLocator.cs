using AdvancedTest.Utils.IoC;
using AdvancedTest.ViewModels;
using AdvancedTest.ViewModels.Base;
using AdvancedTest.ViewModels.Login;
using AdvancedTest.ViewModels.Test;

namespace AdvancedTest.Utils
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => IocKernel.Get<MainViewModel>();
        public LoginViewModel LoginViewModel => IocKernel.Get<LoginViewModel>();
        public RegisterViewModel RegisterViewModel => IocKernel.Get<RegisterViewModel>();
        public TestViewModel TestViewModel => IocKernel.Get<TestViewModel>();
        public UserResultViewModel UserResultViewModel => IocKernel.Get<UserResultViewModel>();
    }
}