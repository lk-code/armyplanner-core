using ArmyPlanner.EventArgs;
using ArmyPlanner.Models.Repositories;
using System;

namespace ArmyPlanner.Interfaces
{
    public interface IDownloadableGame
    {
        /// <summary>
        /// triggered when the download progress changes.
        /// </summary>
        event EventHandler<DownloadProgressChangedEventArgs> DownloadProgressChanged;
        /// <summary>
        /// sets the progress value for the download progress.
        /// </summary>
        /// <param name="isDownloadActive">indicates whether the download is currently active</param>
        /// <param name="progress">the Progress value</param>
        void SetDownloadProgress(bool isDownloadActive, double progress);
        /// <summary>
        /// returns the GameEntry-Object
        /// </summary>
        /// <returns></returns>
        GameEntry GetGameToDownload();
    }
}