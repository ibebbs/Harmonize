
namespace Bebbs.Harmonize.With.Component
{
    public interface IEntityDescription : IDescription
    {
        string Type { get; }
        string Manufacturer { get; }
        string Model { get; }
    }
}
