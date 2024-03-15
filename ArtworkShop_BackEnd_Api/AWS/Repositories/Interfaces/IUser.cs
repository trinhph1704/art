using ArtWorkShop.Repositories.Services;
using AWS.DTO;
using AWS.Models;
using backend_not_clear.DTO.UserDTO;
using backend_not_clear.DTO.UserDTO.SearchUserID;

namespace AWS.Repositories.Interfaces
{
    public interface IUser
    {
        Task<List<Usertb>> GetAllUsers();
        Task<Usertb> Registration(RegisterDTO request);
        Task<string> Login(LoginDTO request);
        Task<List<Usertb>> SearchByName(string name);
        Task<bool> Dellete(RemoveDTO id);
        Task<Usertb> getUserByID(SearchUserID id);
        Task<Usertb> UpdateStatusPost(string userID);

        Task<Usertb> Update(string id, UpdateDTO user);
        //Task<Usertb> UpdatePremium(string OrderPreId, UpdatePreUser update);
        Task<Usertb> UpdateMoney(string id, UpdateMoneyuserDTO user);

    }
}
