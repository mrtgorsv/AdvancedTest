using System.Windows;
using AdvancedTest.Common.Utils;
using AdvancedTest.Common.ViewModels.Practice;
using AdvancedTest.Data.Enum;

namespace AdvancedTest.Practice.Client.Utils
{
    public class PracticeDataTemplateSelector : ContentDataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (container is FrameworkElement element && item is PracticeViewModel)
            {
                PracticeViewModel practiceViewModel = (PracticeViewModel) item;

                if (practiceViewModel.PracticeType == PracticeType.Excel)
                    return element.FindResource("ExcelPracticeView") as DataTemplate;
                if (practiceViewModel.PracticeType == PracticeType.Word)
                {
                    return element.FindResource("WordPracticeView") as DataTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}