
namespace Bebbs.Harmonize.Harmony.Hub
{
    internal class EntityDescription : With.Component.IEntityDescription
    {
        public EntityDescription(string name, string type, string manufacturer, string model, string remarks)
        {
            Name = name;
            Type = type;
            Manufacturer = manufacturer;
            Model = model;
            Remarks = remarks;
        }

        public string Name { get; private set; }

        public string Type { get; private set; }

        public string Manufacturer { get; private set; }

        public string Model { get; private set; }

        public string Remarks { get; private set; }
    }
}
