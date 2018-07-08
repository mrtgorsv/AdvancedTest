using AdvancedTest.Common.Utils;
using AdvancedTest.Common.Utils.IoC;
using AdvancedTest.Common.ViewModels;
using AdvancedTest.Common.ViewModels.Base;
using AdvancedTest.Common.ViewModels.Login;
using AdvancedTest.Common.ViewModels.Test;
using AdvancedTest.Data.Context;
using AdvancedTest.Service.Services.Implementation;
using AdvancedTest.Service.Services.Interface;

namespace AdvancedTest.Utils.IoC
{
    public class ThoeryIocConfiguration : IocConfiguration
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
            Bind<RegisterViewModel>().ToSelf().InTransientScope();
            Bind<TestViewModel>().ToSelf().InTransientScope();
            Bind<UserResultViewModel>().ToSelf().InTransientScope();
        }
    }
}