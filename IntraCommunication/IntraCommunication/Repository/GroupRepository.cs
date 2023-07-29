using AutoMapper;
using IntraCommunication.Models;
using IntraCommunication.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static AutoMapper.Internal.ExpressionFactory;
using Group = IntraCommunication.Models.Group;

namespace IntraCommunication.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IntracommunicatonContext db;
        private readonly IMapper _mapper;

        public GroupRepository(IntracommunicatonContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        public async Task<Boolean> SendInvite_Request(GroupRequestModel invite)
        {
            if (invite == null) return false;
            var new_invite = new GroupInvitesRequest()
            {
                SentTo = invite.SentTo,
                GroupId = invite.GroupId,
                IsAccepted = invite.IsAccepted,
                IsApproved = invite.IsApproved,
                CreatedBy = invite.CreatedBy,
                CreatedAt = invite.CreatedAt,
                UpdatedAt = invite.UpdatedAt,
            };
            await db.GroupInvitesRequests.AddAsync(new_invite);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<Boolean> AddGroupMember(GroupMember member)
        {
            var new_member = new GroupMember()
            {
                GroupId = member.GroupId,
                MemberId = member.MemberId,
            };
            // Check if a user has already sent request to join the group
            var isRequested = await db.GroupInvitesRequests.Where(gID => (gID.GroupId == member.GroupId) && (gID.CreatedBy == member.MemberId)).FirstOrDefaultAsync();

            // the user has not sent request then invite the user
            if (isRequested == null)
            {
                var group = await db.Groups.Where(g => g.GroupId == member.GroupId).FirstOrDefaultAsync();

                var invite = new GroupRequestModel()
                {
                    SentTo = member.MemberId,
                    GroupId = member.GroupId,
                    IsAccepted = false,
                    IsApproved = true,
                    CreatedBy = group.CreatedBy,
                    CreatedAt = System.DateTime.Now,
                    UpdatedAt = System.DateTime.Now,
                };
                return await SendInvite_Request(invite);

            }
            else
            {
                // if the user has sent the request to join then approve his request.
                isRequested.IsApproved = true;
                await db.GroupMembers.AddAsync(member);
                return true;
            }
        }

        public async Task<Boolean> AcceptInvite(int inviteID)
        {
            var invite = await db.GroupInvitesRequests.FindAsync(inviteID);

            if (invite != null)
            {
                invite.IsAccepted = true;
                var member = new GroupMember()
                {
                    MemberId = invite.SentTo,
                    GroupId = invite.GroupId,
                };
                await db.GroupMembers.AddAsync(member);
                db.GroupInvitesRequests.Remove(invite);
                await db.SaveChangesAsync();
                return true;
            }
            return false;

        }



        public async Task<List<GroupMember>> GetAllGroupMembers(int groupID)
        {
            if (db != null)
            {
                var members = await db.GroupMembers.Where(member => member.GroupId == groupID).ToListAsync();
                return _mapper.Map<List<GroupMember>>(members);
            }
            return null;
        }

        public async Task<List<Group>> GetAllGroups()
        { 

            if (db != null)
            {
                var groups = await db.Groups.ToListAsync();
                return _mapper.Map<List<Group>>(groups);
            }
            return null;
        }

        /*public async Task<List<Group>> GetGroupsByName(string groupName)
        {
            if (db != null)
            {
                var groups = await db.Groups.Where(group => group.GroupName == groupName).ToListAsync();
                return _mapper.Map<List<Group>>(groups).ToList();
            }
            return null;
        }*/

        public async Task<List<Group>> GetgroupbyName(string Groupname)
        {
            // return db.StudentTables.FirstOrDefault(s => s.FirstName == firstName);
            var record = await db.Groups.Where(x => x.GroupName == Groupname).ToListAsync();
            return record;
        }

        public async Task<Group> CreateGroup(GroupCreateModel group, int AdminId)
        {

            if (group != null)
            {
                var new_group = new Group()
                {
                    GroupName = group.GroupName,
                    GroupDescription = group.Description,
                    GroupType = group.GroupType,
                    CreatedAt = System.DateTime.Now,
                    CreatedBy = AdminId,
                };
                await db.Groups.AddAsync(new_group);
                await db.SaveChangesAsync();
                return new_group;
            }
            return null;
        }

        public async Task<Group> UpdateGroup(JsonPatchDocument groupPatch, int id)
        {
            var groupData = await db.Groups.FindAsync(id);
            if (groupData != null && groupPatch != null)
            {
                groupPatch.ApplyTo(groupData);
                await db.SaveChangesAsync();
                return groupData;
            }
            return null;
        }

        public async Task DeleteGroup(int id)
        {

            var group = await db.Groups.FindAsync(id);
            if (group != null)
            {
                db.Groups.Remove(group);
                await db.SaveChangesAsync();
            }
        }


        /*public async Task DeleteGroup(int GroupId)
        {
            var record = new Group()
            { GroupId = GroupId };
            db.Groups.Remove(record);
            await db.SaveChangesAsync();

        }*/

        /* public async Task<List<GroupMember>> GetAllGroupmembers()
         {
             if (db != null)
             {
                 var records = await db.GroupMembers.ToListAsync();
                 return _mapper.Map<List<GroupMember>>(records);
             }
             return null;

         }

         public async Task<List<Group>> GetAllGroups()
         {
             if (db != null)
             {
                 var records = await db.Groups.ToListAsync();
                 return _mapper.Map<List<Group>>(records);
             }
             return null;

         }
        */

    }
}
