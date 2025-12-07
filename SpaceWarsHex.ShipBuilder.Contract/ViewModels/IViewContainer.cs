using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public interface IViewContainer
    {
        Guid Id { get; }

        bool OwnsContent { get; }
        string Title { get; }

        IViewModel Content { get; }
    }
}
