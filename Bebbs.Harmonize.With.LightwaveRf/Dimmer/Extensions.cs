using Bebbs.Harmonize.With.Component;
using System;

namespace Bebbs.Harmonize.With.LightwaveRf.Dimmer
{
    public static class Extensions
    {
        public static string ToDescription(this Configuration.DimmerType dimmerType)
        {
            switch (dimmerType)
            {
                case Configuration.DimmerType.OneGang: return Constants.OneGang;
                case Configuration.DimmerType.TwoGang: return Constants.TwoGang;
                case Configuration.DimmerType.ThreeGang: return Constants.ThreeGang;
                case Configuration.DimmerType.FourGang: return Constants.FourGang;
                default: throw new ArgumentException(string.Format("Unknown dimmer type: '{0}'", dimmerType), "dimmerType");
            }
        }

        public static IIdentity ToEntityIdentity(this Configuration.Dimmer dimmer)
        {
            return new Identity(string.Format("R{0}-D{1}-DIMMER-{2}", dimmer.RoomNumber, dimmer.DeviceNumber, dimmer.Name));
        }

        public static IEntityDescription ToEntityDescription(this Configuration.Dimmer dimmer)
        {
            return new EntityDescription { Name = dimmer.Name, Remarks = dimmer.Description, Manufacturer = LightwaveRf.Constants.Manufacturer, Model = Constants.Model, Type = dimmer.DimmerType.ToDescription() };
        }

        public static IIdentity ToLevelIdentity(this Configuration.Dimmer dimmer)
        {
            return new Identity(string.Format("R{0}-D{1}-DIMMER-{2}:LEVEL", dimmer.RoomNumber, dimmer.DeviceNumber, dimmer.Name));
        }

        public static IDescription ToLevelDescription(this Configuration.Dimmer dimmer)
        {
            return new Description { Name = "Level", Remarks = "Sets the lighting level of the dimmer" };
        }

        public static IIdentity ToPercentIdentity(this Configuration.Dimmer dimmer)
        {
            return new Identity(string.Format("R{0}-D{1}-DIMMER-{2}:LEVEL:PERCENT", dimmer.RoomNumber, dimmer.DeviceNumber, dimmer.Name));
        }

        public static IParameterDescription ToPercentDescription(this Configuration.Dimmer dimmer)
        {
            return new ParameterDescription { Name = "Percent", Remarks = "The percentage of maximum intensity", Required = true, Measurement = MeasurementType.Percent, Minimum = new Measurement(MeasurementType.Percent, "0"), Default = new Measurement(MeasurementType.Percent, "0"), Maximum = new Measurement(MeasurementType.Percent, "100") };
        }

        public static IIdentity ToOnIdentity(this Configuration.Dimmer dimmer)
        {
            return new Identity(string.Format("R{0}-D{1}-DIMMER-{2}:ON", dimmer.RoomNumber, dimmer.DeviceNumber, dimmer.Name));
        }

        public static IDescription ToOnDescription(this Configuration.Dimmer dimmer)
        {
            return new Description { Name = "On", Remarks = "Sets the lighting level to full intensity" };
        }

        public static IIdentity ToOffIdentity(this Configuration.Dimmer dimmer)
        {
            return new Identity(string.Format("R{0}-D{1}-DIMMER-{2}:OFF", dimmer.RoomNumber, dimmer.DeviceNumber, dimmer.Name));
        }

        public static IDescription ToOffDescription(this Configuration.Dimmer dimmer)
        {
            return new Description { Name = "On", Remarks = "Sets the lighting level to zero intensity" };
        }
    }
}
