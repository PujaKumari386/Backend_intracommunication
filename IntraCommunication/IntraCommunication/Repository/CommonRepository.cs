using AutoMapper;
using IntraCommunication.Models;
using IntraCommunication.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntraCommunication.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private readonly IntracommunicatonContext db;
        private readonly IMapper _mapper;

        public CommonRepository(IntracommunicatonContext db, IMapper _mapper)
        {
            this.db = db;
            this._mapper = _mapper;
        }

        //Api's for Post
        public async Task<List<Post>> GetPosts(int groupId)
        {
            var posts = await db.Posts.Where(p => p.PostedOn == groupId).ToListAsync();
            return posts;
        }

        public async Task<Post> AddPost(PostModel userPost)
        {
            var newpost = new Post()
            {
                PostDescription = userPost.PostDescription,
                PostType = "t",
                PostedAt = System.DateTime.Now,
                PostedOn = userPost.PostedOn,
                PostedBy = userPost.PostedBy,
                StartTime = System.DateTime.Now,
                EndTime = System.DateTime.Now,
                Url = "",
            };

            db.Posts.Add(newpost);
            await db.SaveChangesAsync();

            return newpost;
        }

        public async Task DeletePost(int PostId)
        {
            var record = new Post()
            { PostId = PostId };
            db.Posts.Remove(record);
            await db.SaveChangesAsync();

        }

        //Api's for Comment
        public async Task<List<Comment>> GetAllComments()
        {
            if (db != null)
            {
                var records = await db.Comments.ToListAsync();
                return records;
            }
            return null;

        }

        public async Task<Comment> AddComment(CommentModel usercomment)
        {
            var newcomment = new Comment()
            {
                CommentedBy = usercomment.CommentedBy,
                PostId = usercomment.PostId,
                CommentDescription = usercomment.Comment1,
                CommentedAt = System.DateTime.Now
            };

            db.Comments.Add(newcomment);
            await db.SaveChangesAsync();

            return newcomment;
        }

        public async Task DeleteComment(int CommentId)
        {
            var record = new Comment()
            { CommentId = CommentId };
            db.Comments.Remove(record);
            await db.SaveChangesAsync();

        }

        //Likes
        public async Task<List<Like>> GetAllLikes()
        {
            if (db != null)
            {
                var records = await db.Likes.ToListAsync();
                return records;
            }
            return null;

        }

        public async Task<Like> AddLikes(LikeModel UserLikes)
        {

            var likepost = new Like()
            {
                PostId = UserLikes.PostId,
                UserId = UserLikes.UserId
            };

            db.Likes.Add(likepost);
            await db.SaveChangesAsync();

            return likepost;
        }





        /*Api's for Event
        public async Task<List<Event>> GetAllEvents()
        {
            if (db != null)
            {
                var records = await db.Events.ToListAsync();
                return _mapper.Map<List<Event>>(records);
            }
            return null;

        }

        public async Task<Event> AddEvent(Event userevent)
        {
            var newevent = new Event()
            {
            EventsDescription = userevent.EventsDescription,
            PostId = userevent.PostId,
            };

            db.Events.Add(newevent);
            await db.SaveChangesAsync();

            return newevent;
        }

        public async Task DeleteEvent(int EventId)
        {
            var record = new Event()
            { EventId = EventId };
            db.Events.Remove(record);
            await db.SaveChangesAsync();

        }*/
    }
}

