using Bebbs.Harmonize.With.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bebbs.Harmonize.With.Owl.Intuition.Command.Response.Builder
{
    internal class DeviceResponse : IBuilder
    {
        private const string StatusGroup = "Status";
        private const string IndexGroup = "Index";
        private const string DeviceAddressGroup = "DeviceAddress";
        private const string DeviceTypeGroup = "DeviceType";
        private const string SecondsSinceLastPacketReceivedGroup = "SecondsSinceLastPacketReceived";
        private const string DeviceStateGroup = "DeviceState";
        private const string SignalStrengthGroup = "SignalStrength";
        private const string LinkQualityGroup = "LinkQuality";
        private const string BatteryStateGroup = "BatteryState";
        private const string ReceivedPacketsGroup = "ReceivedPackets";
        private const string SentPacketsGroup = "SentPackets";
        private const string Unknown1Group = "U1";
        private const string Unknown2Group = "U2";
        private const string Unknown3Group = "U3";
        private const string Unknown4Group = "U4";
        private const string Unknown5Group = "U5";
        private const string Unknown6Group = "U6";
        private const string DeviceResponseName = "DeviceResponse";
        private const string DeviceResponsePattern = @"(?<Status>OK|ERROR),DEVICE,(?<Index>\d+),(?<DeviceAddress>.{2}),(?<DeviceType>[^,]+),(?<SecondsSinceLastPacketReceived>\d*),(?<DeviceState>[^,]+),(?<SignalStrength>-?\d+),(?<LinkQuality>\d+),(?<BatteryState>[^,]+),(?<ReceivedPackets>\d+),(?<SentPackets>\d+),(?<U1>\d+.\d+:\d+),(?<U2>\d+.\d+),(?<U3>\d+),(?<U4>\d+),(?<U5>\d+),(?<U6>\d+.\d+)";

        public IResponse Build(Match match)
        {
            Status status = match.ReadGroupAs<Status>(StatusGroup, value => (Status)Enum.Parse(typeof(Status), value, true));
            int index = match.ReadGroupAs<int>(IndexGroup, Int32.Parse);
            string deviceAddress = match.ReadGroupValue(DeviceAddressGroup);
            string deviceType = match.ReadGroupValue(DeviceTypeGroup);
            int secondsSinceListPacketReceived = match.ReadGroupAs<int>(SecondsSinceLastPacketReceivedGroup, Int32.Parse);
            string deviceState = match.ReadGroupValue(DeviceStateGroup);
            Values.SignalStrength signalStrength = match.ReadGroupAs<Values.SignalStrength>(SignalStrengthGroup, Values.SignalStrength.Parse);
            Values.LinkQuality linkQuality = match.ReadGroupAs<Values.LinkQuality>(LinkQualityGroup, Values.LinkQuality.Parse);
            Values.BatteryState batteryState = match.ReadGroupAs<Values.BatteryState>(BatteryStateGroup, Values.BatteryState.Parse);
            int receivedPackets = match.ReadGroupAs<int>(ReceivedPacketsGroup, Int32.Parse);
            int sentPackets = match.ReadGroupAs<int>(SentPacketsGroup, Int32.Parse);
            string unknown1 = match.ReadGroupValue(Unknown1Group);
            string unknown2 = match.ReadGroupValue(Unknown2Group);
            string unknown3 = match.ReadGroupValue(Unknown3Group);
            string unknown4 = match.ReadGroupValue(Unknown4Group);
            string unknown5 = match.ReadGroupValue(Unknown5Group);
            string unknown6 = match.ReadGroupValue(Unknown6Group);

            return new Command.Response.Device(status, index, deviceAddress, deviceType, deviceState, signalStrength, linkQuality, batteryState, TimeSpan.FromSeconds(secondsSinceListPacketReceived), receivedPackets, sentPackets, unknown1, unknown2, unknown3, unknown4, unknown5, unknown6);
        }

        public string Name
        {
            get { return DeviceResponseName; }
        }

        public string Regex
        {
            get { return DeviceResponsePattern; }
        }
    }
}
