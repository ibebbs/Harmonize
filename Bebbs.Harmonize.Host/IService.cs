using System.Threading.Tasks;

namespace Bebbs.Harmonize.Host
{
    public interface IService
    {
        Task Initialize();
        Task Start();
        Task Stop();
        Task Cleanup();
    }
}
