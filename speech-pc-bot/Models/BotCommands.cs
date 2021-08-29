using System.Collections.Generic;
using System.Linq;
using speech_pc_bot.Helpers;
using speech_pc_bot.Helpers.Models;
using Telegram.Bot;

namespace speech_pc_bot.Models
{
    public class BotCommands : List<BotCommand>
    {
        public BotCommands(ITelegramBotClient client)
        {
            AddRange(new List<BotCommand>
            {
                new()
                {
                    Text = "lock-screen",
                    KeyWords = new List<KeyWord>
                    {
                        new() { Text = "блокир", Value = 0.5, Synonyms = new[] { "оф" } },
                        new() { Text = "экран", Synonyms = new[] { "ноут", "комп", "пк", "скрин" } }
                    },
                    Command = async (chatId) =>
                    {
                        ComputerWorker.LockWorkStation();
                        await client.SendTextMessageAsync(chatId, "Экран заблокирован ✔️");
                    }
                },
                new()
                {
                    Text = "mute-pc",
                    KeyWords = new List<KeyWord>
                    {
                        new() { Text = "выключи", Value = 0.5, Synonyms = new[] { "оф" } },
                        new() { Text = "звук", Synonyms = new[] { "громкость" } }
                    },
                    Command = async (chatId) =>
                    {
                        ComputerWorker.Mute();
                        await client.SendTextMessageAsync(chatId, "Звук выключен ✔️");
                    }
                },
                new()
                {
                    Text = "unmute-pc",
                    KeyWords = new List<KeyWord>
                    {
                        new() { Text = "включи", Value = 0.5, Synonyms = new[] { "оф" } },
                        new() { Text = "звук", Synonyms = new[] { "громкость" } }
                    },
                    Command = async (chatId) =>
                    {
                        ComputerWorker.Mute();
                        await client.SendTextMessageAsync(chatId, "Звук включен ✔️");
                    }
                },
                new()
                {
                    Text = "unmute-pc",
                    KeyWords = new List<KeyWord>
                    {
                        new() { Text = "меньш", Value = 0.5, Synonyms = new[] { "оф" } },
                        new() { Text = "звук", Synonyms = new[] { "громкость" } }
                    },
                    Command = async (chatId) =>
                    {
                        ComputerWorker.VolDown();
                        await client.SendTextMessageAsync(chatId, "Громкость уменьшена ✔️");
                    }
                },
                new()
                {
                    Text = "open-browser",
                    KeyWords = new List<KeyWord>
                    {
                        new() { Text = "откр", Value = 0.5},
                        new() { Text = "браузер", Synonyms = new[] { "гугл" } }
                    },
                    Command = async (chatId) =>
                    {
                        ComputerWorker.OpenBrowser();
                        await client.SendTextMessageAsync(chatId, "Браузер запущен ✔️");
                    }
                },
                new()
                {
                    Text = "hide-windows",
                    KeyWords = new List<KeyWord>
                    {
                        new() { Text = "сверн", Value = 0.5},
                        new() { Text = "окна", Synonyms = new[] { "приложения" } }
                    },
                    Command = async (chatId) =>
                    {
                        ComputerWorker.HideWindows();
                        await client.SendTextMessageAsync(chatId, "Все окна свернуты ✔️");
                    }
                }
            });
        }

        public BotCommand FindByText(string text) => this.FirstOrDefault(x => x.Text == text);

        public BotCommand FindByWords(string words) =>
            this.FirstOrDefault(x => AlgorithmCompare.Compare(words, x.KeyWords));
    }
}