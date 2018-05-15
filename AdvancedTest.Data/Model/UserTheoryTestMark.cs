using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvancedTest.Data.Model
{
    public class UserTheoryTestMark : UserTheoryElementMark
    {
        [ForeignKey("TheoryPart")]
        public int TheoryPartId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Attempt { get; set; }
        public double Result { get; set; }

        public virtual TheoryPart TheoryPart { get; set; }
    }
}
