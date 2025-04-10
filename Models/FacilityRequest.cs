using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeownersMS.Models // ← ADD THIS
{
    public enum RequestStatus
    {
        Pending,
        Approved,
        Declined,
        Cancelled
    }

    public class FacilityRequest
    {
        [Key]
        public int FacilityRequestId { get; set; }


        // Time the facility request was made (doesn't mean it's the time the event will be held)
        public DateTime RequestDate { get; set; } = DateTime.Now;

        public DateTime? ApprovalDate { get; set; }

        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        [ForeignKey("Resident")]
        public int ResidentId { get; set; }

        [ForeignKey("Facility")]
        public int FacilityId { get; set; }

        public string? FullName { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }

        public virtual Resident? Resident { get; set; }
        public virtual Facility? Facility { get; set; }
    }
}
