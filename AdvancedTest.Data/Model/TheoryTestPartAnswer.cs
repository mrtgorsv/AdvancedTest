﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdvancedTest.Data.Model
{
    public partial class TheoryTestPartAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("TheoryTestPart")]
        public int TestPartId { get; set; }

        public bool IsCorrect { get; set; }

        public string Text { get; set; }

        public virtual TheoryTestPart TheoryTestPart { get; set; }
    }
}