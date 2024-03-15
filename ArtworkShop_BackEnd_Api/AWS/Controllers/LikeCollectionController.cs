using AWS.DTO;
using AWS.DTO.ArtworkDTO;
using AWS.Models;
using AWS.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeCollectionController : ControllerBase
    {
        private readonly ICollection collection;

        public LikeCollectionController(ICollection collection)
        {
            this.collection = collection;
        }

        [HttpPost]
        [Route("Love")]

        public async Task<IActionResult> AddToCollection(CollectionDTO create)
        {
            try
            {
                var a = await this.collection.Love(create);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the AddToCollection method: {ex}");

                throw;
            }

        }

        [HttpGet]
        [Route("get-collection-by-userid")]

        public async Task<IActionResult> GetCollectionByID(string id)
        {
            try
            {
                var a = await this.collection.GetCollectionByUserId(id);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the GetCollectionByID method: {ex}");

                throw;
            }

        }

        [HttpGet]
        [Route("get-all-collection-by-userid")]

        public async Task<IActionResult> GetAllCollectionByID(string id)
        {
            try
            {
                var a = await this.collection.GetAllCollectionByUserId(id);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the GetAllCollectionByID method: {ex}");

                throw;
            }

        }

        [HttpGet]
        [Route("get-all-collection")]

        public async Task<IActionResult> GetAllCollection()
        {
            try
            {
                var a = await this.collection.GetAllCollection();
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the GetAllCollection method: {ex}");

                throw;
            }

        }

        [HttpDelete]
        [Route("Un-Love")]

        public async Task<IActionResult> RemoveToCollection(DeleteCollectionDTO id)
        {
            try
            {
                var a = await this.collection.UnLove(id);
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in the RemoveToCollection method: {ex}");

                throw;
            }

        }
    }
}
