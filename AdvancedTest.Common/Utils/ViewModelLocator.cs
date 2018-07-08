using AdvancedTest.Common.Utils.IoC;
using AdvancedTest.Common.ViewModels;
using AdvancedTest.Common.ViewModels.Base;
using AdvancedTest.Common.ViewModels.Login;
using AdvancedTest.Common.ViewModels.Test;

namespace AdvancedTest.Common.Utils
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