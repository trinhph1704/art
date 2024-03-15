using AWS.DTO;
using AWS.Models;
using AWS.Repositories.Interfaces;
using backend_not_clear.DTO.UserDTO;
using backend_not_clear.DTO.UserDTO.SearchUserID;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArtWorkShop.Repositories.Services
{
    public class SUser : IUser
    {
        private readonly IConfiguration _configuration;

        private readonly ARTWORKPLATFORMContext context;

        public SUser(ARTWORKPLATFORMContext Context, IConfiguration configuration)
        {
            context = Context;
            _configuration = configuration;
        }

       

        public async Task<List<Usertb>> GetAllUsers()
        {
            try
            {
                var y = await this.context.Usertbs.ToListAsync();
                return y;
            }
            catch (Exception ex)
            {

                throw new Exception($"{ex.Message}");
            }
            
        }

        public async Task<Usertb> getUserByID(SearchUserID id)
        {
            try
            {
                var search = await this.context.Usertbs.Where(x => x.UserId.Equals(id.userID))
                                                                .FirstOrDefaultAsync();
                return search;
            }
            catch (Exception ex)
            {

                throw new Exception($"{ex.Message}");
            }
          
        }

        public async Task<List<Usertb>> SearchByName(string name)
        {
            try
            {
                var list = await this.context.Usertbs.Where(x => x.Username.Contains(name)).ToListAsync();
                if (list != null) return list;
                throw new Exception("Not Found");
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        } 
 
        private string CreateToken(Usertb user)
        {

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Role, user.RoleId),
                new Claim("userid", user.UserId),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

        }
        public async Task<string> Login(LoginDTO request)
        {
            try
            {
                var user = await this.context.Usertbs.Where(x => x.Username.Equals(request.UserName))
                                                   .Include(y => y.Roles)
                                                   .FirstOrDefaultAsync();
                if (user == null)
                    throw new Exception("USER IS NOT FOUND");
                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                    throw new Exception("INVALID PASSWORD");
                //if (!user.Status)
                //    throw new Exception("ACCOUNT WAS BANNED OR DELETED");
                var token = CreateToken(user);
                return token;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async  Task<Usertb> Registration(RegisterDTO request)
        {
            try
            {
                var r = new Usertb();
                if (request != null)
                {
                    foreach (var x in this.context.Usertbs)
                    {
                        if (request.Username.Equals(x.Username))
                        {
                            throw new Exception("UserName has been existted!");
                        }
                    }
                    r.UserId = "US" + Guid.NewGuid().ToString().Substring(0, 5);
                    r.Username = request.Username;
                    r.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                    r.RoleId = "1";
                    r.StatusPost = true;
                    await this.context.Usertbs.AddAsync(r);
                    await this.context.SaveChangesAsync();
                    return r;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<bool> Dellete(RemoveDTO id)
        {
            try
            {
                if (id != null)
                {
                    var obj = await this.context.Usertbs.Where(x => x.UserId.Equals(id.UserID)).FirstOrDefaultAsync();
                    this.context.Usertbs.Remove(obj);
                    await this.context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Usertb> Update(string id,UpdateDTO user)
        {
            try
            {
                var existingUser = await context.Usertbs.FirstOrDefaultAsync(x => x.UserId == id);
                if (existingUser == null)
                    throw new Exception("USER IS NOT FOUND");


                existingUser.Fullname = user.fullName ?? existingUser.Fullname;
                existingUser.Sex = user.gender ?? existingUser.Sex;
                existingUser.PhoneNumber = user.Phone ?? existingUser.PhoneNumber;
                existingUser.Address = user.Address ?? existingUser.Address;
                existingUser.ImageUrl = user.imgURL ?? existingUser.ImageUrl;
                existingUser.DateOfBirth = user.dateOfBird ?? existingUser.DateOfBirth;
                existingUser.Bank = user.Bannk ?? existingUser.Bank;
                existingUser.BankAccount = user.BankAccount ?? existingUser.BankAccount;

                context.Usertbs.Update(existingUser);
                await context.SaveChangesAsync();

                return existingUser;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Usertb> UpdateStatusPost(string userID)
        {
            try
            {
                var x = await this.context.Usertbs.Where(a => a.UserId == userID).FirstOrDefaultAsync();
                x.StatusPost = false;
                this.context.Usertbs.Update(x);
                await this.context.SaveChangesAsync();
                return x;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Usertb> UpdateMoney(string id, UpdateMoneyuserDTO user)
        {
            try
            {
                var x = await this.context.Usertbs.Where(a => a.UserId == id).FirstOrDefaultAsync();
                x.Money = user.Money;
                this.context.Usertbs.Update(x);
                await this.context.SaveChangesAsync();
                return x;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public async Task<Usertb> UpdatePremium(string OrderPreId, UpdatePreUser update)
        //{
        //    try
        //    {
        //        var order = await this.context.OrderPremia.Where(x => x.OrderPremiumId.Equals(OrderPreId) && x.Status == true).FirstOrDefaultAsync();

        //        var x = await this.context.Usertbs.Where(a => a.UserId == id).FirstOrDefaultAsync();
        //        x.Money = user.Money;
        //        this.context.Usertbs.Update(x);
        //        await this.context.SaveChangesAsync();
        //        return x;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
