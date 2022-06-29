using ArmyPlanner.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ArmyPlanner.Services.Codex
{
    public class CodexService : ICodexService
    {
        #region events

        #endregion

        #region properties

        private readonly IStorageService _storageService;
        private readonly ITranslationService _translationService;
        private readonly IRepositoryService _repositoryService;

        // the local storage folder (public for testing methods)
        public static readonly string CODICIES_LOCALSTORAGE_FOLDER = "data";

        private string _basePathForCodexData;

        #endregion

        #region constrcutors

        public CodexService(IStorageService storageService,
            ITranslationService TranslationService,
            IRepositoryService repositoryService)
        {
            this._storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            this._translationService = TranslationService ?? throw new ArgumentNullException(nameof(TranslationService));
            this._repositoryService = repositoryService ?? throw new ArgumentNullException(nameof(repositoryService));
        }

        #endregion

        #region logic

        public void SetBasePathForCodexData(string basePathForCodexData)
        {
            this._basePathForCodexData = basePathForCodexData;
        }

        /// <summary>
        /// loads a codex for the given game-path and the codex file.
        /// </summary>
        /// <param name="gamePath">the game-path</param>
        /// <param name="codexPath">the codex-file</param>
        /// <returns>the codex-instance</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Models.Codices.Codex> GetCodexAsync(string gamePath, string codexPath)
        {
            string storageDirectory = this.GetCodiciesStorageFilePath(gamePath);
            string codexJsonContent = await this._storageService.GetDataAsync(codexPath,
                storageDirectory);

            if (string.IsNullOrEmpty(codexJsonContent))
            {
                throw new InvalidOperationException($"codexJsonContent is null, empty or invalid");
            }

            Models.Codices.Codex codex = JsonConvert.DeserializeObject<Models.Codices.Codex>(codexJsonContent);
            if (codex == null)
            {
                throw new InvalidOperationException($"codex for {storageDirectory}/{codexPath} is null or invalid");
            }

            if (!string.IsNullOrEmpty(codex.Meta.BasedOnCodexKey))
            {
                codex.BasedOnCodex = await this.GetCodexAsync(gamePath, $"{codex.Meta.BasedOnCodexKey}.json");
            }

            return codex;
        }

        /// <summary>
        /// loads the translations from the requested codex language file and returns it as a dictionary
        /// </summary>
        /// <param name="codex">the codex to translate</param>
        /// <param name="codexLanguage">the requested language</param>
        /// <returns>all translations in a dictionary</returns>
        public async Task<Dictionary<string, string>> GetCodexTranslationsAsync(Models.Codices.Codex codex, Models.Codices.CodexLanguage codexLanguage)
        {
            string storageDirectory = this._repositoryService.GetGameStorageFilePath($"/{codex.Meta.Game.Key}");
            string codexTranslationFileName = this.GetCodexTranslationsStorageFileName(codex.Meta.Codex.Key,
                codexLanguage.LanguageCode);

            string codexTranslationJsonData = await this._storageService.GetDataAsync(codexTranslationFileName,
            $"{this._basePathForCodexData}{Path.DirectorySeparatorChar}{storageDirectory}");
            Dictionary<string, string> translations = JsonConvert.DeserializeObject<Dictionary<string, string>>(codexTranslationJsonData);

            return translations;
        }

        /// <summary>
        /// generates the file name for the local codex translation file by the codex key and the requested language code.
        /// </summary>
        /// <param name="codexKey">the key for the requested codex.</param>
        /// <param name="languageCode">the language-code for the requested language (like 'de' or 'en').</param>
        /// <returns>the file-name for the reuqested codex translation file.</returns>
        public string GetCodexTranslationsStorageFileName(string codexKey, string languageCode)
        {
            string storageFileName = $"{codexKey}.{languageCode}.json".ToLowerInvariant();

            return storageFileName;
        }

        /// <summary>
        /// returns the local storage file path for the given game path
        /// </summary>
        /// <param name="gamePath">the game-path</param>
        /// <returns>the complete local storage path for the codicies</returns>
        public string GetCodiciesStorageFilePath(string gamePath)
        {
            string path = gamePath;

            if (path.StartsWith("/")
                || path.StartsWith("\\"))
            {
                path = path.Remove(0, 1);
            }

            string storagePath = $"{this._basePathForCodexData}{Path.DirectorySeparatorChar}{CODICIES_LOCALSTORAGE_FOLDER}{Path.DirectorySeparatorChar}{path}";

            return storagePath;
        }

        #endregion
    }
}