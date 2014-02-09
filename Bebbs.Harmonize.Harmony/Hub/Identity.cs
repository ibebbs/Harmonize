
namespace Bebbs.Harmonize.Harmony.Hub
{
    internal class Identity : With.Component.IIdentity
    {
        public Identity(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }

        public override string ToString()
        {
            return Id;
        }
    }
}
