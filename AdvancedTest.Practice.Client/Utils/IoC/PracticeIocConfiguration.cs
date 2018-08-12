using AdvancedTest.Common.Utils.IoC;
using AdvancedTest.Common.ViewModels.Base;
using AdvancedTest.Common.ViewModels.Interfaces;
using AdvancedTest.Common.ViewModels.Test;
using AdvancedTest.Practice.Client.ViewModels;

namespace AdvancedTest.Practice.Client.Utils.IoC
{
    public class PracticeIocConfiguration : IocConfiguration
    {
        public override void Load()
        {
            base.Load();

            Bind<IMainWindowViewModel>().To<PracticeMainViewModel>().InTransientScope();
            Bind<TestViewModel>().ToSelf().InTransientScope();
            Bind<UserResultViewModel>().ToSelf().InTransientScope();
        }
    }
}