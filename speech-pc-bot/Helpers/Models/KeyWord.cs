using System.Collections.Generic;

namespace speech_pc_bot.Helpers.Models
{
    public class KeyWord
    {
        public double? Value { get; set; }
        public string Text { get; set; }
        public IEnumerable<string> Synonyms { get; set; }
    }
}