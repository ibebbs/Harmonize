namespace Bebbs.Harmonize.With.Harmony.Hub
{
    internal class Location : With.Component.ILocation
    {
        public Location(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
