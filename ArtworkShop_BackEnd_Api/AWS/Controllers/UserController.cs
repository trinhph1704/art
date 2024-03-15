
using AWS.DTO;
using AWS.Repositories.Interfaces;
using AWS.Repositories.Services;
using backend_not_clear.DTO.UserDTO;
using backend_not_clear.DTO.UserDTO.SearchUserID;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser user;

        public UserController(IUser user) {
            this.user = user;
        }

        [HttpGet]
        [Route("get all user")]
        public async Task<IActionResult> GetAll() {
            
                var a = await this.user.GetAllUsers();
                if (a == null)
                {
                    return NotFound();
                }
                return Ok(a);
        }


        /// <summary>
        /// register for user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("registration")]
        [HttpPost]
        public async Task<IActionResult> Registration(RegisterDTO user)
        {
            try
            {
                var a = await this.user.Registration(user);
                return Ok(a);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Login with username, password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            try
            {
                var a = await this.user.Login(user);
                return Ok(a);
            }
            catch (Exception ex)
            {
               
                return BadRequest(ex.Message);
            }
        }

        /// <returns></returns>
        [AllowAnonymous]
        [Route("update")]
        [HttpPost]
        public async Task<IActionResult> Update(string id, UpdateDTO user)
        {
            try
            {
                var a = await this.user.Update(id,user);
                return Ok(a);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("update-money")]
        [HttpPost]
        public async Task<IActionResult> UpdateMoney(string id, UpdateMoneyuserDTO user)
        {
            try
            {
                var a = await this.user.UpdateMoney(id, user);
                return Ok(a);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("update-status-post")]
        [HttpPost]
        public async Task<IActionResult> UpdateStatusPost(string id)
        {
            try
            {
                var a = await this.user.UpdateStatusPost(id);
                return Ok(a);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("search-by-name")]
        [HttpGet]
        public async Task<IActionResult> Search(string user)
        {
            try
            {
                var a = await this.user.SearchByName(user);
                return Ok(a);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(RemoveDTO user)
        {
            try
            {
                var a = await this.user.Dellete(user);
                return Ok(a);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("get-by-id")]
        public async Task<IActionResult> GetUserById(SearchUserID id)
        {
            try
            {
                var user = await this.user.getUserByID(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
