using EventSourceProxy;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.LightwaveRf.WifiLink
{
    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-LightwaveRf-WifiLink-CommandEndpoint")]
    public interface ICommandEndpoint
    {
        Task Connect();

        Task Disconnect();

        Task TurnDeviceOn(byte roomId, byte deviceId, string displayLine1, string displayLine2);

        Task TurnDeviceOff(byte roomId, byte deviceId, string displayLine1, string displayLine2);

        Task SetDimmerLevel(byte roomId, byte deviceId, byte level, string displayLine1, string displayLine2);
    }

    internal class CommandEndpoint : ICommandEndpoint
    {
        private readonly ICommandBuilder _commandBuilder;
        private readonly Configuration.ISettings _settings;
        private readonly InstructionNumber _instructionNumber;

        private readonly IPAddress _ipAddress;

        private UdpClient _socket;

        public CommandEndpoint(ICommandBuilder commandBuilder, Configuration.IProvider configurationProvider)
        {
            _commandBuilder = commandBuilder;
            _settings = configurationProvider.GetSettings();
            _instructionNumber = new InstructionNumber();

            _ipAddress = string.IsNullOrWhiteSpace(_settings.LocalIpAddress)
              ? Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)
              : IPAddress.Parse(_settings.LocalIpAddress);
        }

        private Task SendCommand(byte roomId, byte deviceId, char function, string parameter, string displayLine1, string displayLine2)
        {
            string instructionNumber = _instructionNumber.Next();

            string command = _commandBuilder.BuildCommandString(instructionNumber, roomId, deviceId, function, parameter, displayLine1, displayLine2);

            byte[] packet = Encoding.ASCII.GetBytes(command);

            return _socket.SendAsync(packet, packet.Length);
        }

        private Task Pair()
        {
            return SendCommand(0, 0, '0', null, "Pairing with", _ipAddress.ToString());
        }

        private UdpClient BuildSocket(IPEndPoint endpoint)
        {
            UdpClient udpClient = new UdpClient();
            udpClient.Client.ExclusiveAddressUse = false;
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpClient.Client.Bind(endpoint);
            return udpClient;
        }

        public Task Connect()
        {
            _socket = BuildSocket(new IPEndPoint(_ipAddress, _settings.CommandPort));
            _socket.Connect(new IPEndPoint(IPAddress.Parse(_settings.WifiLinkIpAddress), _settings.CommandPort));

            return Pair();
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
