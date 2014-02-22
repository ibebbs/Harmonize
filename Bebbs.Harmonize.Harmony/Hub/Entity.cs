using System;
using System.Collections.Generic;
using System.Linq;

namespace Bebbs.Harmonize.With.Harmony.Hub
{
    public interface IEntity : With.Component.IEntity
    {
        int Id { get; }
        string Icon { get; }
        bool IsManualPower { get; }
        string DeviceProfileUri { get; }
        string DeviceTypeDisplayName { get; }
        int Transport { get; }
        int ControlPort { get; }
        string SuggestedDisplay { get; }
        IEnumerable<IControl> Controls { get; }
        
        string HubName { get; set; }
    }

    internal class Entity : IEntity
    {
        private readonly Lazy<With.Component.IIdentity> _identity;
        private readonly Lazy<With.Component.IEntityDescription> _description;

        public Entity()
        {
            _identity = new Lazy<With.Component.IIdentity>(() => new Identity(id.ToString()));
            _description = new Lazy<With.Component.IEntityDescription>(() => new EntityDescription(label, type, manufacturer, model, deviceProfileUri));
        }

        With.Component.IIdentity With.Component.IEntity.Identity 
        {
            get { return _identity.Value; }
        }

        With.Component.IEntityDescription With.Component.IEntity.Description
        {
            get { return _description.Value; }
        }

        IEnumerable<With.Component.IObservable> With.Component.IEntity.Observables
        {
            get { return Enumerable.Empty<With.Component.IObservable>(); }
        }

        IEnumerable<With.Component.IActionable> With.Component.IEntity.Actionables
        {
            get { return controlGroup.SelectMany(group => group.function).ToArray(); }
        }

        string IEntity.Icon
        {
            get { return icon; }
        }

        int IEntity.Id
        {
            get { return id; }
        }

        bool IEntity.IsManualPower
        {
            get { return isManualPower; }
        }

        string IEntity.DeviceProfileUri
        {
            get { return deviceProfileUri; }
        }

        string IEntity.DeviceTypeDisplayName
        {
            get { return deviceTypeDisplayName; }
        }

        int IEntity.Transport
        {
            get { return transport; }
        }

        int IEntity.ControlPort
        {
            get { return controlPort; }
        }

        string IEntity.SuggestedDisplay
        {
            get { return suggestedDisplay; }
        }

        IEnumerable<IControl> IEntity.Controls 
        {
            get { return controlGroup; }
        }

        public int id { get; set; }
        public string type { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public string label { get; set; }
        public string icon { get; set; }
        public bool isManualPower { get; set; }
        public string deviceProfileUri { get; set; }
        public string deviceTypeDisplayName { get; set; }
        public int transport { get; set; }
        public int controlPort { get; set; }
        public string suggestedDisplay { get; set; }
        public Control[] controlGroup { get; set; }

        public string HubName { get; set; }
    }
}
