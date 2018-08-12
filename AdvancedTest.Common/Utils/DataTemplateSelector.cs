using System.Windows;
using System.Windows.Controls;

namespace AdvancedTest.Common.Utils
{
    public class ContentDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return base.SelectTemplate(item, container);
        }
    }
}