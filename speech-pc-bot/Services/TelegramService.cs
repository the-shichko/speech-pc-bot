using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace speech_pc_bot.Services
{
    public class TelegramService
    {
        private const string Token = "your-telegram-token";
        private readonly ITelegramBotClient _botClient;

        private static IBotExecute<Message> _voiceExecuteService;
        private static IBotExecute<CallbackQuery> _callbackExecuteService;
        public TelegramService()
        {
            _botClient = new TelegramBotClient(Token);
            _botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, _receiverOptions);

            _voiceExecuteService = new VoiceService(_botClient);
            _callbackExecuteService = new CallbackService(_botClient);
        }

        private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            switch (update.Type)
            {
                case UpdateType.Message when update.Message is { Text: { } }: //&& update.Message!.Text.StartsWith("/"):
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, "fsdf", replyMarkup: new InlineKeyboardMarkup(
                        new InlineKeyboardButton("lock")
                        {
                            CallbackData = "lock-screen"
                        }));
                    break;
                case UpdateType.Message when update.Message is { Voice: { } }:
                    await _voiceExecuteService.Execute(update.Message);
                    break;
                case UpdateType.CallbackQuery:
                    await _callbackExecuteService.Execute(update.CallbackQuery);
                    break;
                default:
                    throw new Exception($"Implement update.Type - {update.Type}");
            }
        }

        private static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is ApiRequestException apiRequestException)
            {
                await botClient.SendTextMessageAsync(337383405, apiRequestException.ToString(),
                    cancellationToken: cancellationToken);
            }
        }

        private static readonly ReceiverOptions _receiverOptions = new()
        {
            AllowedUpdates = new[]
                { UpdateType.Message, UpdateType.CallbackQuery }
        };
    }
}