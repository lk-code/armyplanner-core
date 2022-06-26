using System.Collections.Generic;

namespace ArmyPlanner.Interfaces
{
    public interface ITranslationService
    {
        /// <summary>
        /// 
        /// examples:
        /// 
        /// "{{weapon_type_assault}} {{title_dices,6}}" => "Assault D6"
        /// "{{weapon_fusion_blaster_name}}" => "Fusionblaster"
        /// </summary>
        /// <param name="template">the requested translation like "{{keyword_wargear}}"</param>
        /// <param name="translations">the list of all available translations</param>
        /// <returns>the translated value, or the key itself if it was not found or the translation list is empty.</returns>
        string Translate(string template, Dictionary<string, string> translations);
        /// <summary>
        /// Returns the parts of a requested translation as a dictionary:
        /// 
        /// template: "{{weapon_type_assault}} {{title_dices,6}}"
        /// 
        /// result:
        /// {
        ///     "weapon_type_assault":
        ///     [
        ///         "weapon_type_assault"
        ///     ],
        ///     "title_dices":
        ///     [
        ///         "title_dices,6",
        ///         "6"
        ///     ]
        /// }
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        Dictionary<string, List<string>> GetTemplateData(string template);
    }
}