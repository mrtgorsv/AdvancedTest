using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvancedTest.Data.Model
{
    public partial class TheoryDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("TheoryPart")]
        public int TheoryPartId { get; set; }
        public int Seq { get; set; }
        public string Name { get; set; }

        public string DocumentPath => $"\\{TheoryPart?.Seq}\\{Seq}";

        public bool IsVisible { get; set; }

        public virtual TheoryPart TheoryPart { get; set; }
    }
}
