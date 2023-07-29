using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace IntraCommunication.Models
{
    [Index(nameof(GroupName), Name = "UK_Groups_Group_Name", IsUnique = true)]
    public partial class Group
    {
        public Group()
        {
            GroupInvitesRequests = new HashSet<GroupInvitesRequest>();
            GroupMembers = new HashSet<GroupMember>();
        }

        [Key]
        [Column("GroupID")]
        public int GroupId { get; set; }
        [Required]
        [Column("Group_Name")]
        [StringLength(50)]
        public string GroupName { get; set; }
        [Required]
        [Column("Group_Description", TypeName = "text")]
        public string GroupDescription { get; set; }
        [Column("Group_Type")]
        public int GroupType { get; set; }
        [Column("Created_At", TypeName = "date")]
        public DateTime CreatedAt { get; set; }
        [Column("Created_By")]
        public int CreatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(UserProfile.Groups))]
        public virtual UserProfile CreatedByNavigation { get; set; }
        [InverseProperty(nameof(GroupInvitesRequest.Group))]
        public virtual ICollection<GroupInvitesRequest> GroupInvitesRequests { get; set; }
        [InverseProperty(nameof(GroupMember.Group))]
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
    }
}
