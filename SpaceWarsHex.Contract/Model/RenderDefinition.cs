using System;
using System.Runtime.Serialization;

namespace SpaceWarsHex.Model
{
    /// <summary>
    /// Represents the definition of a render operation, including its type and associated path.
    /// </summary>
    /// <remarks>This class is used to specify the kind of render operation to perform and the path associated
    /// with it.</remarks>
    [Serializable]
    [DataContract]
    public class RenderDefinition
    {
        [DataMember]
        public SingleRenderKind Kind { get; set; }

        [DataMember]
        public string Path { get; set; } = "";
    }
}
