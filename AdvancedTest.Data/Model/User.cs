using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvancedTest.Data.Model
{
    public partial class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UserTheoryTestMark> UserTheoryTests { get; set; }
        public virtual ICollection<UserTheoryDocumentMark> UserTheoryDocumentMarks { get; set; }
    }
}
