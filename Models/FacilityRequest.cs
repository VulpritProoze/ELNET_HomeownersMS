using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeownersMS.Models
{
    public class FacilityRequest
    {
        [Key]
        public int FacilityRequestId { get; set; }

        public DateTime? ReservationDate { get; set; } 

        public DateTime? RequestCompletionDate { get; set; }

        public Statuses? Status { get; set; }

        [ForeignKey("Resident")]
        public int? ResidentId { get; set; }

        [ForeignKey("Facility")]
        public int? FacilityId { get; set; }

        public virtual Resident? Resident { get; set; }
        public virtual Facility? Facility { get; set; }
    }
}
