using AdvancedTest.Utils.IoC;
using AdvancedTest.ViewModel;

namespace AdvancedTest.Utils
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => IocKernel.Get<MainViewModel>();
    }
}