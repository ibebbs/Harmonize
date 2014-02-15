
namespace Bebbs.Harmonize.With.Messaging.Mapping
{
    internal class EntityDescription : Description, Component.IEntityDescription
    {
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
    }
}
