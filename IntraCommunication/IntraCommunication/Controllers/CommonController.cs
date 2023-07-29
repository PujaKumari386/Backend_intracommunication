using IntraCommunication.Models;
using IntraCommunication.Repository;
using IntraCommunication.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IntraCommunication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonRepository _commonRepository;
        public CommonController(ICommonRepository CommonRepository)
        {
            _commonRepository = CommonRepository;
        }

        
        //Post Table
        [HttpGet("post")]

        public async Task<IActionResult> GetPosts([FromQuery] int groupId)
        {
            var posts = await _commonRepository.GetPosts(groupId);
            if (posts == null)
            {
                return NotFound();
            }
            return Ok(posts);
        }

        [HttpPost("add/Post")]

         public async Task<IActionResult> AddPost([FromBody] PostModel Post)
         {
             var newpost = await _commonRepository.AddPost(Post);
             return Ok(newpost);
         }

         [HttpDelete("delete/Post/{id}")]

         public async Task<IActionResult> DeletePost([FromRoute] int id)
         {
             await _commonRepository.DeletePost(id);
             return Ok();
         }

        //HTTP Calls for Comment
        [HttpGet("comment")]

        public async Task<IActionResult> GetAllcomment()
        {
            var comments = await _commonRepository.GetAllComments();
            if (comments == null)
            {
                return NotFound();
            }
            return Ok(comments);
        }

        [HttpPost("add/Comment")]

        public async Task<IActionResult> AddComment([FromBody] CommentModel comment)
        {
            var newcomment = await _commonRepository.AddComment(comment);
            return Ok(newcomment);
        }

        [HttpDelete("delete/Comment/{id}")]

        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            await _commonRepository.DeleteComment(id);
            return Ok();
        }

        [HttpGet("likes")]

        public async Task<IActionResult> GetAlllikes()
        {
            var likes = await _commonRepository.GetAllLikes();
            if (likes == null)
            {
                return NotFound();
            }
            return Ok(likes);
        }

        [HttpPost("add/like")]

        public async Task<IActionResult> Addlike([FromBody] LikeModel like)
        {
            var newlike = await _commonRepository.AddLikes(like);
            return Ok(newlike);
        }
    }
}
