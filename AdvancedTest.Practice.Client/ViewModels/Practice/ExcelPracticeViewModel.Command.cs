using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using AdvancedTest.Common.EventArgs;
using AdvancedTest.Common.Extensions;
using AdvancedTest.Common.Utils;
using Microsoft.Office.Interop.Excel;

namespace AdvancedTest.Practice.Client.ViewModels.Practice
{
    /// <summary>
    /// Модель представления для документов теории
    /// </summary>
    public partial class ExcelPracticeViewModel
    {
        private Application _app = new Application();

        private Workbooks _wbs;
        private Workbook _wb;
        private Worksheet _worksheet;
        private Sheets _sheets;
        private Range _cells;

        // Функция запуска теста
        protected override void Start()
        {
            if (IsStarted)
            {
                Complete();
            }
            else
            {
                IsStarted = true;
                ButtonText = "Ожидание закрытия...";
                ShowStartDocument();
                StartTimer();
            }
        }

        // Функция завершения теста
        protected override void Complete()
        {
            StopTimer();
            try
            {
                bool passed = CheckResult();
                var result = new TestCompletedEventArgs(CurrentTheoryId, _elapsedTime, passed);
                OnTestCompleted(result);
            }
            catch (Exception ex)
            {
                var result = new TestCompletedEventArgs(CurrentTheoryId, _elapsedTime, false, ex.Message);
                OnTestFailed(result);
            }
        }

        private bool CheckResult()
        {
            _sheets = _wb.Worksheets;
            _worksheet = (Worksheet)_sheets["task19"];
            _cells = _worksheet.Cells;
            bool isValid = true;
            switch (SelectedOption)
            {
                case 0:
                    if (_cells.Named("G1").FormulaLocal != "=МАКС(E2:E264)")
                    {
                        isValid = false;
                    }
                    if (_cells.Named("F2").FormulaLocal != "=СЧЁТЕСЛИ(B2:B264;\"Подгорный\")")
                    {
                        isValid = false;
                    }
                    if (_cells.Named("G2").FormulaLocal != "=F2/263*100")
                    {
                        isValid = false;
                    }
                    break;
                case 1:
                    if (_cells.Named("H2").FormulaLocal != "=СРЗНАЧ(B61:B152)")
                    {
                        isValid = false;
                    }
                    if (_cells.Named("H3").FormulaLocal != "=СУММЕСЛИ(E2:E366;\"Ю\";C2:C366)/СЧЁТЕСЛИ(E2:E366;\"Ю\")")
                    {
                        isValid = false;
                    }
                    break;
                case 2:
                    if (_cells.Named("H2").FormulaLocal != "=СРЗНАЧ(E199:E258)")
                    {
                        isValid = false;
                    }
                    if (_cells.Named("H3").FormulaLocal != "=СУММЕСЛИ(D2:D371;\"Информатика\";E2:E371)/СЧЁТЕСЛИ(D2:D371;\"Информатика\")")
                    {
                        isValid = false;
                    }
                    break;
                default:
                    isValid = false;
                    break;
            }

            return isValid;
        }

        private void ShowStartDocument()
        {
            _app.Visible = true;
            _wbs = _app.Workbooks;
            _wb = _wbs.Open(Path.GetFullPath(PathResolver.GenerateExcelPracticePath(DocumentPath)));
            _wb.Activate();
            _wb.BeforeClose += OnBeforeCloseExcel;
        }

        private void OnBeforeCloseExcel(ref bool cancel)
        {
            _app.DisplayAlerts = false;
            _wb.BeforeClose -= OnBeforeCloseExcel;
            Complete();
        }

        public override void Dispose()
        {
            try
            {
                if (_app != null && _wb != null)
                {
                    _wb.Close(false);
                    _app.Quit();
                    _wbs.Close();
                    CloseExcelApp();
                    Marshal.FinalReleaseComObject(_cells);
                    Marshal.FinalReleaseComObject(_worksheet);
                    Marshal.FinalReleaseComObject(_sheets);
                    Marshal.FinalReleaseComObject(_wb);
                    Marshal.FinalReleaseComObject(_wbs);
                    Marshal.FinalReleaseComObject(_app);
                }
            }
            catch (Exception)
            {
                // ignored
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            base.Dispose();

        }

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        private void CloseExcelApp()
        {
            int hWnd = _app.Application.Hwnd;

            GetWindowThreadProcessId((IntPtr)hWnd, out var processID);
            Process.GetProcessById((int)processID).Kill();
            _cells = null;
            _worksheet = null;
            _sheets = null;
            _wb = null;
            _wbs = null;
            _app = null;
        }
    }
}