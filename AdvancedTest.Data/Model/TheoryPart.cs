using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvancedTest.Data.Model
{
    public partial class TheoryPart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Seq { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TestTime { get; set; }

        public bool IsLast { get; set; }

        public virtual ICollection<TheoryTestPart> TheoryTestParts { get; set; }
        public virtual ICollection<TheoryDocument> TheoryDocuments { get; set; }
    }
}
