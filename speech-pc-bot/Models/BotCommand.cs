using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using speech_pc_bot.Helpers.Models;

namespace speech_pc_bot.Models
{
    public class BotCommand
    {
        public string Text { get; set; }
        public IEnumerable<KeyWord> KeyWords { get; set; }
        public Func<long, Task> Command { get; set; }
    }
}