using System.Threading.Tasks;

namespace ArmyPlanner.Interfaces
{
    public interface IStorageService
    {
        /// <summary>
        /// writes the given content to the specified file
        /// </summary>
        /// <param name="fileName">the file-name</param>
        /// <param name="folderPath">the target folder</param>
        /// <param name="content">the file content</param>
        Task WriteDataAsync(string fileName, string content, string folderPath);
        /// <summary>
        /// reads the entire content from the file and returns it as a string.
        /// </summary>
        /// <param name="fileName">the file-name</param>
        /// <param name="folderPath">the target folder</param>
        /// <returns>the content of the requested file</returns>
        Task<string> GetDataAsync(string fileName, string folderPath);
    }
}