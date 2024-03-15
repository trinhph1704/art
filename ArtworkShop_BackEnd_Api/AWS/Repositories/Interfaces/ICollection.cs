using AWS.DTO;
using AWS.Models;

namespace AWS.Repositories.Interfaces
{
    public interface ICollection
    {
        Task<LikeCollection> Love(CollectionDTO ArtworkID);
        Task<bool> UnLove(DeleteCollectionDTO ArtworkID);
        Task<LikeCollection> GetCollectionByUserId(string userId);
        Task<List<LikeCollection>> GetAllCollectionByUserId(string userId);
        Task<List<LikeCollection>> GetAllCollection();

    }
}
