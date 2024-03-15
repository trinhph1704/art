using AWS.DTO;
using AWS.DTO.ArtworkDTO;
using AWS.Models;
using AWS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AWS.Repositories.Services
{
    public class SComment : IComment
    {
        private readonly ARTWORKPLATFORMContext cxt;

        public SComment(ARTWORKPLATFORMContext cxt)
        {
            this.cxt = cxt;
        }

        public async Task<Comment> CreateNewComment(NewComment newComment)
        {
            try
            {
                var comment = new Comment
                {
                    CommentId = "C" + Guid.NewGuid().ToString().Substring(0, 5),
                    UserId = newComment.UserId,
                    ArtworkId = newComment.ArtWorkId,
                    Timestamp = DateTime.Now, // Set current time
                    Text = newComment.Content
                };

                //Add Genres to the artwork if provided
               

                cxt.Comments.Add(comment);
                await cxt.SaveChangesAsync();

                return comment;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<Comment> GetCommentByUserID(string userID)
        {
            try
            {
                var b = await this.cxt.Comments.Where(x => x.UserId == userID).FirstOrDefaultAsync();

                return b;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Comment>> GetAllCommentByUserID(string userID)
        {
            try
            {
                var b = await this.cxt.Comments.Where(x => x.UserId == userID).ToListAsync();

                return b;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Comment>> GetCommentByArtworkID(string ArtworkID)
        {
            try
            {
                var b = await this.cxt.Comments.Where(x => x.ArtworkId == ArtworkID).ToListAsync();

                return b;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Comment> GetCommentByID(string id)

        {
            try
            {
                var a = await this.cxt.Comments.Where(x => x.CommentId.Equals(id)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);
            }
        }

      
        public async Task<List<Comment>> GetComments()
        {
            try
            {
                var y = await this.cxt.Comments.ToListAsync();
                return y;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
