using IntraCommunication.Models;
using IntraCommunication.Repository;
using IntraCommunication.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace IntraCommunication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        public GroupsController(IGroupRepository GroupRepository)
        {
            _groupRepository = GroupRepository;
        }

        //Api Call for Groups Table

        [HttpPost("inviterequest")]
        public async Task<IActionResult> AddGroupMembers([FromBody] GroupMember member)
        {
            if (member == null) return BadRequest();
            var isAdded = await _groupRepository.AddGroupMember(member);
            if (isAdded == true)
            {
                return Ok("member added");
            }
            else
            {
                return Ok("invite sent");
            }
        }

        [HttpPost("create/{AdminId}")]
        public async Task<IActionResult> CreateNewGroup([FromBody] GroupCreateModel group, [FromRoute] int AdminId)
        {
            var new_group = await _groupRepository.CreateGroup(group, AdminId);
            if (new_group != null)
            {
                return Ok(new_group);
            }
            return BadRequest("some error occured, try again.");
        }

        [HttpPost("accept/invitation")]
        public async Task<IActionResult> AcceptInvite([FromQuery] int inviteId)
        {
            var accepted = await _groupRepository.AcceptInvite(inviteId);
            if (accepted)
            {
                return Ok("user accepted invitation");
            }
            else
            {
                return Ok("user rejected invitation");
            }
        }

        [HttpGet("members/{groupID}")]
        public async Task<IActionResult> GetAllGroupMembers([FromRoute] int groupID)
        {
            var members = await _groupRepository.GetAllGroupMembers(groupID);
            if (members == null)
            {
                return NotFound();
            }
            return Ok(members);
        }

        [HttpGet("Allgroups")]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _groupRepository.GetAllGroups();
            if (groups == null)
            {
                return NotFound();
            }
            return Ok(groups);
        }

        [HttpGet("searchGroupByname")]
        public async Task<IActionResult> GetgroupbyName([FromQuery] string Groupname)
        {
            var groups = await _groupRepository.GetgroupbyName(Groupname);
            if (groups == null)
            {
                return NotFound();
            }
            return Ok(groups);
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateGroup([FromBody] JsonPatchDocument groupPatch, [FromRoute] int id)
        {
            var group = await _groupRepository.UpdateGroup(groupPatch, id);
            if (group == null)
            {
                return BadRequest();
            }
            return Ok(group);
        }


        /*[HttpGet("all")]

        public async Task<IActionResult> GetAllGroup()
        {
            var groups = await __groupRepository.GetAllGroups();
            if (groups == null)
            {
                return NotFound();
            }
            return Ok(groups);
        }

        [HttpPost("add")]

        public async Task<IActionResult> AddNewGroup([FromBody] Group user, [FromRoute] int id)
        {
            var newgroup = await __groupRepository.AddGroup(user, id);
            return Ok(newgroup);
        }

        [HttpPatch("patch/{id}")]

        public async Task<IActionResult> UpdateGroupPatch([FromBody] JsonPatchDocument group, [FromRoute] int id)
        {
            await __groupRepository.UpdateGroupPatch(id, group);
            return Ok();
        }

        [HttpDelete("delete/{id}")]

        public async Task<IActionResult> Deletegroup([FromRoute] int id)
        {
            await __groupRepository.DeleteGroup(id);
            return Ok();
        }

        [HttpGet("members")]

        public async Task<IActionResult> GetAllGroupMember()
        {
            var groups = await __groupRepository.GetAllGroupmembers();
            if (groups == null)
            {
                return NotFound();
            }
            return Ok(groups);
        }

        [HttpPost("add/member")]

        public async Task<IActionResult> Addmember([FromQuery] int MemberId, [FromQuery] int GroupId)
        {
            var newmember = await _groupRepository.AddMembers(MemberId, GroupId);
            return Ok(newmember);
        }

        [HttpGet("{name}")]

         public async Task<IActionResult> GetUserByName([FromRoute] string Name)
         {
             var user = await _userRepository.GetUserbyName(Name);
             if (user == null)
             {
                 return NotFound();
             }
             return Ok(user);
         }*/
    }
}
