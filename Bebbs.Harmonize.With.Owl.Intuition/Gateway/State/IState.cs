
namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.State
{
    public interface IState
    {
        void OnEnter();

        void OnExit();

        Name Name { get; }
    }
}
