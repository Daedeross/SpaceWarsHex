using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    /// <summary>
    /// Represents the view model for a document, providing properties and functionality to manage and display
    /// document-related data.
    /// </summary>
    /// <remarks>This interface extends <see cref="IViewModel"/> and includes a property for the document's
    /// name. It is intended to be implemented by classes that manage the state and behavior of document
    /// views.</remarks>
    public interface IDocumentViewModel : IViewModel
    {
        public string Name { get; set; }
    }
}
