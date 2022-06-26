using ArmyPlanner.Models.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArmyPlanner.Interfaces
{
    public interface IRepositoryService
    {
        /// <summary>
        /// returns a list of all games from the repository.
        /// </summary>
        /// <param name="forceUpdate">if TRUE updates first the list of all games. if FALSE, returns only the local list.</param>
        /// <returns>the list of all loaded games</returns>
        Task<List<GameEntry>> GetGamesFromRepositoryAsync(bool forceUpdate = false);
        /// <summary>
        /// loads the index for with all subscribed games from the local storage
        /// </summary>
        /// <returns>a list with all local stored games</returns>
        Task<List<GameEntry>> GetSubscribedGamesAsync();
        /// <summary>
        /// subscribes to the given game and downloads all content for this game (all codezies and all available languages for this content)
        /// </summary>
        /// <param name="gameToDownload">the game to be subscribed to.</param>
        Task SubscribeGameAsync(IDownloadableGame download);
        /// <summary>
        /// updates the data for the given game entry
        /// </summary>
        /// <param name="gameEntry">the game entry to be updated.</param>
        /// <returns>the updated game entry</returns>
        Task<GameEntry> UpdateGameAsync(GameEntry gameEntry);
        /// <summary>
        /// returns the local storage file path for the given game path
        /// </summary>
        /// <param name="gamePath">the game-path</param>
        /// <returns>the complete local storage path for the game</returns>
        string GetGameStorageFilePath(string gamePath);
        /// <summary>
        /// used to specify the base path within the device storage.
        /// </summary>
        /// <param name="path">the basic path for the app. app data is stored in this folder (The folder should not be public)</param>
        void SetBasePathForData(string basePathForData);
    }
}