using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeownersMS.Models
{
    public class FacilityReview
    {
        [Key]
        public int ReviewId { get; set; }

        // Foreign key to Facility
        [ForeignKey("Facility")]
        public int? FacilityId { get; set; }

        // Navigation property to Facility
        public virtual Facility? Facility { get; set; }

        [ForeignKey("Resident")]
        public int? ResidentId { get; set;}

        public virtual Resident? Resident { get; set;}

        // Review content
        public string? Content { get; set; }

        // Review date
        public DateTime ReviewDate { get; set; } = DateTime.Now;

        // Rating in stars (1 to 5)
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int? Rating { get; set; }
    }
}