using System;
using System.Threading.Tasks;
using speech_pc_bot.Models;
using Telegram.Bot;

namespace speech_pc_bot.Services
{
    public abstract class BaseExecuteService<T> : IBotExecute<T>
    {
        protected static ITelegramBotClient _botClient;
        protected static BotCommands _commands;
        protected abstract Func<T, Task> FuncExecute { get; }
        protected BaseExecuteService(ITelegramBotClient client)
        {
            _botClient = client;
            _commands = new BotCommands(client);
        }

        public async Task Execute(T modelTelegram)
        {
            try
            {
                await FuncExecute.Invoke(modelTelegram);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}