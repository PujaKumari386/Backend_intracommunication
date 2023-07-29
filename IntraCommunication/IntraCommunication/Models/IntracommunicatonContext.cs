using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace IntraCommunication.Models
{
    public partial class IntracommunicatonContext : DbContext
    {
        public IntracommunicatonContext()
        {
        }

        public IntracommunicatonContext(DbContextOptions<IntracommunicatonContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupInvitesRequest> GroupInvitesRequests { get; set; }
        public virtual DbSet<GroupMember> GroupMembers { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:IntraCommDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Comments_PostID");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.GroupName).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Groups_Created_By");
            });

            modelBuilder.Entity<GroupInvitesRequest>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.GroupInvitesRequestCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Group_Invites_Requests_Created_by");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupInvitesRequests)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Group_Invites_Requests_GroupID");

                entity.HasOne(d => d.SentToNavigation)
                    .WithMany(p => p.GroupInvitesRequestSentToNavigations)
                    .HasForeignKey(d => d.SentTo)
                    .HasConstraintName("FK_Group_Invites_Requests_Sent_to");
            });

            modelBuilder.Entity<GroupMember>(entity =>
            {
                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupMembers)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Group_Members_GroupID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.GroupMembers)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Group_Members_MemberID");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.PostType)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.PostedByNavigation)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PostedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Posts_User_Profile");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.Property(e => e.AddressLine1).IsUnicode(false);

                entity.Property(e => e.AddressLine2).IsUnicode(false);

                entity.Property(e => e.City).IsUnicode(false);

                entity.Property(e => e.Contact).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.PermanentCity).IsUnicode(false);

                entity.Property(e => e.PermanentState).IsUnicode(false);

                entity.Property(e => e.State).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
