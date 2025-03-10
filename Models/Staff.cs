using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeownersMS.Models
{
    public class Staff
    {
        [Key, ForeignKey("User")]
        public int UserId { get; set; }
        public string? LName { get; set; }
        public string? FName { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public string? Job { get; set; }
        public DateTime? HireDate { get; set; }
        public virtual User User { get; set; }

        public ICollection<Service> Services { get; set; } = new List<Service>();

        public Staff()
        {
            User = new User
            {
                Privilege = Privileges.staff,
                Staff = this
            };
        }
    }
}
