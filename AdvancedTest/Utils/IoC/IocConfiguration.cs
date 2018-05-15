using AdvancedTest.Data.Context;
using AdvancedTest.Service.Services.Implementation;
using AdvancedTest.Service.Services.Interface;
using AdvancedTest.ViewModel;
using AdvancedTest.ViewModel.Login;
using Ninject.Modules;

namespace AdvancedTest.Utils.IoC
{
    class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<AppDbContext>().To<AppDbContext>().InSingletonScope();

            Bind<ISecurityManager>().To<SecurityManager>().InSingletonScope();

            Bind<ITestService>().To<TestService>().InSingletonScope();
            Bind<IUserService>().To<UserService>().InSingletonScope();
            Bind<ITheoryService>().To<TheoryService>().InSingletonScope();

            Bind<MainViewModel>().ToSelf().InTransientScope();
            Bind<LoginViewModel>().ToSelf().InTransientScope();
        }
    }
}