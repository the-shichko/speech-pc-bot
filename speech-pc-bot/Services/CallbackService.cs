using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using speech_pc_bot.Helpers;
using speech_pc_bot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using BotCommand = speech_pc_bot.Models.BotCommand;

namespace speech_pc_bot.Services
{
    public class CallbackService : BaseExecuteService<CallbackQuery>
    {
        public CallbackService(ITelegramBotClient client) : base(client)
        {
        }

        protected override Func<CallbackQuery, Task> FuncExecute { get; } = async (callback) =>
        {
            var model = _commands.FindByText(callback.Data);
            await model.Command.Invoke(callback.From.Id);
        };
    }
}