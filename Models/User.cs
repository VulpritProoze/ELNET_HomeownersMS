using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeownersMS.Models
{
    public enum Privileges
    {
        resident, staff, admin
    }

    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public Privileges? Privilege { get; set; }

        public virtual Admin? Admin { get; set; }
        public virtual Staff? Staff { get; set; }
        public virtual Resident? Resident { get; set; }

        public ICollection<CommunityPost> CommunityPosts { get; set; } = new List<CommunityPost>();
        public ICollection<CommunityComment> CommunityComments { get; set; } = new List<CommunityComment>();

        public virtual ICollection<CommunityVote> CommunityVotes { get; set; } = new List<CommunityVote>();

        public virtual ICollection<Event> Events { get; set; } = new List<Event>();


        public void SetPassword(string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            PasswordHash = passwordHasher.HashPassword(this, password);
        }

        public bool VerifyPassword(string password)
        {
            if (PasswordHash != null)
            {
                var passwordHasher = new PasswordHasher<User>();
                var result = passwordHasher.VerifyHashedPassword(this, PasswordHash, password);
                return result == PasswordVerificationResult.Success;
            }
            return false;
        }
    }
}
