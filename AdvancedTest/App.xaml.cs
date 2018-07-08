using System.Windows;
using AdvancedTest.Common.Utils.IoC;
using AdvancedTest.Utils.IoC;

namespace AdvancedTest
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IocKernel.Initialize(new ThoeryIocConfiguration());

            base.OnStartup(e);
        }
    }
}
