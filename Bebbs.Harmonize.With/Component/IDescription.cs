
namespace Bebbs.Harmonize.With.Component
{
    public interface IDescription
    {
        string Name { get; }
        string Remarks { get; }
    }

    public class Description : IDescription
    {
        public string Name { get; set; }

        public string Remarks { get; set; }
    }
}
