
namespace Bebbs.Harmonize.With.Owl.Intuition.Command.Response
{
    public class Save : IResponse
    {
        public Save(Status status)
        {
            Status = status;
        }

        public Status Status { get; private set; }
    }
}
