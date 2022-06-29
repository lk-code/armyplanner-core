namespace ArmyPlanner.EventArgs
{
    public class DownloadProgressChangedEventArgs : System.EventArgs
    {
        public bool IsDownloadActive { get; set; }
        public double DownloadProgress { get; set; }
    }
}