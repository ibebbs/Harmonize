using System.Reactive.Concurrency;

namespace Bebbs.Harmonize.With
{
    public interface IAsyncHelper
    {
        IScheduler SyncScheduler { get; }
        IScheduler AsyncScheduler { get; }
    }

    public class AsyncHelper : IAsyncHelper
    {
        public IScheduler SyncScheduler 
        {
            get { return Scheduler.Immediate; }
        }

        public IScheduler AsyncScheduler 
        {
            get { return Scheduler.Default; }
        }
    }
}
