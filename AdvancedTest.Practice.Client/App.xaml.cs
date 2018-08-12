using System.Windows;
using AdvancedTest.Common.Utils.IoC;
using AdvancedTest.Practice.Client.Utils.IoC;

namespace AdvancedTest.Practice.Client
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IocKernel.Initialize(new PracticeIocConfiguration());

            base.OnStartup(e);
        }
    }
}
