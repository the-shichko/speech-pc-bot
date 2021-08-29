namespace speech_pc_bot.Api
{
    public class ResponseResult<T>
    {
        public ResponseResult(bool isSucceed)
        {
            IsSucceed = isSucceed;
        }
        public ResponseResult(bool isSucceed, T data) : this(isSucceed)
        {
            Result = data;
        }

        public bool IsSucceed { get; }
        public T Result { get; }
        public string Error { get; }
    }
}