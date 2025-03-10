using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeownersMS.Models
{
    public class Resident
    {
        [Key, ForeignKey("User")]
        public int UserId { get; set; }
        public string? LName { get; set; } 
        public string? FName { get; set; } 
        public string? Email { get; set; } 
        public string? ContactNo { get; set; } 
        public string? Address { get; set; } 
        public DateTime? MoveInDate { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();

        public Resident()
        {
            User = new User
            {
                Privilege = Privileges.resident,
                Resident = this
            };
        }
    }
}
