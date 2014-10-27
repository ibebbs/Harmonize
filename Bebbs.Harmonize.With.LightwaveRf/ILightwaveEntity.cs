using Bebbs.Harmonize.With.Component;
using System;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.LightwaveRf
{
    public interface ILightwaveEntity : IEntity, IObserver<With.Message.IMessage> 
    {
        Task Initialize();

        Task CleanUp();
    }
}
