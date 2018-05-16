using System.ComponentModel.DataAnnotations.Schema;

namespace AdvancedTest.Data.Model
{
    public class UserTheoryDocumentMark : UserTheoryElementMark
    {
        [ForeignKey("Document")]
        public int? DocumentId { get; set; }
        public virtual TheoryDocument Document { get; set; }
    }
}
