using SpaceWars.Interfaces;
using System;

namespace SpaceWars.Model
{
    public class EntityCreatedEventArgs : EventArgs
    {
        public IHexObject Entity { get; }

        public EntityCreatedEventArgs(IHexObject entity)
        {
            Entity = entity;
        }
    }

    public delegate void EntityCreatedEventHandler(object sender, EntityCreatedEventArgs e);
}
