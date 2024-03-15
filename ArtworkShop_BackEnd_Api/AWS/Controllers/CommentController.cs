using AWS.DTO;
using AWS.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace AWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IComment comment;

        public CommentController(IComment comment)
        {
            this.comment = comment;
        }

        [HttpGet]
        [Route("get-all")]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var a = await this.comment.GetComments();
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the GetAll method: {ex}");

                throw;
            }

        }

        [HttpGet]
        [Route("get-all-comment-By-User-Id")]

        public async Task<IActionResult> GetAllCommentByUserID(string id)
        {
            try
            {
                var a = await this.comment.GetAllCommentByUserID(id);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the GetAllCommentByUserID method: {ex}");

                throw;
            }

        }

        [HttpPost]
        [Route("create-new")]

        public async Task<IActionResult> CreateComment(NewComment id)
        {
            try
            {
                var a = await this.comment.CreateNewComment(id);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the CreateNewComment method: {ex}");

                throw;
            }

        }


    }
}
