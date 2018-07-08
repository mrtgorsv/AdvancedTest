using System;

namespace AdvancedTest.Common.EventArgs
{
    /// <summary>
    /// Событие завершения теста
    /// </summary>
    public class TestCompletedEventArgs : System.EventArgs
    {
        public double TestResult { get; set; }
        public int TheoryId { get; set; }
        public int TestAttempt { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
        public TimeSpan ElapsedTimeSpan { get; set; }

        public TestCompletedEventArgs()
        {
        }

        public TestCompletedEventArgs(int theoryId, TimeSpan elapsedTime, bool success = true, string error = null)
        {
            TheoryId = theoryId;
            Success = success;
            Error = error;
            ElapsedTimeSpan = elapsedTime;
        }

        public TestCompletedEventArgs(double result, int theoryId, int testAttempt, TimeSpan elapsedTime, bool success = true,
            string error = null)
        {
            TestResult = result;
            TheoryId = theoryId;
            TestAttempt = testAttempt;
            Success = success;
            Error = error;
            ElapsedTimeSpan = elapsedTime;
        }
    }
}