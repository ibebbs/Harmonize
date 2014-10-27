
namespace Bebbs.Harmonize.With.Component
{
    public interface IEntityDescription : IDescription
    {
        string Type { get; }
        string Manufacturer { get; }
        string Model { get; }
    }

    public class EntityDescription : Description, IEntityDescription
    {
        public string Type { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }
    }
}
