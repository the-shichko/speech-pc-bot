using System.Threading.Tasks;

namespace speech_pc_bot.Services
{
    public interface IBotExecute<in T>
    {
        Task Execute(T modelTelegram);
    }
}