using AdvancedTest.Common.ViewModels.Login;
using AdvancedTest.Data.Context;
using AdvancedTest.Service.Services.Implementation;
using AdvancedTest.Service.Services.Interface;
using Ninject.Modules;

namespace AdvancedTest.Common.Utils.IoC
{
    public class IocConfiguration : NinjectModule
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

            Bind<LoginViewModel>().ToSelf().InTransientScope();
            Bind<RegisterViewModel>().ToSelf().InTransientScope();
        }
    }
}