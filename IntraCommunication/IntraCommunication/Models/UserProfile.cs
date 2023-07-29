using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace IntraCommunication.Models
{
    [Table("User_Profile")]
    [Index(nameof(Contact), Name = "UK_User_Profile_Contact", IsUnique = true)]
    [Index(nameof(Email), Name = "UK_User_Profile_Email", IsUnique = true)]
    public partial class UserProfile
    {
        public UserProfile()
        {
            GroupInvitesRequestCreatedByNavigations = new HashSet<GroupInvitesRequest>();
            GroupInvitesRequestSentToNavigations = new HashSet<GroupInvitesRequest>();
            GroupMembers = new HashSet<GroupMember>();
            Groups = new HashSet<Group>();
            Posts = new HashSet<Post>();
        }

        [Key]
        [Column("UserID")]
        public int UserId { get; set; }
        [Required]
        [Column("First_Name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [Column("Last_Name")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(15)]
        public string Contact { get; set; }
        [Column("DOB", TypeName = "date")]
        public DateTime Dob { get; set; }
        [Required]
        [StringLength(64)]
        public string Password { get; set; }
        [Required]
        [StringLength(100)]
        public string AddressLine1 { get; set; }
        [Required]
        [StringLength(100)]
        public string AddressLine2 { get; set; }
        [Required]
        [StringLength(30)]
        public string City { get; set; }
        [Required]
        [StringLength(30)]
        public string State { get; set; }
        [Required]
        [StringLength(30)]
        public string PermanentCity { get; set; }
        [Required]
        [StringLength(30)]
        public string PermanentState { get; set; }

        [InverseProperty(nameof(GroupInvitesRequest.CreatedByNavigation))]
        public virtual ICollection<GroupInvitesRequest> GroupInvitesRequestCreatedByNavigations { get; set; }
        [InverseProperty(nameof(GroupInvitesRequest.SentToNavigation))]
        public virtual ICollection<GroupInvitesRequest> GroupInvitesRequestSentToNavigations { get; set; }
        [InverseProperty(nameof(GroupMember.Member))]
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        [InverseProperty(nameof(Group.CreatedByNavigation))]
        public virtual ICollection<Group> Groups { get; set; }
        [InverseProperty(nameof(Post.PostedByNavigation))]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
