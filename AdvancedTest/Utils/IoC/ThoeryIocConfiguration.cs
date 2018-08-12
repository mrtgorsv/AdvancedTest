using AdvancedTest.Common.Utils.IoC;
using AdvancedTest.Common.ViewModels;
using AdvancedTest.Common.ViewModels.Base;
using AdvancedTest.Common.ViewModels.Interfaces;
using AdvancedTest.Common.ViewModels.Test;

namespace AdvancedTest.Utils.IoC
{
    public class ThoeryIocConfiguration : IocConfiguration
    {
        public override void Load()
        {
            base.Load();

            Bind<IMainWindowViewModel>().To<MainViewModel>().InTransientScope();
            Bind<TestViewModel>().ToSelf().InTransientScope();
            Bind<UserResultViewModel>().ToSelf().InTransientScope();
        }
    }
}