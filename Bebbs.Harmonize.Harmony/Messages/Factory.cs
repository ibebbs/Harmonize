namespace Bebbs.Harmonize.With.Harmony.Messages
{
    public interface IFactory
    {
        IHarmonyCommandMessage ConstructHarmonyCommand(ISession session, Harmonize.With.Command.ICommand command);
    }

    internal class Factory : IFactory
    {
        public IHarmonyCommandMessage ConstructHarmonyCommand(ISession session, Harmonize.With.Command.ICommand command)
        {
            Hub.Command hubCommand = (command as Hub.Command);

            if (hubCommand != null)
            {
                return new HarmonyCommandMessage(session, hubCommand.deviceId, hubCommand.command);
            }
            else
            {
                return null;
            }
        }
    }
}
