using System.Windows;
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
            IocKernel.Initialize(new IocConfiguration());

            base.OnStartup(e);
        }
    }
}
