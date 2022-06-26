namespace ArmyPlanner.EventArgs
{
    public class DownloadProgressChangedEventArgs : System.EventArgs
    {
        public bool IsDownloadActive { get; set; } = false;
        public double DownloadProgress { get; set; } = 0;
    }
}