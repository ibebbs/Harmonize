
using System.Threading.Tasks;
namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service
{
    public static class HarmonizeDispatcher
    {
        private static void Process(dynamic client, Common.Dto.Identity registrar, Common.Dto.Identity entity, Common.Dto.Observation message)
        {
            client.Observation(registrar, entity, message);
        }

        public static void Dispatch(dynamic client, Common.Dto.Identity registrar, Common.Dto.Identity entity, Common.Dto.Message message)
        {
            dynamic dynamicMessage = message;

            Process(client, registrar, entity, dynamicMessage);
        }
    }
}
