using System.Threading.Tasks;
using speech_pc_bot.Api;
using speech_pc_bot.Speech.Models;

namespace speech_pc_bot.Speech
{
    public class YandexSpeech
    {
        private readonly YandexSpeechApiRequest<SpeechResult> _yandexSpeechApi;
        public YandexSpeech()
        {
            _yandexSpeechApi = new YandexSpeechApiRequest<SpeechResult>();
        }

        public async Task<string> SendVoice(byte[] voice)
        {
            var result = await _yandexSpeechApi.SpeechPost(voice);
            return result.IsSucceed ? result.Result.Result : result.Error;
        }
    }
}