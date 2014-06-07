using System.Threading.Tasks;

namespace Bebbs.Harmonize.With
{
    public static class TaskEx
    {
        public static Task Done
        {
            get { return Task.FromResult<object>(null); }
        }
    }
}
