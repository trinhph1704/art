using AWS.DTO;
using AWS.DTO.ArtworkDTO;
using AWS.Models;
using backend_not_clear.DTO.UserDTO;

namespace AWS.Repositories.Interfaces
{
    public interface IArtwork
    {
        Task<List<Artwork>> GetAllArtworks();
        Task<Artwork> GetArtworkId(string id);
        Task<List<Artwork>> SearchByName(string name);
        Task<Artwork> CreateArtwork(string userId, CreateArtwork createArtwork);
        Task<Artwork> UpdateArtWork(string artworkId, UpdateArtWork updatedArtwork);
        Task<Artwork> UpdateArtWorkImageUrl2(string artworkId, UpdateArtWork2 updatedArtwork);
        Task<Artwork> UpdateArtWorkProccessing(string artworkId, UpdateArtWorkProccessing updatedArtwork);
        Task<List<Artwork>> GetByGenre(string genreId);
        Task<List<Artwork>> GetArtworkStatusTrue();
        Task<bool> Dellete(string id);

    }
}
