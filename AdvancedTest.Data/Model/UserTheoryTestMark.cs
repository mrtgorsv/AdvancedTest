using System;

namespace AdvancedTest.Data.Model
{
    public class UserTheoryTestMark : UserTheoryElementMark
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int Attempt { get; set; }
        public int? OptionId { get; set; }
        public double Result { get; set; }
    }
}
