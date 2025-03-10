using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace HomeownersMS.Models
{
    public class Admin
    {
        [Key, ForeignKey("User")]
        public int UserId { get; set; }
        public string? LName { get; set; }
        public string? FName { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; } 
        public string? Job { get; set; }
        public DateTime? HireDate { get; set; }

        public required virtual User User { get; set; }

        public Admin()
        {
            if (!IsSeeding)
            {
                User = new User
                {
                    Privilege = Privileges.admin,
                    Admin = this
                };
            }
        }

        [NotMapped]
        public static bool IsSeeding { get; set; } = false;
    }
}
