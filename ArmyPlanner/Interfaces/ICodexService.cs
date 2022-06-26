using ArmyPlanner.Models.Codices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArmyPlanner.Interfaces
{
    public interface ICodexService
    {
        /// <summary>
        /// loads a codex for the given game-path and the codex file.
        /// </summary>
        /// <param name="gamePath">the game-path</param>
        /// <param name="codexPath">the codex-file</param>
        /// <returns>the codex-instance</returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<Codex> GetCodexAsync(string gamePath, string codexPath);
        /// <summary>
        /// loads the translations from the requested codex language file and returns it as a dictionary
        /// </summary>
        /// <param name="codex">the codex to translate</param>
        /// <param name="codexLanguage">the requested language</param>
        /// <returns>all translations in a dictionary</returns>
        Task<Dictionary<string, string>> GetCodexTranslationsAsync(Codex codex, CodexLanguage codexLanguage);
        /// <summary>
        /// used to specify the base path within the device storage.
        /// </summary>
        /// <param name="path">the basic path for the app. app data is stored in this folder (The folder should not be public)</param>
        void SetBasePathForCodexData(string path);
    }
}
