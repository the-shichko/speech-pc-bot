using System;
using System.Threading.Tasks;
using speech_pc_bot.Services;

namespace speech_pc_bot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            _ = new TelegramService();
            await Task.Delay(-1);
        }
    }
}