using Bebbs.Harmonize.Harmony.Messages;

namespace Bebbs.Harmonize.Harmony.Command
{
    public interface IFactory
    {
        IHarmonyCommandMessage ConstructHarmonyCommand(ISession session, Harmonize.With.Command.ICommand command);
    }

    internal class Factory : IFactory
    {
        private IHarmonyCommandMessage BuildHarmonyCommand(ISession session, Harmonize.With.Command.PowerOn command)
        {
            return new HarmonyCommandMessage(session, command.DeviceId, "PowerOn");
        }

        private IHarmonyCommandMessage BuildHarmonyCommand(ISession session, Harmonize.With.Command.PowerOff command)
        {
            return new HarmonyCommandMessage(session, command.DeviceId, "PowerOff");
        }

        private IHarmonyCommandMessage BuildHarmonyCommand(ISession session, Harmonize.With.Command.ICommand command)
        {
            return new HarmonyCommandMessage(session, null, null);
        }

        public IHarmonyCommandMessage ConstructHarmonyCommand(ISession session, Harmonize.With.Command.ICommand command)
        {
            dynamic dynamicCommand = command;

            return BuildHarmonyCommand(session, dynamicCommand);
        }
    }
}
