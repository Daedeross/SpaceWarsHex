using SpaceWars.Interfaces.Rules;
using System;
using System.Runtime.Serialization;

namespace SpaceWars.Rules
{
    /// <inheritdoc />
    [Serializable]
    [DataContract]
    public class BoardRules : IRules
    {
        [DataMember(Name = "PhasorTable")]
        private PhasorTable _phasorTable = new ();

        [DataMember(Name = "ConcussionTable")]
        private ConcussionTable _concussionTable = new ();

        /// <inheritdoc />
        [IgnoreDataMember]
        public IPhasorTable PhasorTable => _phasorTable;

        /// <inheritdoc />
        [IgnoreDataMember]
        public IConcussionTable ConcussionTable => _concussionTable;
    }
}
