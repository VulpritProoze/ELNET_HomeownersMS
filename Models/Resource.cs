// emptyusing System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeownersMS.Models
{
    public class Resource
    {
        [Key]
        public int ResourceId { get; set; }

        public string? Title { get; set; } 

        public string? Content { get; set; }

        [ForeignKey("Admin")]
        public int CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public required virtual Admin Admin { get; set; }
    }
}
