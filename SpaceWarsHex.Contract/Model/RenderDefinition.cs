using System;
using System.Runtime.Serialization;

namespace SpaceWarsHex.Model
{
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
