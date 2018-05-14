using AdvancedTest.Utils.IoC;
using AdvancedTest.ViewModel;

namespace AdvancedTest.Utils
{
    internal class ViewModelLocator
    {
        public MainViewModel MainViewModel => IocKernel.Get<MainViewModel>();
    }
}