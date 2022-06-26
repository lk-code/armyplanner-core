using ArmyPlanner.Interfaces;

namespace ArmyPlanner.EventArgs
{
    public class GameDownloadProgressChangedEventArgs : System.EventArgs
    {
        public IDownloadableGame DownloadableGame { get; set; } = null;
        public bool IsDownloadActive { get; set; } = false;
        public double DownloadProgress { get; set; } = 0;
    }
}