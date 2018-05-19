using AdvancedTest.Data.Context;
using AdvancedTest.Service.Services.Implementation;
using AdvancedTest.Service.Services.Interface;
using AdvancedTest.ViewModels;
using AdvancedTest.ViewModels.Base;
using AdvancedTest.ViewModels.Test;
using Ninject.Modules;
using LoginViewModel = AdvancedTest.ViewModels.Login.LoginViewModel;

namespace AdvancedTest.Utils.IoC
{
    class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<AppDbContext>().To<AppDbContext>().InSingletonScope();
            Bind<ViewModelLocator>().To<ViewModelLocator>().InSingletonScope();

            Bind<ISecurityManager>().To<SecurityManager>().InSingletonScope();

            Bind<ITestService>().To<TestService>().InSingletonScope();
            Bind<IUserService>().To<UserService>().InSingletonScope();
            Bind<ITheoryService>().To<TheoryService>().InSingletonScope();
            Bind<IDocumentService>().To<DocumentService>().InSingletonScope();

            Bind<MainViewModel>().ToSelf().InTransientScope();
            Bind<LoginViewModel>().ToSelf().InTransientScope();
            Bind<TestViewModel>().ToSelf().InTransientScope();
            Bind<UserResultViewModel>().ToSelf().InTransientScope();
        }
    }
}