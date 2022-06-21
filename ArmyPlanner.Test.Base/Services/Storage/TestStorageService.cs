using ArmyPlanner.Interfaces;

namespace ArmyPlanner.Test.Base.Services.Storage
{
    internal class TestStorageService : IStorageService
    {
        #region properties

        private Dictionary<string, string> _registeredContent;

        #endregion

        #region constructors

        public TestStorageService()
        {
            this._registeredContent = new Dictionary<string, string>();
        }

        #endregion

        #region logic

        public async Task<string> GetDataAsync(string filename, string folderPath)
        {
            await Task.CompletedTask;

            string id = this.GetId(filename, folderPath);

            if (!this._registeredContent.ContainsKey(id))
            {
                return string.Empty;
            }

            string content = this._registeredContent[id];

            return content;
        }

        public async Task WriteDataAsync(string filename, string jsonData, string folderPath)
        {
            await Task.CompletedTask;

            string id = this.GetId(filename, folderPath);
            string content = jsonData;

            this._registeredContent[id] = content;
        }

        private string GetId(string filename, string folderPath)
        {
            return $"{folderPath}_{filename}";
        }

        #endregion
    }
}