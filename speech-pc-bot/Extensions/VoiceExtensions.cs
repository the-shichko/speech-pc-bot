using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace speech_pc_bot.Extensions
{
    public static class VoiceExtensions
    {
        public static async Task<byte[]> GetBytes(this Voice voice, ITelegramBotClient botClient)
        {
            var tempDirectory = Directory.GetCurrentDirectory() + "\\temp-voices";
            var file = await botClient.GetFileAsync(voice.FileId);

            var millis = DateTime.Now.Millisecond;

            if (!Directory.Exists(tempDirectory))
                Directory.CreateDirectory(tempDirectory);

            await using (var saveImageStream =
                new FileStream($"{tempDirectory}\\voice-{millis}.ogg", FileMode.Create))
            {
                await botClient.DownloadFileAsync(file.FilePath ?? string.Empty, saveImageStream);
            }

            return await System.IO.File.ReadAllBytesAsync($"{tempDirectory}\\voice-{millis}.ogg");
        }
    }
}