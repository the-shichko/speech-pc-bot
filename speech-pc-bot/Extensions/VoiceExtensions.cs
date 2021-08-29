using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = Telegram.Bot.Types.File;

namespace speech_pc_bot.Extensions
{
    public static class VoiceExtensions
    {
        public static async Task<byte[]> GetBytes(this Voice voice, ITelegramBotClient botClient)
        {
            var file = await botClient.GetFileAsync(voice.FileId);

            var millis = DateTime.Now.Millisecond;
            await using (var saveImageStream = new FileStream(Directory.GetCurrentDirectory() + $"\voice-{millis}.ogg", FileMode.Create))
            {
                await botClient.DownloadFileAsync(file.FilePath ?? string.Empty, saveImageStream);
            }
            
            return await System.IO.File.ReadAllBytesAsync(Directory.GetCurrentDirectory() + $"\voice-{millis}.ogg");
        }
    }
}