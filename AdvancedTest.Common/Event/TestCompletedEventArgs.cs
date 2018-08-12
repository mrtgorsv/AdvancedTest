using System;

namespace AdvancedTest.Common.Event
{
    /// <summary>
    /// Событие завершения теста
    /// </summary>
    public class TestCompletedEventArgs : EventArgs
    {
        public double TestResult { get; set; }
        public TimeSpan ElapsedTimeSpan { get; set; }

        public int TheoryId { get; set; }
        public int TestAttempt { get; set; }

        public bool Complete { get; set; }

        public string Error { get; set; }
        public string Message { get; set; }

        public TestCompletedEventArgs()
        {
        }
        public TestCompletedEventArgs(int theoryId , string message, bool complete = true)
        {
            TheoryId = theoryId;
            Message = message;
            Complete = complete;
        }

        public TestCompletedEventArgs(int theoryId, TimeSpan elapsedTime, bool complete = true, string error = null)
        {
            TheoryId = theoryId;
            Complete = complete;
            Error = error;
        }


        public TestCompletedEventArgs(double result, int theoryId, int testAttempt, TimeSpan elapsedTime, bool complete = true, string error = null)
        {
            TestResult = result;
            TheoryId = theoryId;
            TestAttempt = testAttempt;
            Complete = complete;
            Error = error;
            ElapsedTimeSpan = elapsedTime;
        }
    }
}