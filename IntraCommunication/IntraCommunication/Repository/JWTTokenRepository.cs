using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using IntraCommunication.ViewModels;
using IntraCommunication.Models;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace IntraCommunication.Repository
{
    public class JWTTokenRepository : IJWTTokenRepository
    {
        private readonly IntracommunicatonContext db;
        public JWTTokenRepository(IntracommunicatonContext db, IConfiguration configuration)
        {
            this.db = db;
            iconfiguration = configuration;
        }

        public IConfiguration iconfiguration { get; }
        public Tokens Authenticate(SignInModel user)
        {
            var isUser = db.UserProfiles.Where(u => u.Email == user.Email).FirstOrDefault();

            if (isUser != null && BCrypt.Net.BCrypt.Verify(user.Password, isUser.Password))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Email, user.Email)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new Tokens { Token = tokenHandler.WriteToken(token) };
            }

            return null;

        }
    }
}
 
