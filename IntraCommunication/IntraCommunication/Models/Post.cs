using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace IntraCommunication.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        [Key]
        [Column("PostID")]
        public int PostId { get; set; }
        [Required]
        [Column("Post_Description", TypeName = "text")]
        public string PostDescription { get; set; }
        [Required]
        [Column("Post_Type")]
        [StringLength(1)]
        public string PostType { get; set; }
        [Column("Posted_On")]
        public int PostedOn { get; set; }
        [Column(TypeName = "date")]
        public DateTime PostedAt { get; set; }
        public int PostedBy { get; set; }
        [Column("Start_Time", TypeName = "datetime")]
        public DateTime? StartTime { get; set; }
        [Column("End_Time", TypeName = "datetime")]
        public DateTime? EndTime { get; set; }
        [Column("URL", TypeName = "text")]
        public string Url { get; set; }

        [ForeignKey(nameof(PostedBy))]
        [InverseProperty(nameof(UserProfile.Posts))]
        public virtual UserProfile PostedByNavigation { get; set; }
        [InverseProperty(nameof(Comment.Post))]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
