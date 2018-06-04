namespace AdvancedTest.Utils
{
    public static class PathResolver
    {
        private static string _testFolder = "Content/Tests";
        private static string _docFolder = "Content\\Docs";
        private static string _practiceFolder = "Content\\Works";

        public static string GenerateTestImagePath(string theoryFolder , string testPartName)
        {
            return $"{_testFolder}/{theoryFolder}/{testPartName}.png";
        }
        public static string GenerateDocumentPath(string docPath , string extension = "pdf")
        {
            return $"{_docFolder}\\{docPath}.{extension}";
        }
        public static string GenerateWordPracticePath(string docPath, string extension = "docx")
        {
            return $"{_practiceFolder}\\{docPath}.{extension}";
        }
        public static string GenerateExcelPracticePath(string docPath, string extension = "xls")
        {
            return $"{_practiceFolder}\\{docPath}.{extension}";
        }
        public static string GenerateAnswerImagePath(string theoryNum,string testNum, string answerNum)
        {
            return $"{_testFolder}/{theoryNum}/{testNum}/{answerNum}.png";
        }
    }
}
