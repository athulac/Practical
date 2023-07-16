using Chinook.Models;

namespace Chinook.Services
{
    public interface IPlaylistService
    {
        Task ModifyUserPlaylist(long playlistId, long trackId);       
        Task CreateUserPlaylist(ClientModels.Playlist playlist, ClientModels.PlaylistTrack track, string currUsrId);        
        Task AddToFaviroute(bool isFav, long trackId, string currUsrId);
        Task<List<UserPlaylist>> GetUserPlaylists(string currUsrId);
    }
}
