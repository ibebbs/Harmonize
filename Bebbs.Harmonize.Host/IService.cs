using System.Threading.Tasks;

namespace Bebbs.Harmonize.Host
{
    public interface IService
    {
        void Initialize();
        Task Start();
        Task Stop();
        void Cleanup();
    }
}
