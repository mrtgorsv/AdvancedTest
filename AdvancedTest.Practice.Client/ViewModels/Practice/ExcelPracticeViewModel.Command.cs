using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using AdvancedTest.Common.Event;
using AdvancedTest.Common.Utils;
using AdvancedTest.Data.Enum;
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
                OnTestCompleted(GetSuccessResult());
            }
            catch (Exception ex)
            {
                OnTestFailed(GetError(ex));
            }
        }

        private bool IsValid()
        {
            _sheets = _wb.Worksheets;
            _worksheet = (Worksheet)_sheets[1];
            _cells = _worksheet.Cells;

            return CellValidation.All(cv => cv.Validate(_cells));
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
            ButtonText = "Завершено";
        }

        protected override Grade GetGrade()
        {
            if (!IsValid())
            {
                return Grade.E;
            }

            var minutes = Convert.ToInt32(_elapsedTime.TotalMinutes);

            switch (minutes)
            {
                case int value when value <= 12:
                {
                    return Grade.A;
                }
                case int value when value <= 15:
                {
                    return Grade.B;
                }
                case int value when value <= 18:
                {
                    return Grade.C;
                }
                case int value when value > 18:
                {
                    return Grade.D;
                }
                default:
                    return Grade.E;
            }
        }

        protected override string GetResultMessage()
        {
            var grade = GetGrade();
            switch (grade)
            {
                case Grade.E:
                    return "В решении допущена ошибка, попробоуйте выполнить задание в следующий раз";
                case Grade.D:
                    return "Задания выполнены верно. Превышен лимит времени на выполнение заданий, попробуйте выполнить задание в следующий раз";
            }

            return $"Задания выполнены верно. Время выполнения: {_elapsedTime.TotalMinutes} минут. Ваша оценка {(int) grade}";
        }

        protected override TestCompletedEventArgs GetError(Exception ex)
        {
            return new TestCompletedEventArgs
            {
                TheoryId = CurrentTheoryId,
                Complete = false,
                Error = ex.Message
            };
        }

        protected override TestCompletedEventArgs GetSuccessResult()
        {
            return new TestCompletedEventArgs
            {
                Message = GetResultMessage(),
                TheoryId = CurrentTheoryId,
                Complete = true
            };
        }

        #region IDisposable

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

                    if (_cells != null)
                    {
                        Marshal.FinalReleaseComObject(_cells);
                    }
                    if (_worksheet != null)
                    {
                        Marshal.FinalReleaseComObject(_worksheet);
                    }
                    if (_sheets != null)
                    {
                        Marshal.FinalReleaseComObject(_sheets);
                    }
                    if (_wb != null)
                    {
                        Marshal.FinalReleaseComObject(_wb);
                    }
                    if (_wbs != null)
                    {
                        Marshal.FinalReleaseComObject(_wbs);
                    }
                    if (_app != null)
                    {
                        Marshal.FinalReleaseComObject(_app);
                    }
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

        #endregion
    }
}