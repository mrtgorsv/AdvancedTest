using System;
using System.IO;
using AdvancedTest.Common.Event;
using AdvancedTest.Common.Utils;
using AdvancedTest.Data.Enum;
using AdvancedTest.Practice.Client.Properties;
using Microsoft.Office.Interop.Word;

namespace AdvancedTest.Practice.Client.ViewModels.Practice
{
    /// <summary>
    /// Модель представления для документов теории
    /// </summary>
    public partial class WordPracticeViewModel
    {
        private Document _userFile;
        private bool _complete;
        private Application _app = new Application();
        private int _testResult;

        // Функция инициализации команд
        private void InitializeCommands()
        {
            StartCommand = new DelegateCommand(Start);
            RulesCommand = new DelegateCommand(ShowRules);
        }

        public override bool CanStart => (!IsStarted || !_complete) && CanEdit;

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
                ButtonText = "Проверить";
                ShowStartDoc();
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
                var result = GetError(ex);
                OnTestFailed(result);
            }

            _complete = true;
            ButtonText = "Завершено";
            OnPropertyChanged(nameof(CanStart));
        }

        private void ShowStartDoc()
        {
            _userFile = CreateDocument(StartDocument, _app);
            _app.DocumentBeforeClose += OnBeforeClose;
            ((DocumentEvents2_Event) _userFile).Close += OnUserDocumentClose;
            _app.Visible = true;
            _userFile.Activate();
        }

        private void OnBeforeClose(Document doc, ref bool cancel)
        {
            if (_app != null)
            {
                _app.DocumentBeforeClose -= OnBeforeClose;
                if (doc.FullName == _userFile.FullName)
                {
                    CompareFiles();
                }
            }
        }

        private void OnUserDocumentClose()
        {
            Complete();
        }

        private void ShowRules()
        {
            var rules = File.ReadAllBytes(Path.GetFullPath(PathResolver.GenerateWordPracticePath(DocumentPath, Resources.DocExtenstion)));
            OpenFile(rules);
        }

        private void OpenFile(byte[] file)
        {
            Application app = new Application {Visible = true};
            Document doc = CreateDocument(file , app);
            doc.Activate();
        }

        private Document CreateDocument(byte[] file , Application app)
        {
            var tmpFile = Path.GetTempFileName();
            var tmpFileStream = File.OpenWrite(tmpFile);
            tmpFileStream.Write(file, 0, file.Length);
            tmpFileStream.Close();
            Document doc = app.Documents.Open(tmpFile);
            return doc;
        }

        public override void Dispose()
        {
            try
            {
                if (_app != null)
                {
                    _app.Quit(WdSaveOptions.wdDoNotSaveChanges);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(_app);
                    _app = null;
                }
                if (_userFile != null)
                {
                    _userFile.Close(WdSaveOptions.wdDoNotSaveChanges);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(_userFile);
                    _userFile = null;
                }
            }
            catch (Exception)
            {
                //
            }
            GC.Collect();
            base.Dispose();
        }

        protected override TestCompletedEventArgs GetError(Exception ex)
        {
            return new TestCompletedEventArgs
            {
                TheoryId = CurrentTheoryId,
                Error = ex.Message,
                Complete = false
            };
        }

        protected override TestCompletedEventArgs GetSuccessResult()
        {
            _testResult = CompareFiles();

            return new TestCompletedEventArgs
            {
                TestAttempt = -1,
                TheoryId = CurrentTheoryId,
                Message = GetResultMessage(),
                Complete = true
            };
        }

        protected override string GetResultMessage()
        {
            var grade = GetGrade();

            var pointMessage = string.Format(Resources.PointMessage, (int)grade);
            var baseMessage = string.Format(Resources.PracticeCompleteTemplateMessage, _elapsedTime.ToString("T"));

            if (_testResult != 0)
            {
               return $"{baseMessage} {string.Format(Resources.PracticeFailedTemplateMessage, _testResult)} {pointMessage}";
            } 
            return $"{baseMessage} {Resources.PracticeSuccessTemplateMessage} {pointMessage}";
        }

        protected override Grade GetGrade()
        {
            switch (_testResult)
            {

                case int value when value == 0:
                {
                    return CheckTotalMinutes(20) ? Grade.A : Grade.B;
                }
                case int value when value <= 2:
                {
                    return CheckTotalMinutes(23) ? Grade.B : Grade.C;
                }
                case int value when value <= 3:
                {
                    return CheckTotalMinutes(25) ? Grade.C : Grade.D;
                }
                default:
                    return Grade.E;
            }
        }

        private bool CheckTotalMinutes(int minutes)
        {
            return _elapsedTime.TotalMinutes < minutes;
        }

        private int CompareFiles()
        {
            if (_userFile != null)
            {
                _app.Visible = false;
                var resultFile = CreateDocument(ResultDocument, _app);
                var compareDoc = _app.CompareDocuments(resultFile, _userFile);
                return compareDoc.Revisions.Count;
            }
            throw new NullReferenceException();
        }
    }
}
