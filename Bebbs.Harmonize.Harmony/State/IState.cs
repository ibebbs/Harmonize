
namespace Bebbs.Harmonize.Harmony.State
{
    public interface IState { }

    public interface IState<T> : IState where T : IContext
    {
        void OnEnter(T context);
        void OnExit(T context);
    }
}
