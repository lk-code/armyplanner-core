using ArmyPlanner.EventArgs;
using ArmyPlanner.Interfaces;
using ArmyPlanner.Models.Repositories;

namespace ArmyPlanner.Test.Base.Models
{
    public class DownloadableGame : IDownloadableGame
    {
        #region properties

        public event EventHandler<DownloadProgressChangedEventArgs> DownloadProgressChanged;

        public GameEntry GameEntry { get; set; }
        public double DownloadProgress { get; set; }
        public bool Enabled { get; set; } = true;

        #endregion

        #region constrcutors

        public DownloadableGame(GameEntry gameEntry)
        {
            this.GameEntry = gameEntry;

            this.DownloadProgressChanged?.Invoke(this, new DownloadProgressChangedEventArgs
            {
                IsDownloadActive = false,
                DownloadProgress = 0
            });
        }

        #endregion

        #region logic

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isDownloadActive"></param>
        /// <param name="progress"></param>
        public void SetDownloadProgress(bool isDownloadActive, double progress)
        {
            this.DownloadProgress = progress;

            this.DownloadProgressChanged?.Invoke(this, new DownloadProgressChangedEventArgs
            {
                IsDownloadActive = isDownloadActive,
                DownloadProgress = progress
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GameEntry GetGameToDownload()
        {
            return this.GameEntry;
        }

        #endregion
    }
}