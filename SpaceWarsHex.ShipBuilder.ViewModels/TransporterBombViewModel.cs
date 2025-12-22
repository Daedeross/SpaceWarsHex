using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Model;
using SpaceWarsHex.Prototypes;
using System;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class TransporterBombViewModel : OrdinanceViewModel, IViewModel<TransporterBombPrototype>
    {
        [Reactive]
        private int _detonationDelay;
        [Reactive]
        private TurnPhase _detonationPhase;
        [Reactive]
        private int _revealDelay;
        [Reactive]
        private TurnPhase _revealPhase;

        public TransporterBombViewModel()
            : this(new TransporterBombPrototype())
        { }

        public TransporterBombViewModel(TransporterBombPrototype prototype)
            : base(prototype)
        { }

        public override void LoadFrom(OrdinancePrototype prototype)
        {
            base.LoadFrom(prototype);

            if (prototype is TransporterBombPrototype tb)
            {
                _saved = tb;
                DetonationDelay = tb.DetonationDelay;
                DetonationPhase = tb.DetonationPhase;
                RevealDelay = tb.RevealDelay;
                RevealPhase = tb.RevealPhase;
            }
        }

        public override void SaveTo(OrdinancePrototype prototype)
        {
            base.SaveTo(prototype);

            if (prototype is TransporterBombPrototype tb)
            {
                tb.DetonationDelay = DetonationDelay;
                tb.DetonationPhase = DetonationPhase;
                tb.RevealDelay = RevealDelay;
                tb.RevealPhase = RevealPhase;
            }
            else if (prototype is TransporterBombPrototype tbIface)
            {
                // best effort for non-concrete runtime types
                tbIface.DetonationDelay = DetonationDelay;
                tbIface.DetonationPhase = DetonationPhase;
                tbIface.RevealDelay = RevealDelay;
                tbIface.RevealPhase = RevealPhase;
            }
        }

        TransporterBombPrototype IViewModel<TransporterBombPrototype>.GetLast()
        {
            return (TransporterBombPrototype)_saved;
        }

        TransporterBombPrototype IViewModel<TransporterBombPrototype>.Commit()
        {
            return (TransporterBombPrototype)base.Commit();
        }
    }
}