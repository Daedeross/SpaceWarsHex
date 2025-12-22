using DynamicData;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Model;
using System.Collections.ObjectModel;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class ThresholdsViewModel : ViewModelBase
    {
        [Reactive]
        private ObservableCollection<ThresholdViewModel> _thresholds = [];

        public ThresholdsViewModel(IEnumerable<DamageThreshold> thresholds)
        {
            _thresholds = new ObservableCollection<ThresholdViewModel>(
                thresholds.Select(t => new ThresholdViewModel(t)));
        }

        public IEnumerable<DamageThreshold> GetThresholds()
        {
            return _thresholds.Select(t => t.GetDamageThreshold());
        }

        public void SetStandard()
        {
            _thresholds.Clear();
            _thresholds.AddRange([
                new ThresholdViewModel(1f, 1f),
                new ThresholdViewModel(0.75f, 0.875f),
                new ThresholdViewModel(0.5f, 0.75f),
                new ThresholdViewModel(0.25f, 0.5f)
                ]);
        }

        public void SetLinear(float hullEnd, float multEnd, int count)
        {
            if (count < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be at least 1.");
            }
            if (hullEnd > 1f || hullEnd < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(hullEnd), "Hull end must be between 0 and 1.");
            }
            if (multEnd > 1f || multEnd < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(multEnd), "Multiplier end must be between 0 and 1.");
            }

            var hullStep = (1f - hullEnd) / (count - 1);
            var multStep = (1f - multEnd) / (count - 1);
            var thresholds = new List<ThresholdViewModel>(count);

            for (int i = 0; i < count; i++)
            {
                thresholds.Add(new ThresholdViewModel(
                    1f - hullStep * i,  // HullStrength
                    1f - multStep * i   // SystemMultiplier
                ));
            }

            Thresholds = new ObservableCollection<ThresholdViewModel>(thresholds);
        }

        public static ThresholdsViewModel CreateLinear(float hullEnd, float multEnd, int count)
        {
            var vm = new ThresholdsViewModel([]);
            vm.SetLinear(hullEnd, multEnd, count);
            return vm;
        }

        public static ThresholdsViewModel CreateQuadratic(float hullEnd, float multEnd, int count)
        {
            if (count < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be at least 1.");
            }
            if (hullEnd > 1f || hullEnd < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(hullEnd), "Hull end must be between 0 and 1.");
            }
            if (multEnd > 1f || multEnd < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(multEnd), "Multiplier end must be between 0 and 1.");
            }
            var thresholds = new List<DamageThreshold>();
            for (int i = 0; i < count; i++)
            {
                var t = (float)i / (count - 1);
                thresholds.Add(new DamageThreshold
                {
                    HullStrength = 1f - (1f - hullEnd) * t * t,
                    SystemMultiplier = 1f - (1f - multEnd) * t * t
                });
            }
            return new ThresholdsViewModel(thresholds);
        }
    }
}
