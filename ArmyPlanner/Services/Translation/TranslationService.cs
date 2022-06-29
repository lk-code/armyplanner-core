using ArmyPlanner.Interfaces;
using ArmyPlanner.Services.Translation.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArmyPlanner.Services.Translation
{
    public class TranslationService : ITranslationService
    {
        #region properties

        private const string TEMPLATE_START = "{{";
        private const string TEMPLATE_END = "}}";

        #endregion

        #region constrcutors

        public TranslationService()
        {
        }

        #endregion

        #region logic

        public string Translate(string template, Dictionary<string, string> translations)
        {
            if (translations == null)
            {
                return template;
            }

            try
            {
                Dictionary<string, List<string>> templateData = this.GetTemplateData(template);
                string translatedText = template;
                foreach (KeyValuePair<string, List<string>> data in templateData)
                {
                    translatedText = this.Translate(translatedText, translations[data.Key], data.Value);
                }

                return translatedText;
            }
            catch (KeyNotFoundException)
            {
                return template;
            }
        }

        private string Translate(string template, string translation, List<string> templateParameter)
        {
            string translated = translation;
            translated = this.RenderTranslation(translated, templateParameter.ToArray());
            string templatePlaceholder = TEMPLATE_START + templateParameter[0] + TEMPLATE_END;

            string result = template.Replace(templatePlaceholder, translated);

            return result;
        }

        private string RenderTranslation(string translation, params string[] args)
        {
            for (int i = 1; i < args.Length; i++)
            {
                translation = translation.Replace("{" + (i - 1) + "}", args[i]);
            }

            if (translation.Contains("{"))
            {
                args = new [] { "", "", "", "", "", "", "", "", "", "" };

                translation = this.RenderTranslation(translation, args);
            }

            return translation;
        }

        private List<string> SplitTemplate(string templateValue)
        {
            if (!templateValue.Contains(","))
            {
                return new List<string> {
                    templateValue
                };
            }

            List<string> parameters = templateValue.Split(',').ToList();

            return parameters;
        }

        private List<string> GetTemplateValues(string template)
        {
            if (string.IsNullOrEmpty(template))
            {
                return new List<string>();
            }

            try
            {
                List<StringPosition> positions = new List<StringPosition>();
                StringPosition position = new StringPosition();
                for (int i = 0; i < template.Length; i++)
                {

                    // check start
                    if (template[i] == '{'
                        && ((i + 1) < template.Length && template[i + 1] == '{'))
                    {
                        position.Start = (i + 2);
                    }

                    if (position.Start > 0
                        && template[i] == '}'
                        && ((i + 1) < template.Length && template[i + 1] == '}'))
                    {
                        position.End = i;

                        positions.Add(position);
                        position = new StringPosition();
                    }
                }

                List<string> templateValues = new List<string>();
                foreach (StringPosition stringPosition in positions)
                {
                    string templateValue = template.Substring(stringPosition.Start, stringPosition.End - stringPosition.Start);
                    templateValues.Add(templateValue);
                }

                return templateValues;
            }
            catch (ArgumentOutOfRangeException)
            {
                return new List<string>();
            }
        }

        public Dictionary<string, List<string>> GetTemplateData(string template)
        {
            List<string> templateValues = this.GetTemplateValues(template);
            Dictionary<string, List<string>> templatesParameter = new Dictionary<string, List<string>>();
            foreach (string templateValue in templateValues)
            {
                List<string> templateParameter = this.SplitTemplate(templateValue);
                string templateKey = templateParameter[0];
                templateParameter.RemoveAt(0);
                templateParameter.Insert(0, templateValue);
                templatesParameter.Add(templateKey, templateParameter);
            }

            return templatesParameter;
        }

        #endregion
    }
}