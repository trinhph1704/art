using AWS.DTO.ArtworkDTO;
using AWS.Models;
using AWS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using AWS.Repositories.Interfaces;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;
using ArtWorkShop.Repositories.Services;
using AWS.DTO;

namespace AWS.Repositories.Services
{
    public class SArtwork : IArtwork
    {
        private readonly ARTWORKPLATFORMContext cxt;

        public SArtwork(ARTWORKPLATFORMContext Cxt)
        {
            cxt = Cxt;
        }

        public async Task<Artwork> CreateArtwork(string userId, CreateArtwork createArtwork)
        {
            try
            {
                 
                var user = await cxt.Usertbs.FindAsync(userId);

                if (user.StatusPost == true || user.PremiumId != null)
                {
                    var artwork = new Artwork
                    {
                        UserId = userId,
                        ArtworkId = "A" + Guid.NewGuid().ToString().Substring(0, 5),
                        Title = createArtwork.Title,
                        Description = createArtwork.Description,
                        Price = createArtwork.Price,
                        ImageUrl = createArtwork.ImageUrl,
                        ImageUrl2 = createArtwork.ImageUrl2,
                        Reason = createArtwork.Reason,
                        Time = DateTime.Now, // Set current time
                        StatusProcessing = false
                    };

                    //Add Genres to the artwork if provided
                    if (createArtwork.Genres != null && createArtwork.Genres.Any())
                    {
                        foreach (var genreDto in createArtwork.Genres)
                        {
                            var genre = await cxt.Genres.FindAsync(genreDto.GenreID);
                            if (genre != null)
                            {
                                artwork.Genre = genre;
                            }
                            else
                            {
                                // Handle error if Genre doesn't exist
                                throw new Exception($"Genre with ID {genreDto.GenreID} not found.");
                            }
                        }
                    }
                    cxt.Artworks.Add(artwork);
                    await cxt.SaveChangesAsync();

                    return artwork;
                }
                return null; 
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<Artwork> UpdateArtWork(string artworkId, UpdateArtWork updatedArtwork)
        {
            try
            {
                // Retrieve the artwork from the database
                var artwork = await cxt.Artworks.FindAsync(artworkId);

                if (artwork == null)
                {
                    throw new Exception($"Artwork with ID {artworkId} not found.");
                }

                // Update the artwork properties
                artwork.Title = updatedArtwork.Title ?? artwork.Title;
                artwork.Description = updatedArtwork.Description ?? artwork.Description;
                artwork.Price = updatedArtwork.Price.HasValue ? updatedArtwork.Price.Value : artwork.Price;
                artwork.ImageUrl = updatedArtwork.ImageUrl ?? artwork.ImageUrl;
                artwork.ImageUrl2 = updatedArtwork.ImageUrl2 ?? artwork.ImageUrl2;
                artwork.Reason = updatedArtwork.Reason ?? artwork.Reason;
                artwork.GenreId = updatedArtwork.GenreId ?? artwork.GenreId;
                artwork.Time = DateTime.Now;
              

                // Update the artwork in the database
                cxt.Artworks.Update(artwork);
                await cxt.SaveChangesAsync();

                return artwork;
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while updating artwork.", e);
            }
        }


        public async Task<Artwork> UpdateArtWorkImageUrl2(string artworkId, UpdateArtWork2 up)
        {
            try
            {
                // Retrieve the artwork from the database
                var artwork = await cxt.Artworks.FindAsync(artworkId);

                if (artwork == null)
                {
                    throw new Exception($"Artwork with ID {artworkId} not found.");
                }

                // Update the artwork properties
                artwork.ImageUrl = up.ImageUrl;


                // Update the artwork in the database
                cxt.Artworks.Update(artwork);
                await cxt.SaveChangesAsync();

                return artwork;
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while updating artwork.", e);
            }
        }

        public async Task<Artwork> UpdateArtWorkProccessing(string artworkId, UpdateArtWorkProccessing updatedArtwork)
        {
            try
            {
                // Retrieve the artwork from the database
                var artwork = await cxt.Artworks.FindAsync(artworkId);

                if (artwork == null)
                {
                    throw new Exception($"Artwork with ID {artworkId} not found.");
                }

                // Update the artwork properties
             
                artwork.Reason = updatedArtwork.Reason ?? artwork.Reason;
              
                artwork.TimeProcessing = DateTime.Now;
                artwork.StatusProcessing = true;
                // Update the genre if provided
                //if (updatedArtwork.GenreId == null)
                //{
                //    artwork.GenreId = null;
                //}
                //if (updatedArtwork.GenreId != null)
                //{
                //    var genre = await cxt.Genres.FindAsync(updatedArtwork.GenreId);
                //    if (genre != null)
                //    {
                //        artwork.Genre = genre;
                //    }
                //    else
                //    {
                //        throw new Exception($"Genre with ID {updatedArtwork.GenreId} not found.");
                //    }
                //}


                // Update the artwork in the database
                cxt.Artworks.Update(artwork);
                await cxt.SaveChangesAsync();

                return artwork;
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while updating artwork.", e);
            }
        }

        public async Task<List<Artwork>> GetAllArtworks()
        {
            try
            {
                var y = await this.cxt.Artworks.ToListAsync();
                return y;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public async Task<Artwork> GetArtworkId(string id)
        {
            try
            {
                var a = await this.cxt.Artworks.Where(x => x.ArtworkId.Equals(id)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<List<Artwork>> SearchByName(string name)
        {
            try
            {
                var list = await this.cxt.Artworks
                            .Where(x => x.Title.Contains(name))
                            .ToListAsync();
                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Artwork>> GetByGenre(string genreId)
        {

            try
            {
                var artworks = await this.cxt.Artworks
               .Where(a => a.GenreId == genreId)
               .ToListAsync();

                return artworks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Artwork>> GetArtworkStatusTrue()
        {
            try
            {
                var artworks = await this.cxt.Artworks
               .Where(a => a.StatusProcessing == true)
               .ToListAsync();

                return artworks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Artwork>> GetArtworkByUserId(string id)
        {
            try
            {
                var artworks = await this.cxt.Artworks
               .Where(a => a.UserId == id)
               .ToListAsync();

                return artworks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Dellete(string id)
        {
            try
            {
                if (id != null)
                {
                    var obj = await this.cxt.Artworks.Where(x => x.ArtworkId.Equals(id)).FirstOrDefaultAsync();
                    this.cxt.Artworks.Remove(obj);
                    await this.cxt.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw new Exception($"{ex.Message}");
            }
        }

       
    }
}

        
    
          

