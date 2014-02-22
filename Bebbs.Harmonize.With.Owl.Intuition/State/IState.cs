
namespace Bebbs.Harmonize.With.Owl.Intuition.State
{
    public interface IState
    {
        void OnEnter();

        void OnExit();

        Name Name { get; }
    }
}
