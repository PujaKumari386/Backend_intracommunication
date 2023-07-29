using AutoMapper;
using IntraCommunication.Models;
using IntraCommunication.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntraCommunication.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IntracommunicatonContext db;
        private readonly IMapper _mapper;

        public UserRepository(IntracommunicatonContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        public async Task<List<UserProfile>> GetAllUsers()
        {
            if (db != null)
            {
                var records = await db.UserProfiles.ToListAsync();
                return _mapper.Map<List<UserProfile>>(records);
            }
            return null;

        }

        public async Task<List<UserProfile>> GetUserbyName(string Name)
        {
            // return db.StudentTables.FirstOrDefault(s => s.FirstName == firstName);
            var record = await db.UserProfiles.Where(x => x.FirstName == Name || x.LastName == Name).ToListAsync();
            return record;
        }

        public async Task<UserProfile> GetUserbyId(int UserId)
        {
            var user = await db.UserProfiles.FindAsync(UserId);
            return user;
        }

        public async Task<UserProfile> AddUser(UserProfileModel userProfile)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userProfile.Password);
            var userdata = new UserProfile()
            {
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                Email = userProfile.Email,
                Contact = userProfile.Contact,
                Dob = userProfile.Dob,
                Password = hashedPassword,
                AddressLine1 = userProfile.AddressLine1,
                AddressLine2 = userProfile.AddressLine2,
                City = userProfile.City,
                State = userProfile.State,
                PermanentCity = userProfile.PermanentCity,
                PermanentState = userProfile.PermanentState
            };

            db.UserProfiles.Add(userdata);
            await db.SaveChangesAsync();

            return userdata;
        }

        public async Task<UserProfile> Login(string email, string password)
        {
            var user = await db.UserProfiles.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;
            }
            return null;
        }

        public async Task UpdateUserPatch(int UserId, JsonPatchDocument user)
        {
            var record = await db.UserProfiles.FindAsync(UserId);
            if (record != null)
            {
                user.ApplyTo(record);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteUser(int UserId)
        {
            var record = new UserProfile()
            { UserId = UserId };
            db.UserProfiles.Remove(record);
            await db.SaveChangesAsync();

        }
    }
}
