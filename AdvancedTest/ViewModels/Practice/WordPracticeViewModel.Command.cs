using System;
using System.IO;
using AdvancedTest.EventArgs;
using AdvancedTest.Properties;
using AdvancedTest.Utils;
using Microsoft.Office.Interop.Word;

namespace AdvancedTest.ViewModels.Practice
{
    /// <summary>
    /// Модель представления для документов теории
    /// </summary>
    public partial class WordPracticeViewModel : IDisposable
    {
        private Document _userFile;
        private Application _app = new Application();
        // Команда отображения формы результатов пользователя
        public DelegateCommand StartCommand { get; set; }

        // Команда выхода из приложения
        public DelegateCommand RulesCommand { get; set; }

        // Функция инициализации команд
        private void InitializeCommands()
        {
            StartCommand = new DelegateCommand(StartTest);
            RulesCommand = new DelegateCommand(ShowRules);
        }

        // Функция запуска теста
        protected override void StartTest()
        {
            if (IsStarted)
            {
                CompletePart();
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
        protected override void CompletePart()
        {
            StopTimer();
            try
            {
                var result = new TestCompletedEventArgs(CompareFiles(), CurrentTheoryId, -1, _elapsedTime, true);
                OnTestCompleted(result);
            }
            catch (Exception ex)
            {
                var result = new TestCompletedEventArgs(0, CurrentTheoryId, -1, _elapsedTime, false, ex.Message);
                OnTestFailed(result);
            }
        }

        private int CompareFiles()
        {
            if (_userFile != null)
            {
                _app.Visible = false;
                var resultFile = CreateDocument(Resources.result, _app);
                var compareDoc = _app.CompareDocuments(resultFile, _userFile);
                return compareDoc.Revisions.Count;
            }
            throw new NullReferenceException();
        }

        private void ShowStartDoc()
        {
            _userFile = CreateDocument(Resources.start , _app);
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
            CompletePart();
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

        public void Dispose()
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
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
            GC.Collect();
        }
    }
}
