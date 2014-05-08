
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class EntityDescription : Description, With.Component.IEntityDescription
    {
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
    }
}
