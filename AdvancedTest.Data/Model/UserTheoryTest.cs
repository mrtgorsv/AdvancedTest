using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvancedTest.Data.Model
{
    public partial class UserTheoryTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("TheoryPart")]
        public int TheoryPartId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public int Attempt { get; set; }
        public double Result { get; set; }

        public virtual TheoryPart TheoryPart { get; set; }
        public virtual User User { get; set; }
    }
}
