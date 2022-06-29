using ArmyPlanner.Exceptions;
using ArmyPlanner.Interfaces;
using ArmyPlanner.Models.Repositories;
using ArmyPlanner.Services.Roster.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace ArmyPlanner.Services.Roster
{
    public class RosterService : IRosterService
    {
        #region properties

        private readonly IStorageService _storageService;
        private readonly IRepositoryService _repositoryService;

        // the local storage folder
        private const string ROSTER_LOCALSTORAGE_FOLDER = "rosters";
        // the local storage index file
        private const string INDEX_FILE_NAME = "roster-index.json";
        // the file extension for roster files
        private const string ROSTER_FILE_EXTENSION = "apr";

        private string _basePathForRosterData;
        private readonly List<ArmyPlanner.Models.Rosters.Roster> _storedRosters;

        #endregion

        #region constructors

        public RosterService(IStorageService storageService,
            IRepositoryService repositoryService)
        {
            this._storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            this._repositoryService = repositoryService ?? throw new ArgumentNullException(nameof(repositoryService));

            this._storedRosters = new List<ArmyPlanner.Models.Rosters.Roster>();
        }

        #endregion

        #region logic

        public void SetBasePathForRosterData(string basePathForRosterData)
        {
            this._basePathForRosterData = basePathForRosterData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task LoadRosterIndexAsync()
        {
            this._storedRosters.Clear();
            List<RosterIndexEntry> rosterIndex = await this.GetRosterIndexAsync();

            if (rosterIndex.Count <= 0)
            {
                return;
            }

            foreach (RosterIndexEntry indexEntry in rosterIndex)
            {
                string rosterFileName = $"{indexEntry.FileId}.{ROSTER_FILE_EXTENSION}";
                string rosterData = await this._storageService.GetDataAsync(
                    rosterFileName,
                    $"{this._basePathForRosterData}{Path.DirectorySeparatorChar}{ROSTER_LOCALSTORAGE_FOLDER}");
                ArmyPlanner.Models.Rosters.Roster roster = JsonConvert.DeserializeObject<ArmyPlanner.Models.Rosters.Roster>(rosterData);
                this._storedRosters.Add(roster);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<List<RosterIndexEntry>> GetRosterIndexAsync()
        {
            string indexJsonData = await this._storageService.GetDataAsync(
                INDEX_FILE_NAME,
                $"{this._basePathForRosterData}{Path.DirectorySeparatorChar}{ROSTER_LOCALSTORAGE_FOLDER}");
            List<RosterIndexEntry> rosterIndex = new List<RosterIndexEntry>();
            if (!string.IsNullOrEmpty(indexJsonData.Trim()))
            {
                rosterIndex = JsonConvert.DeserializeObject<List<RosterIndexEntry>>(indexJsonData);
            }

            return rosterIndex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task SaveRosterIndexAsync(List<RosterIndexEntry> rosterIndex)
        {
            string indexJsonData = JsonConvert.SerializeObject(rosterIndex);

            await this._storageService.WriteDataAsync(
                INDEX_FILE_NAME,
                indexJsonData,
            $"{this._basePathForRosterData}{Path.DirectorySeparatorChar}{ROSTER_LOCALSTORAGE_FOLDER}");
        }

        /// <summary>
        /// returns a list of all locally stored rosters.
        /// </summary>
        /// <returns>the list of Roster objects stored on the device.</returns>
        public async Task<List<ArmyPlanner.Models.Rosters.Roster>> GetStoredRostersAsync()
        {
            return await this.GetStoredRostersAsync(false);
        }

        /// <summary>
        /// returns a list of all locally stored rosters.
        /// </summary>
        /// <param name="forceUpdate">if true, all rosters are read again locally, otherwise the saved list is simply returned - default is false</param>
        /// <returns>the list of Roster objects stored on the device.</returns>
        public async Task<List<ArmyPlanner.Models.Rosters.Roster>> GetStoredRostersAsync(bool forceUpdate = false)
        {
            if (forceUpdate == true
                || this._storedRosters.Count <= 0)
            {
                await this.LoadRosterIndexAsync();
            }

            return this._storedRosters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roster"></param>
        /// <returns></returns>
        public async Task<ArmyPlanner.Models.Rosters.Roster> SaveAsync(ArmyPlanner.Models.Rosters.Roster roster)
        {
            if (roster.Id.Equals(Guid.Empty))
            {
                // new roster

                roster.Id = Guid.NewGuid();

                // update roster index
                List<RosterIndexEntry> rosterIndex = await this.GetRosterIndexAsync();
                rosterIndex.Add(new RosterIndexEntry
                {
                    FileId = roster.Id.ToString()
                });
                await this.SaveRosterIndexAsync(rosterIndex);
            }

            // save file
            string rosterFileName = $"{roster.Id}.{ROSTER_FILE_EXTENSION}";
            string rosterJsonData = JsonConvert.SerializeObject(roster);
            await this._storageService.WriteDataAsync(
                rosterFileName,
                rosterJsonData,
            $"{this._basePathForRosterData}{Path.DirectorySeparatorChar}{ROSTER_LOCALSTORAGE_FOLDER}");

            // reload roster index
            await this.LoadRosterIndexAsync();

            return roster;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roster"></param>
        /// <returns></returns>
        /// <exception cref="MissingGameException"></exception>
        public async Task EnsureRosterAsync(ArmyPlanner.Models.Rosters.Roster roster)
        {
            string gameKey = roster.RequiredGame;

            List<GameEntry> subscribedGames = await this._repositoryService.GetSubscribedGamesAsync();
            List<GameEntry> matchingGames = subscribedGames.Where(x => x.Path == gameKey).ToList();
            if (matchingGames.Count() <= 0)
            {
                throw new MissingGameException(gameKey);
            }
        }

        #endregion
    }
}