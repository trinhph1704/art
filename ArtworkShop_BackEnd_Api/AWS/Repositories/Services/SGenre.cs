using AWS.Models;
using AWS.Repositories.Interfaces;
using backend_not_clear.DTO.UserDTO;
using Microsoft.EntityFrameworkCore;

namespace ArtWorkShop.Repositories.Services
{
    public class SGenre : IGenre
    {
        private readonly ARTWORKPLATFORMContext context;

        public SGenre(ARTWORKPLATFORMContext Context)
        {
            this.context = Context;
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            try
            {
                var y = await this.context.Genres.ToListAsync();
                return y;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Genre> GetGenreId(string id)
        {
            try     
            {
                var a = await this.context.Genres.Where(x => x.GenreId.Equals(id)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
