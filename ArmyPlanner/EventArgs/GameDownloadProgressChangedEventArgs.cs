using ArmyPlanner.Interfaces;

namespace ArmyPlanner.EventArgs
{
    public class GameDownloadProgressChangedEventArgs : System.EventArgs
    {
        public IDownloadableGame DownloadableGame { get; set; }
        public bool IsDownloadActive { get; set; }
        public double DownloadProgress { get; set; }
    }
}