using Microsoft.Office.Interop.Excel;

namespace AdvancedTest.Common.Extensions
{
    public static class ExcelExtensions
    {
        public static Range Named(this Range cells, string cellName)
        {

            char cellLetter = cellName.Substring(0, 1).ToUpper()[0];
            int xCoordinate = (cellLetter - 'A') + 1;
            int yCoordinate = int.Parse(cellName.Substring(1));
            return cells[yCoordinate, xCoordinate];
        }
    }
}
