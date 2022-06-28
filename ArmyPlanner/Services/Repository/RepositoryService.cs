using ArmyPlanner.Interfaces;
using ArmyPlanner.Models.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArmyPlanner.Services.Repository
{
    public class RepositoryService : IRepositoryService
    {
        #region properties

        private readonly IStorageService _storageService;
        private readonly IHttpService _httpService;

        // => Load from Azure Storage Source
        //// the repository host url
        //private const string REPOSITORY_HOST = "https://armyplaner.blob.core.windows.net";
        //// the repository container path
        //private const string REPOSITORY_CONTAINER = "/games";
        // => Load from GitHub-Source
        // the repository host url
        private const string REPOSITORY_HOST = "https://raw.githubusercontent.com";
        // the repository container path
        private const string REPOSITORY_CONTAINER = "/lk-code/armyplanner/main";
        // the local storage folder (public for testing methods)
        public const string REPOSITORY_LOCALSTORAGE_FOLDER = "data";
        // the local storage index file
        private const string INDEX_FILE_NAME = "armyplanner-index.json";

        private List<GameEntry> _gamesInRepository = null;
        private List<GameEntry> _gamesInLocalStorage = null;
        private string _basePathForData = null;

        #endregion

        #region constructors

        public RepositoryService(IStorageService storageService,
            IHttpService httpService)
        {
            this._storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            this._httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));

            this._gamesInRepository = new List<GameEntry>();
            this._gamesInLocalStorage = new List<GameEntry>();
        }

        #endregion

        #region logic

        public void SetBasePathForData(string basePathForData)
        {
            this._basePathForData = basePathForData;
        }

        /// <summary>
        /// loads the index for with all subscribed games from the local storage
        /// </summary>
        /// <returns>a list with all local stored games</returns>
        public async Task<List<GameEntry>> GetSubscribedGamesAsync()
        {
            string indexJsonData = await this._storageService.GetDataAsync(INDEX_FILE_NAME,
            $"{this._basePathForData}{Path.DirectorySeparatorChar}{REPOSITORY_LOCALSTORAGE_FOLDER}");

            if (string.IsNullOrEmpty(indexJsonData))
            {
                // no index file (maybe first app-start, etc.)
                return new List<GameEntry>();
            }

            List<GameEntry> gameEntries = JsonConvert.DeserializeObject<List<GameEntry>>(indexJsonData);

            return gameEntries;
        }

        /// <summary>
        /// returns a list of all games from the repository.
        /// </summary>
        /// <param name="forceUpdate">if TRUE updates first the list of all games. if FALSE, returns only the local list.</param>
        /// <returns>the list of all loaded games</returns>
        public async Task<List<GameEntry>> GetGamesFromRepositoryAsync(bool forceUpdate = false)
        {
            if (forceUpdate == true
                || this._gamesInRepository.Count <= 0)
            {
                this._gamesInRepository = await this.GetGamesFromRepositoryAsync();
            }

            return this._gamesInRepository;
        }

        /// <summary>
        /// loads all available games from the repository and returns all as a list.
        /// </summary>
        /// <returns>all loaded games as a list.</returns>
        private async Task<List<GameEntry>> GetGamesFromRepositoryAsync()
        {
            string gamesJsonUrl = "/index.json";
            JObject gameIndexJson = await this.GetJObjectFromUrlInRepositoryAsync(gamesJsonUrl);
            JArray gameEntriesIndexJson = (JArray)gameIndexJson["games"];

            List<GameEntry> gameEntries = new List<GameEntry>();
            if (gameEntriesIndexJson != null)
            {
                List<GameIndex> gameIndexPaths = gameEntriesIndexJson.ToObject<List<GameIndex>>();

                // load games
                foreach (GameIndex gameIndex in gameIndexPaths)
                {
                    GameEntry loadedGameEntry = await this.GetGameEntryByPath(gameIndex.Path);

                    gameEntries.Add(loadedGameEntry);
                }
            }

            return gameEntries;
        }

        /// <summary>
        /// loads the data for the game in the given repository-path and returns a GameEntry-instance of it.
        /// </summary>
        /// <param name="gamePath">the path to the game.</param>
        /// <returns>the generated GameEntry-instance for the game.</returns>
        private async Task<GameEntry> GetGameEntryByPath(string gamePath)
        {
            string pathToGameIndex = $"{gamePath}/index.json";

            GameEntry loadedGameEntry = await this.GetFromUrlInRepositoryAsync<GameEntry>(pathToGameIndex);
            loadedGameEntry.Path = gamePath;

            loadedGameEntry.Codices = loadedGameEntry.Codices
                .Where(x => x.Enabled != false)
                .ToList();

            return loadedGameEntry;
        }

        /// <summary>
        /// load a JSON from the given URL and returns it as JObject.
        /// </summary>
        /// <param name="requestUrl">the URL from which the JSON should be loaded.</param>
        /// <returns>the JSON-Response as JObject.</returns>
        private async Task<JObject> GetJObjectFromUrlInRepositoryAsync(string requestUrl)
        {
            string responseJson = await this.GetJsonFromUrlInRepositoryAsync(requestUrl);
            JObject json = JObject.Parse(responseJson);

            return json;
        }

        /// <summary>
        /// load a JSON-String from the given URL and returns it.
        /// </summary>
        /// <typeparam name="T">the type into which the JSON should be parsed.</typeparam>
        /// <param name="requestUrl">the URL from which the JSON should be loaded.</param>
        /// <returns>the JSON-Response as T.</returns>
        private async Task<T> GetFromUrlInRepositoryAsync<T>(string requestUrl)
        {
            string responseJson = await this.GetJsonFromUrlInRepositoryAsync(requestUrl);
            T obj = JsonConvert.DeserializeObject<T>(responseJson);

            return obj;
        }

        /// <summary>
        /// load a JSON-String from the given URL and returns it.
        /// </summary>
        /// <param name="requestUrl">the URL from which the JSON should be loaded.</param>
        /// <returns>the JSON-Response as string.</returns>
        private async Task<string> GetJsonFromUrlInRepositoryAsync(string requestUrl)
        {
            string requestUri = $"{REPOSITORY_HOST}{REPOSITORY_CONTAINER}{requestUrl}";
            string responseJson = await this._httpService.GetStringAsync(requestUri);

            return responseJson;
        }

        /// <summary>
        /// subscribes to the given game and downloads all content for this game (all codezies and all available languages for this content)
        /// </summary>
        /// <param name="gameToDownload">the game to be subscribed to.</param>
        public async Task SubscribeGameAsync(IDownloadableGame gameToDownload)
        {
            string dataDirectoryPath = $"{this._basePathForData}{Path.DirectorySeparatorChar}{REPOSITORY_LOCALSTORAGE_FOLDER}";
            GameEntry gameEntry = gameToDownload.GetGameToDownload();
            List<CodexEntry> codexEntries = gameEntry.Codices
                .Where(x => x.Enabled != false)
                .ToList();

            int numberOfCodices = codexEntries.Count;

            // download codices
            int currentCodexIndex = 1;
            foreach (CodexEntry codexEntry in codexEntries)
            {
                string gameDirectoryPath = $"{dataDirectoryPath}{Path.DirectorySeparatorChar}{gameEntry.Path.Remove(0, 1)}";

                // load codex
                string codexPath = $"{gameEntry.Path}/{codexEntry.Path}";
                Models.Codices.Codex parsedCodex = null;

                string codexJson = await this.GetJsonFromUrlInRepositoryAsync(codexPath);
                await this._storageService.WriteDataAsync(codexEntry.Path,
                    codexJson,
                    gameDirectoryPath);
                parsedCodex = JsonConvert.DeserializeObject<Models.Codices.Codex>(codexJson);

                if (parsedCodex == null)
                {
                    continue;
                }

                // load codex languages
                foreach (Models.Codices.CodexLanguage codexLanguage in parsedCodex.Languages)
                {
                    string codexLanguagePath = $"{gameEntry.Path}/{codexLanguage.Path}";

                    string codexLanguageJson = await this.GetJsonFromUrlInRepositoryAsync(codexLanguagePath);
                    await this._storageService.WriteDataAsync(codexLanguage.Path,
                        codexLanguageJson,
                        gameDirectoryPath);
                }

                currentCodexIndex++;
                gameToDownload.SetDownloadProgress(true, ((currentCodexIndex / numberOfCodices) * 100));
            }

            gameToDownload.SetDownloadProgress(false, 0);

            GameEntry existingGameEntry = this._gamesInLocalStorage
                .Where(x => x.Path.ToLowerInvariant() == gameEntry.Path.ToLowerInvariant())
                .FirstOrDefault();
            if (existingGameEntry != null)
            {
                this._gamesInLocalStorage.Remove(existingGameEntry);
            }
            this._gamesInLocalStorage.Add(gameEntry);

            // update game index file
            string indexJsonData = JsonConvert.SerializeObject(this._gamesInLocalStorage);
            await this._storageService.WriteDataAsync(INDEX_FILE_NAME,
                indexJsonData,
            dataDirectoryPath);
        }

        /// <summary>
        /// updates the data for the given game entry
        /// </summary>
        /// <param name="gameEntryToUpdate">the game entry to be updated.</param>
        /// <returns>the updated game entry</returns>
        public async Task<GameEntry> UpdateGameAsync(GameEntry gameEntryToUpdate)
        {
            // update game
            GameEntry updatedGameEntry = await this.GetGameEntryByPath(gameEntryToUpdate.Path);

            // update index
            GameEntry existingGameEntry = this._gamesInLocalStorage
                .Where(x => x.Path.ToLowerInvariant() == gameEntryToUpdate.Path.ToLowerInvariant())
                .FirstOrDefault();
            if (existingGameEntry != null)
            {
                this._gamesInLocalStorage.Remove(existingGameEntry);
            }
            this._gamesInLocalStorage.Add(updatedGameEntry);

            return updatedGameEntry;
        }

        /// <summary>
        /// returns the local storage file path for the given game path
        /// </summary>
        /// <param name="gamePath">the game-path</param>
        /// <returns>the complete local storage path for the game</returns>
        public string GetGameStorageFilePath(string gamePath)
        {
            string path = gamePath;

            if (path.StartsWith("/")
                || path.StartsWith("\\"))
            {
                path = path.Remove(0, 1);
            }
            return $"{REPOSITORY_LOCALSTORAGE_FOLDER}{Path.DirectorySeparatorChar}{path}";
        }

        #endregion
    }
}