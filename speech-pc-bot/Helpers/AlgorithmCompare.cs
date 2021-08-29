using System.Collections.Generic;
using System.Linq;
using speech_pc_bot.Helpers.Models;

namespace speech_pc_bot.Helpers
{
    public class AlgorithmCompare
    {
        public static bool Compare(string originalText, IEnumerable<KeyWord> keyWords)
        {
            #region Set value keyWors

            var text = originalText.ToLower();
            var keyWordsList = keyWords.ToList();
            var allValue = keyWordsList.Sum(x => x.Value);

            keyWordsList.ForEach(x => { x.Value ??= (1 - allValue) / keyWordsList.Count(word => word.Value == null); });

            #endregion

            var percent = keyWordsList
                .Where(keyWord =>
                    text.Contains(keyWord.Text.ToLower()) || keyWord.Synonyms.Any(text.Contains))
                .Sum(keyWord => keyWord.Value);
            return percent > 0.7;
        }
    }
}