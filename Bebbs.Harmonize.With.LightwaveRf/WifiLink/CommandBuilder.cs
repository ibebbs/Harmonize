using EventSourceProxy;
using System.Text;

namespace Bebbs.Harmonize.With.LightwaveRf.WifiLink
{
    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-LightwaveRf-WifiLink-CommandBuilder")]
    public interface ICommandBuilder
    {
        string BuildCommandString(string instructionNumber, byte roomId, byte deviceId, char function, string parameter, string displayLine1, string displayLine2);
    }

    internal class CommandBuilder : ICommandBuilder
    {
        public string BuildCommandString(string instructionNumber, byte roomId, byte deviceId, char function, string parameter, string displayLine1, string displayLine2)
        {
            StringBuilder commandBuilder = new StringBuilder();
            commandBuilder.AppendFormat("{0},!R{1}D{2}F{3}", instructionNumber, roomId, deviceId, function);

            if (!string.IsNullOrWhiteSpace(parameter))
            {
                commandBuilder.AppendFormat("P{0}", parameter);
            }

            commandBuilder.AppendFormat("|{0}|{1}", displayLine1, displayLine2);

            return commandBuilder.ToString();
        }
    }
}
