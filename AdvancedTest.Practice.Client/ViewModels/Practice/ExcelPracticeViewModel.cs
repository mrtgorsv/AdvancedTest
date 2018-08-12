using System.Collections.Generic;
using System.Linq;
using AdvancedTest.Common.Models;
using AdvancedTest.Common.ViewModels.Practice;
using AdvancedTest.Data.Enum;

namespace AdvancedTest.Practice.Client.ViewModels.Practice
{
    /// <summary>
    /// Модель представления для документов теории
    /// </summary>
    public partial class ExcelPracticeViewModel : PracticeViewModel
    {
        private string _buttonText;

        private List<ExcelCellValidation> _cellValidation;

        protected List<ExcelCellValidation> CellValidation
        {
            get { return _cellValidation ?? (_cellValidation = GetCellValidation().ToList()); }
        }


        public override string DocumentPath => $"2\\{SelectedOption + 1}";
        public override PracticeType PracticeType => PracticeType.Excel;

        public override string ButtonText
        {
            get => _buttonText;
            set
            {
                _buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }
        public override string SelectionDescription => Options[_selectedOption].Description;


        public ExcelPracticeViewModel()
        {
            _buttonText = "Начать";
        }

        private int _selectedOption;

        public override int SelectedOption
        {
            get => _selectedOption;
            set
            {
                _selectedOption = value;
                OnPropertyChanged(nameof(SelectedOption));
                OnPropertyChanged(nameof(SelectionDescription));
            }
        }

        private IEnumerable<ExcelCellValidation> GetCellValidation()
        {
            switch (SelectedOption)
            {
                case 0:
                    yield return new ExcelCellValidation { Cell = "G1", ExpectedValue = "=СРЗНАЧ(B61:B152)" };
                    yield return new ExcelCellValidation { Cell = "F2", ExpectedValue = "=СЧЁТЕСЛИ(B2:B264;\"Подгорный\")" };
                    yield return new ExcelCellValidation { Cell = "G2", ExpectedValue = "=F2/263*100" };
                    break;
                case 1:
                    yield return new ExcelCellValidation { Cell = "H2", ExpectedValue = "=СРЗНАЧ(B61:B152)" };
                    yield return new ExcelCellValidation { Cell = "H3", ExpectedValue = "=СУММЕСЛИ(E2:E366;\"Ю\";C2:C366)/СЧЁТЕСЛИ(E2:E366;\"Ю\")" };
                    break;
                case 2:
                    yield return new ExcelCellValidation { Cell = "H2", ExpectedValue = "=СРЗНАЧ(E199:E258)" };
                    yield return new ExcelCellValidation { Cell = "H3", ExpectedValue = "=СУММЕСЛИ(D2:D371;\"Информатика\";E2:E371)/СЧЁТЕСЛИ(D2:D371;\"Информатика\")" };
                    break;
                case 3:
                    yield return new ExcelCellValidation { Cell = "H2", ExpectedValue = "=СУММ(F2:F1001)" };
                    yield return new ExcelCellValidation { Cell = "H3", ExpectedValue = "=СРЗНАЧ(G2:G1001)" };
                    break;
                case 4:
                    yield return new ExcelCellValidation { Cell = "H2", ExpectedValue = "=СЧЁТЕСЛИ(F2:F1001; \">200\"" };
                    yield return new ExcelCellValidation { Cell = "H3", ExpectedValue = "=СУММЕСЛИ(E2:E1001;\">60\";C2:C1001)/СЧЁТЕСЛИ(E2:E1001;\">60\")" };
                    break;
                case 5:
                    yield return new ExcelCellValidation { Cell = "I2", ExpectedValue = "=СЧЁТЕСЛИ(G2:G1001;ИСТИНА)" };
                    yield return new ExcelCellValidation { Cell = "I3", ExpectedValue = "=СУММ(H2:H1001)/I2" };
                    break;
                case 6:
                    yield return new ExcelCellValidation { Cell = "H2", ExpectedValue = "=СУММ(F2:F1001)" };
                    yield return new ExcelCellValidation { Cell = "H3", ExpectedValue = "=СУММЕСЛИ(B2:B1001;\"0\";E2:E1001)/СЧЁТЕСЛИ(B2:B1001;\"0\")" };
                    break;
                case 7:
                    yield return new ExcelCellValidation { Cell = "H2", ExpectedValue = "=СРЗНАЧ(E102:E155)" };
                    yield return new ExcelCellValidation { Cell = "H3", ExpectedValue = "=СУММЕСЛИ(D2:D371;\"География\";E2:E371)/СЧЁТЕСЛИ(D2:D371;\"География\")" };
                    break;
                case 8:
                    yield return new ExcelCellValidation { Cell = "H2", ExpectedValue = "=СРЗНАЧ(C61:C152)" };
                    yield return new ExcelCellValidation { Cell = "H3", ExpectedValue = "=СУММЕСЛИ(E2:E366;\"ЮЗ\";F2:F366)/СЧЁТЕСЛИ(E2:E366;\"ЮЗ\")" };
                    break;
            }
        }
    }
}
