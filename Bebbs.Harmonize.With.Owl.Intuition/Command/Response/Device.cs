﻿using System;

namespace Bebbs.Harmonize.With.Owl.Intuition.Command.Response
{
    public class Device : IResponse
    {
        public Device(Status status, int index, string deviceAddress, string deviceType, string deviceState, Values.SignalStrength signalStrength, Values.LinkQuality linkQuality, Values.BatteryState batteryState, TimeSpan timeSinceLastPacketReceived, int receivedPackets, int sentPackets)
        {
            Status = status;
            Index = index;
            DeviceAddress = deviceAddress;
            DeviceType = deviceType;
            DeviceState = deviceState;
            SignalStrength = signalStrength;
            LinkQuality = linkQuality;
            BatteryState = batteryState;
            TimeSinceLastPacketReceived = timeSinceLastPacketReceived;
            ReceivedPackets = receivedPackets;
            SentPackets = sentPackets;
        }

        public Status Status { get; private set; }
        public int Index { get; private set; }
        public string DeviceAddress { get; private set; }
        public string DeviceType { get; private set; }
        public string DeviceState { get; private set; }
        public Values.SignalStrength SignalStrength { get; private set; }
        public Values.LinkQuality LinkQuality { get; private set; }
        public Values.BatteryState BatteryState { get; private set; }
        public TimeSpan TimeSinceLastPacketReceived { get; private set; }
        public int ReceivedPackets { get; private set; }
        public int SentPackets { get; private set; }
    }
}
