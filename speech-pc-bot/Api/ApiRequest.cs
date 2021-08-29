using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace speech_pc_bot.Api
{
    public interface IApiRequest<TP>
    {
        Task<ResponseResult<TP>> Post(string jsonBody);
        Task<ResponseResult<TP>> Post(byte[] bytes, string queryParams);
    }

    public abstract class ApiRequest<TP> : IApiRequest<TP>
    {
        protected ApiRequest(string url, string apiToken)
        {
            Url = url;
            ApiToken = apiToken;
        }

        private string Url { get; }
        private string ApiToken { get; }

        public async Task<ResponseResult<TP>> Post(string jsonBody)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response =
                await httpClient.PostAsync(Url, new StringContent(jsonBody, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode) return new ResponseResult<TP>(false);

            var result = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            return new ResponseResult<TP>(true, JsonConvert.DeserializeObject<TP>(result));
        }

        public virtual async Task<ResponseResult<TP>> Post(byte[] bytes, string queryParams)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Api-Key", ApiToken);

            var response =
                await httpClient.PostAsync($"{Url}?{queryParams}", new ByteArrayContent(bytes));

            if (!response.IsSuccessStatusCode) return new ResponseResult<TP>(false);

            var result = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            return new ResponseResult<TP>(true, JsonConvert.DeserializeObject<TP>(result));
        }
    }

    public class YandexSpeechApiRequest<TP> : ApiRequest<TP>
    {
        private const string YandexApi = "https://stt.api.cloud.yandex.net";
        private const string YandexSpeechApi = YandexApi + "/speech/v1/stt:recognize";

        private const string ApiToken = "your-yandex-token";

        public YandexSpeechApiRequest() : base(YandexSpeechApi, ApiToken)
        {
        }

        public async Task<ResponseResult<TP>> SpeechPost(byte[] bytes, string topic = "general",
            bool profanityFilter = false,
            string format = "oggopus")
        {
            var queryParams = $"topic={topic}&profanityFilter={profanityFilter}&format={format}";
            return await Post(bytes, queryParams);
        }
    }
}