// emptyusing System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeownersMS.Models
{
    public class CommunityComment
    {
        [Key]
        public int CommunityCommentId { get; set; }

        public string? Content { get; set; } 

        public DateTime? CreatedAt { get; set; }

        [ForeignKey("CommunityPost")]
        public int CommunityPostId { get; set; }


        public required virtual CommunityPost CommunityPost { get; set; }
    }
}
