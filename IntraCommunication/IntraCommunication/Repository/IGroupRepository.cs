using IntraCommunication.Models;
using IntraCommunication.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntraCommunication.Repository
{
    public interface IGroupRepository
    {
        Task<List<Group>> GetAllGroups();
        Task DeleteGroup(int GroupId);
        Task<Boolean> SendInvite_Request(GroupRequestModel invite);
        Task<Boolean> AddGroupMember(GroupMember member);
        Task<Boolean> AcceptInvite(int inviteID);
        Task<List<GroupMember>> GetAllGroupMembers(int groupID);
        Task<List<Group>> GetgroupbyName(string Groupname);
        Task<Group> CreateGroup(GroupCreateModel group, int AdminId);
        Task<Group> UpdateGroup(JsonPatchDocument groupPatch, int id);

    }
}
