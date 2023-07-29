using IntraCommunication.Models;
using IntraCommunication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntraCommunication.Repository
{
    public interface ICommonRepository
    {
        /*Task<List<Event>> GetAllEvents();
        Task<Event> AddEvent(Event userevent);
        Task DeleteEvent(int EventId);*/
        Task<List<Post>> GetPosts(int groupId);
        Task<Post> AddPost(PostModel userPost);
        Task DeletePost(int PostId);
        Task<List<Comment>> GetAllComments();
        Task<Comment> AddComment(CommentModel usercomment);
        Task DeleteComment(int CommentId);
        Task<List<Like>> GetAllLikes();
        Task<Like> AddLikes(LikeModel UserLikes);

    }
}
