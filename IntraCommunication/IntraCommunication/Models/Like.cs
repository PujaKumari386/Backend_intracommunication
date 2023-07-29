using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace IntraCommunication.Models
{
    [Index(nameof(UserId), nameof(PostId), Name = "UX_likes_table_user_post", IsUnique = true)]
    public partial class Like
    {
        [Column("PostID")]
        public int PostId { get; set; }
        public int UserId { get; set; }
        [Key]
        [Column("LikeID")]
        public int LikeId { get; set; }
    }
}
