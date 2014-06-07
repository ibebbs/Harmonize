using System.Collections.ObjectModel;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service.Registration
{
    public class Collection : KeyedCollection<string, IInstance>
    {
        protected override string GetKeyForItem(IInstance item)
        {
            return item.Key;
        }

        public bool TryGetValue(string key, out IInstance registration)
        {
            if (Contains(key))
            {
                registration = this[key];
                return true;
            }
            else
            {
                registration = null;
                return false;
            }
        }
    }
}
