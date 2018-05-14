using AdvancedTest.Data.Context;
using AdvancedTest.Service.Services.Implementation;
using AdvancedTest.Service.Services.Interface;
using AdvancedTest.ViewModel;
using Ninject.Modules;

namespace AdvancedTest.Utils.IoC
{
    class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<ITestService>().To<TestService>().InSingletonScope();
            Bind<ITheoryService>().To<TheoryService>().InSingletonScope();
            Bind<AppDbContext>().To<AppDbContext>().InSingletonScope();
            Bind<MainViewModel>().ToSelf().InTransientScope();
        }
    }
}