using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Model;
using SpaceWarsHex.Prototypes;
using System;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class OrdinanceViewModel : SystemViewModel<OrdinancePrototype>
    {
        [Reactive]
        private FireMode _fireMode;
        [Reactive]
        private TurnPhase _firePhase;
        [Reactive]
        private int _maxRange;
        [Reactive]
        private int _strength;
        [Reactive]
        private int _maxUses;

        public OrdinanceViewModel()
            : this(new OrdinancePrototype())
        { }

        public OrdinanceViewModel(OrdinancePrototype prototype)
            : base(prototype)
        { }

        public override void LoadFrom(OrdinancePrototype prototype)
        {
            base.LoadFrom(prototype);

            FireMode = prototype.FireMode;
            FirePhase = prototype.FirePhase;
            MaxRange = prototype.MaxRange;
            Strength = prototype.Strength;
            MaxUses = prototype.MaxUses;
        }

        public override void SaveTo(OrdinancePrototype prototype)
        {
            base.SaveTo(prototype);

            prototype.FireMode = FireMode;
            prototype.FirePhase = FirePhase;
            prototype.MaxRange = MaxRange;
            prototype.Strength = Strength;
            prototype.MaxUses = MaxUses;
        }

        public virtual OrdinancePrototype ToPrototype()
        {
            var proto = new OrdinancePrototype();
            SaveTo(proto);

            return proto;
        }
    }
}