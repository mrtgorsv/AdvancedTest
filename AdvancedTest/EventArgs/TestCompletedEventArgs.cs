namespace AdvancedTest.EventArgs
{
    public class TestCompletedEventArgs : System.EventArgs
    {
        public double TestResult { get; set; }
        public int TheoryId { get; set; }
        public int TestAttempt { get; set; }
        public TestCompletedEventArgs()
        {
        }
        public TestCompletedEventArgs(double result, int theoryId , int testAttempt)
        {
            TestResult = result;
            TheoryId = theoryId;
            TestAttempt = testAttempt;
        }
    }
}
