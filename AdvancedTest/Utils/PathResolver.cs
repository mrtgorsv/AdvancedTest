namespace AdvancedTest.Utils
{
    public static class PathResolver
    {
        private static string _testFolder = "Content/Tests";
        private static string _docFolder = "Content\\Docs";

        public static string GenerateTestImagePath(string theoryFolder , string testPartName)
        {
            return $"{_testFolder}/{theoryFolder}/{testPartName}.png";
        }
        public static string GenerateDocumentPath(string docPath)
        {
            return $"{_docFolder}\\{docPath}.pdf";
        }
        public static string GenerateAnswerImagePath(string theoryNum,string testNum, string answerNum)
        {
            return $"{_testFolder}/{theoryNum}/{testNum}/{answerNum}.png";
        }
    }
}
