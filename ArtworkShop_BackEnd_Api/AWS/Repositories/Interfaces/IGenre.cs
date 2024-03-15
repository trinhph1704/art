using AWS.Models;
using ArtWorkShop.Repositories.Services;
using backend_not_clear.DTO.UserDTO;
using AWS.DTO.ArtworkDTO;

namespace AWS.Repositories.Interfaces
{
    public interface IGenre
    {
        Task<List<Genre>> GetAllGenres();
        Task<Genre> GetGenreId(string id);
       
    }
}
