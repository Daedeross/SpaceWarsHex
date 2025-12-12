using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Model;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class ThresholdViewModel : ViewModelBase
    {
        [Reactive]
        private float _hullStrength;

        [Reactive]
        private float _systemMultiplier;

        public ThresholdViewModel(float hullStrength, float systemMultiplier)
        {
            _hullStrength = hullStrength;
            _systemMultiplier = systemMultiplier;
        }

        public ThresholdViewModel(DamageThreshold threshold)
        {
            _hullStrength = threshold.HullStrength;
            _systemMultiplier = threshold.SystemMultiplier;
        }

        public DamageThreshold GetDamageThreshold()
        {
            return new DamageThreshold
            {
                HullStrength = _hullStrength,
                SystemMultiplier = _systemMultiplier
            };
        }
    }
}
