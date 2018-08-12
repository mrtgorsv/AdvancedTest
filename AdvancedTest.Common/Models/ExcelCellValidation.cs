using AdvancedTest.Common.Extensions;
using Microsoft.Office.Interop.Excel;

namespace AdvancedTest.Common.Models
{
    public class ExcelCellValidation
    {
        public string Cell { get; set; }
        public string ExpectedValue { get; set; }

        public bool Validate(Range cells)
        {
            return cells.Named(Cell).FormulaLocal == ExpectedValue;
        }
    }
}
