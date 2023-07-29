using IntraCommunication.Models;
using IntraCommunication.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntraCommunication.Repository
{
    public interface IUserRepository
    {
        Task<List<UserProfile>> GetAllUsers();
        Task<List<UserProfile>> GetUserbyName(string Name);
        Task<UserProfile> GetUserbyId(int UserId);
        Task<UserProfile> AddUser(UserProfileModel userProfile);
        Task UpdateUserPatch(int UserId, JsonPatchDocument user);
        Task DeleteUser(int UserId);
        Task<UserProfile> Login(string email, string password);
    }
}
