using IntraCommunication.Models;
using IntraCommunication.Repository;
using IntraCommunication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IntraCommunication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJWTTokenRepository jwttoken;
        private readonly IUserRepository _userRepository;
        private readonly IntracommunicatonContext _dbContext;
        public UserController(IUserRepository UserRepository, IJWTTokenRepository jWTToken  , IntracommunicatonContext DbContext)
        {
            _userRepository = UserRepository;
            jwttoken = jWTToken;
            _dbContext = DbContext;
        }


        //Api Call for User Table
        [AllowAnonymous]
        [HttpGet("")]

        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userRepository.GetAllUsers();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpGet("{name}")]

        public async Task<IActionResult> GetUserByName([FromRoute] string name)
        {
            var user = await _userRepository.GetUserbyName(name);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [AllowAnonymous]
        [HttpGet("getById/{Userid}")]

        public async Task<IActionResult> GetUserById([FromRoute] int Userid)
        {
            var user = await _userRepository.GetUserbyId(Userid);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [AllowAnonymous]
        [HttpPost("add")]

        public async Task<IActionResult> AddNewUser([FromBody] UserProfileModel user)
        {
            /*if (user == null)
            {
                return BadRequest();
            }*/
            var newuser = await _userRepository.AddUser(user);
            return Ok(new { user, message = "welcome😊!!" });
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        
        public IActionResult Authenticate(SignInModel usersdata)
        {    
            var user = _dbContext.UserProfiles.Where(x=> x.Email == usersdata.Email).FirstOrDefault();
            var token = jwttoken.Authenticate(usersdata);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token, id = user.UserId });
         
        }

        [HttpPatch("patch/{id}")]

        public async Task<IActionResult> UpdateUserPatch([FromBody] JsonPatchDocument user, [FromRoute] int id)
        {
            await _userRepository.UpdateUserPatch(id, user);
            return Ok();
        }

        [HttpDelete("delete/{id}")]

        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            await _userRepository.DeleteUser(id);
            return Ok();
        }
    }
}
