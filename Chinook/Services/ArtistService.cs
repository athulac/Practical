using Chinook.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Services
{
    
    public class ArtistService : IArtistService
    {
        public IDbContextFactory<ChinookContext> DbFactory;

        public ArtistService(IDbContextFactory<ChinookContext> DbFactory)
        {
            this.DbFactory = DbFactory;
        }

        public async Task<List<Artist>> GetArtists()
        {
            var dbContext = await DbFactory.CreateDbContextAsync();
            var users = dbContext.Users.Include(a => a.UserPlaylists).ToList();

            return dbContext.Artists.ToList();
        }
    }
}
