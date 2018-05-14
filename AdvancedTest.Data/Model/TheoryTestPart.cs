using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvancedTest.Data.Model
{
    public partial class TheoryTestPart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("TheoryPart")]
        public int TheoryId { get; set; }

        public int Seq { get; set; }
        public string Description { get; set; }

        public virtual TheoryPart TheoryPart { get; set; }

        public virtual ICollection<TheoryTestPartAnswer> Answers { get; set; }
    }
}
