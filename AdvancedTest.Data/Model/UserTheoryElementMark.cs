﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvancedTest.Data.Model
{
    public class UserTheoryElementMark
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("TheoryPart")]
        public int TheoryPartId { get; set; }

        public bool IsCompleted { get; set; }

        public virtual User User { get; set; }

        public virtual TheoryPart TheoryPart { get; set; }
    }
}
