using ReactiveUI;
using SpaceWarsHex.Interfaces.Prototypes;
using SpaceWarsHex.Model;
using SpaceWarsHex.Prototypes;
using System;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public class TransporterBombViewModel : OrdinanceViewModel, IViewModel<ITransporterBombPrototype>
    {
        private ITransporterBombPrototype? _saved;

        private int _detonationDelay;
        private TurnPhase _detonationPhase;
        private int _revealDelay;
        private TurnPhase _revealPhase;

        public TransporterBombViewModel()
            : this(new TransporterBombPrototype())
        { }

        public TransporterBombViewModel(TransporterBombPrototype prototype)
            : base(prototype)
        {
            _saved = prototype ?? throw new ArgumentNullException(nameof(prototype));
            LoadFrom(_saved);
        }

        public override void LoadFrom(ISystemPrototype prototype)
        {
            base.LoadFrom(prototype);

            if (prototype is ITransporterBombPrototype tb)
            {
                _saved = tb;
                DetonationDelay = tb.DetonationDelay;
                DetonationPhase = tb.DetonationPhase;
                RevealDelay = tb.RevealDelay;
                RevealPhase = tb.RevealPhase;
            }
        }

        public override void SaveTo(ISystemPrototype prototype)
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

        public int DetonationDelay
        {
            get => _detonationDelay;
            set => this.RaiseAndSetIfChanged(ref _detonationDelay, value);
        }

        public TurnPhase DetonationPhase
        {
            get => _detonationPhase;
            set => this.RaiseAndSetIfChanged(ref _detonationPhase, value);
        }

        public int RevealDelay
        {
            get => _revealDelay;
            set => this.RaiseAndSetIfChanged(ref _revealDelay, value);
        }

        public TurnPhase RevealPhase
        {
            get => _revealPhase;
            set => this.RaiseAndSetIfChanged(ref _revealPhase, value);
        }
    }
}