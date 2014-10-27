using EventSourceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.LightwaveRf.WifiLink
{
    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-LightwaveRf-WifiLink-QueryEndpoint")]
    public interface IQueryEndpoint
    {
        Task Connect();

        Task Disconnect();
    }

    internal class QueryEndpoint : IQueryEndpoint
    {
        private readonly Configuration.ISettings _settings;
        private readonly InstructionNumber _instructionNumber;

        private readonly IPAddress _ipAddress;

        private UdpClient _socket;

        public QueryEndpoint(Configuration.IProvider configurationProvider)
        {
            _settings = configurationProvider.GetSettings();
            _instructionNumber = new InstructionNumber();

            _ipAddress = string.IsNullOrWhiteSpace(_settings.LocalIpAddress)
              ? Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)
              : IPAddress.Parse(_settings.LocalIpAddress);
        }

        private string BuildCommandString(string instructionNumber, byte roomId, byte deviceId, char function, string parameter, string displayLine1, string displayLine2)
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

        private Task SendCommand(byte roomId, byte deviceId, char function, string parameter, string displayLine1, string displayLine2)
        {
            string instructionNumber = _instructionNumber.Next();

            string command = BuildCommandString(instructionNumber, roomId, deviceId, function, parameter, displayLine1, displayLine2);

            byte[] packet = Encoding.ASCII.GetBytes(command);

            return _socket.SendAsync(packet, packet.Length);
        }

        private Task Pair()
        {
            return SendCommand(0, 0, '0', null, "Pairing with", _ipAddress.ToString());
        }

        public Task Connect()
        {
            _socket = new UdpClient();
            _socket.Client.Bind(new IPEndPoint(_ipAddress, _settings.QueryPort));

            return TaskEx.Done;
        }

        public Task Disconnect()
        {
            if (_socket != null)
            {
                _socket.Close();
                _socket = null;
            }

            return TaskEx.Done;
        }

        public Task TurnDeviceOn(byte roomId, byte deviceId, string displayLine1, string displayLine2)
        {
            return SendCommand(roomId, deviceId, '1', null, displayLine1, displayLine2);
        }

        public Task TurnDeviceOff(byte roomId, byte deviceId, string displayLine1, string displayLine2)
        {
            return SendCommand(roomId, deviceId, '0', null, displayLine1, displayLine2);
        }

        public Task SetDimmerLevel(byte roomId, byte deviceId, byte level, string displayLine1, string displayLine2)
        {
            return SendCommand(roomId, deviceId, 'p', level.ToString("00"), displayLine1, displayLine2);
        }
    }
}
