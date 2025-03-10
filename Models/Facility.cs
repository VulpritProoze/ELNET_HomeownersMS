// emptyusing System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeownersMS.Models
{
    public class Facility
    {
        [Key]
        public int FacilityId { get; set; }

        public string? Name { get; set; } 

        public string? Description { get; set; }

        public float? PricePerHour { get; set; }
    }
}
