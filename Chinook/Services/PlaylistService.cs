using Chinook.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Chinook.Services
{
    /// <summary>
    /// this class use for play list related operations
    /// </summary>
    public class PlaylistService : IPlaylistService
    {

        public IDbContextFactory<ChinookContext> DbFactory;                     

        public PlaylistService(IDbContextFactory<ChinookContext> dbFactory)
        {
            DbFactory = dbFactory;
        }
        /// <summary>
        /// This method use for add track to the playlist by moidifying it.
        /// </summary>
        /// <param name="playlistId">Play list id</param>
        /// <param name="trackId">Track d</param>
        /// <returns></returns>
        public async Task ModifyUserPlaylist(long playlistId, long trackId)
        {
            try
            {              
                var DbContext = await DbFactory.CreateDbContextAsync();
                var resPlaylist = await GetPlayList(playlistId);
                var resTrack = await GetTrack(trackId);
                if (!resPlaylist.Tracks.Any(x => x.TrackId == resTrack.TrackId))
                {
                    resPlaylist.Tracks.Clear();
                    resPlaylist.Tracks.Add(resTrack);

                    DbContext.Update(resPlaylist);
                    DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get single play list by using id
        /// </summary>
        /// <param name="playlistId">playlist id</param>
        /// <returns>Playlist object</returns>
        private async Task<Chinook.Models.Playlist> GetPlayList(long playlistId)
        {
            var DbContext = await DbFactory.CreateDbContextAsync();
            var res = DbContext.Playlists.Include(x => x.Tracks).FirstOrDefault(x => x.PlaylistId == playlistId);

            return res;
        }

        private async Task<Track> GetTrack(long id)
        {
            var res = new List<Track> { };
            var DbContext = await DbFactory.CreateDbContextAsync();
            res = DbContext.Tracks.Where(x => x.TrackId == id)?.ToList();

            return res.FirstOrDefault();
        }

        public async Task CreateUserPlaylist(ClientModels.Playlist playlist, ClientModels.PlaylistTrack track, string currUsrId)
        {
            try
            {
                var DbContext = await DbFactory.CreateDbContextAsync();

                var playList = new Chinook.Models.Playlist
                {
                    PlaylistId = GenPlayListId(),
                    Name = playlist.Name,
                };

                var usrPlayList = new Chinook.Models.UserPlaylist
                {
                    UserId = currUsrId,
                    PlaylistId = playList.PlaylistId,
                };

                var resTrack = await GetTrack(track.TrackId);
                resTrack.Playlists.Add(playList);

                DbContext.Entry(playList).State = EntityState.Added;
                await DbContext.SaveChangesAsync();
                DbContext.Entry(usrPlayList).State = EntityState.Added;
                await DbContext.SaveChangesAsync();
                DbContext.Entry(resTrack).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private long GenPlayListId()
        {
            var DbContext = DbFactory.CreateDbContext();
            var id = DbContext.Playlists.Max(x => x.PlaylistId) + 1;
            return id;
        }

        public async Task AddToFaviroute(bool isFav, long trackId, string currUsrId)
        {
            try
            {
                var DbContext = await DbFactory.CreateDbContextAsync();
                var track = await GetTack(trackId);

                if (isFav)
                {
                    await CreateDefaultPlaylist(currUsrId);
                    var dp = await GetDefaultPlaylist();
                    dp.Tracks.Clear();

                    var isExists = await IsExistsPlaylistTack(trackId, dp.PlaylistId);
                    if (!isExists)
                    {
                        dp.Tracks.Add(track);
                        DbContext.Update(dp);
                        DbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private async Task<Chinook.Models.Track> GetTack(long trackId)
        {
            var DbContext = await DbFactory.CreateDbContextAsync();
            var track = await DbContext.Tracks.FirstOrDefaultAsync(t => t.TrackId == trackId);

            return track;
        }

        public async Task CreateDefaultPlaylist(string currUsrId)
        {
            var DbContext = await DbFactory.CreateDbContextAsync();
            var defaultPlayList = await GetDefaultPlaylist();

            if (defaultPlayList == null)
            {
                var playList = new Chinook.Models.Playlist
                {
                    PlaylistId = GenPlayListId(),
                    Name = "My favorite tracks",
                };
                var userPlaylist = new UserPlaylist
                {
                    UserId = currUsrId,
                    PlaylistId = playList.PlaylistId,
                };

                await DbContext.Playlists.AddAsync(playList);
                await DbContext.SaveChangesAsync();

                await DbContext.UserPlaylists.AddAsync(userPlaylist);
                await DbContext.SaveChangesAsync();
            }
        }

        private async Task<Chinook.Models.Playlist> GetDefaultPlaylist()
        {
            var DbContext = await DbFactory.CreateDbContextAsync();
            var playList = await DbContext.Playlists.Include(x => x.Tracks).FirstOrDefaultAsync(t => t.Name == "My favorite tracks");

            return playList;
        }

        private async Task<bool> IsExistsPlaylistTack(long trackId, long playlistId)
        {
            var DbContext = await DbFactory.CreateDbContextAsync();
            var playLists = DbContext.Playlists.Include(x => x.Tracks).Where(x => x.PlaylistId == playlistId).ToList();
            var tracks = playLists.Where(x => x.Tracks.Any(y => y.TrackId == trackId));

            return tracks.Any();
        }

        public async Task<List<UserPlaylist>> GetUserPlaylists(string currUsrId)
        {
            var res = new List<UserPlaylist> { };
            var DbContext = await DbFactory.CreateDbContextAsync();
            res = DbContext.UserPlaylists.Include(x => x.Playlist).Where(x => x.UserId == currUsrId)?.ToList();

            return res.ToList();
        }
    }
}
