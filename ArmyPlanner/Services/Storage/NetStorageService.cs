using ArmyPlanner.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace ArmyPlanner.Services.Storage
{
    public class NetStorageService : IStorageService
    {
        #region properties


        #endregion

        #region constrcutors

        public NetStorageService()
        {
        }

        #endregion

        #region logic

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public async Task<string> GetDataAsync(string filename, string folderPath)
        {
#if NET5_0_OR_GREATER
            string filePath = Path.Combine(folderPath, filename);

            if (!File.Exists(filePath))
            {
                return string.Empty;
            }

            return await File.ReadAllTextAsync(filePath);
#else
            return string.Empty;
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="content"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public async Task WriteDataAsync(string filename, string content, string folderPath)
        {
#if NET5_0_OR_GREATER
            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, filename);

            await File.WriteAllTextAsync(filePath, content);
#endif
        }

        #endregion
    }
}