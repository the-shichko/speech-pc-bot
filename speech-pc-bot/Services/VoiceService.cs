using System;
using System.Threading.Tasks;
using speech_pc_bot.Extensions;
using speech_pc_bot.Speech;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace speech_pc_bot.Services
{
    public class VoiceService : BaseExecuteService<Message>
    {
        private static YandexSpeech _yandexSpeech;

        public VoiceService(ITelegramBotClient client) : base(client)
        {
            _yandexSpeech = new YandexSpeech();
        }

        protected override Func<Message, Task> FuncExecute { get; } = async (message) =>
        {
            var bytes = await message.Voice.GetBytes(_botClient);
            var text = await _yandexSpeech.SendVoice(bytes);

            var command = _commands.FindByWords(text);

            if (command != null)
                await command.Command.Invoke(message.Chat.Id);
            else
                await _botClient.SendTextMessageAsync(message.Chat.Id, "Команда не распознана ❌");
        };
    }
}