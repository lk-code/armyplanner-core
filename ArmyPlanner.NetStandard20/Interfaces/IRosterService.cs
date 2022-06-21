using ArmyPlanner.Exceptions;
using ArmyPlanner.Models.Rosters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArmyPlanner.Interfaces
{
    public interface IRosterService
    {
        /// <summary>
        /// returns a list of all locally stored rosters.
        /// </summary>
        /// <param name="forceUpdate">if true, all rosters are read again locally, otherwise the saved list is simply returned - default is false</param>
        /// <returns>the list of Roster objects stored on the device.</returns>
        Task<List<Roster>> GetStoredRostersAsync(bool forceUpdate = false);
        /// <summary>
        /// saves the given Roster object.
        /// </summary>
        /// <param name="roster">the roster-object</param>
        /// <returns>the saved (and updated) roster-object</returns>
        Task<Roster> SaveAsync(Roster roster);
        /// <summary>
        /// checks the information in this Roster object for validity. in case of errors an exception is thrown.
        /// </summary>
        /// <param name="roster">the Roster object to be checked.</param>
        /// <exception cref="MissingGameException"></exception>
        Task EnsureRosterAsync(Roster roster);
        /// <summary>
        /// used to specify the base path within the device storage.
        /// </summary>
        /// <param name="path">the public storage path for the user data (like documents). user data is stored in this folder (roster files are stored here)</param>
        void SetBasePathForRosterData(string path);
    }
}