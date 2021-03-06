﻿
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Observe : Message, With.Message.IObserve
    {
        With.Component.IIdentity IObserve.Entity
        {
            get { return Entity; }
        }

        With.Component.IIdentity IObserve.Observable
        {
            get { return Observable; }
        }

        With.Component.IIdentity IObserve.Observer
        {
            get { return Observer; }
        }

        public Identity Entity { get; set; }
        public Identity Observable { get; set; }
        public Identity Observer { get; set; }
    }
}
