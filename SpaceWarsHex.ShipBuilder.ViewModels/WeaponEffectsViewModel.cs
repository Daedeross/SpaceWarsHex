using DynamicData;
using ReactiveUI.SourceGenerators;
using SpaceWarsHex.Model;
using System.Collections.ObjectModel;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public partial class WeaponEffectsViewModel : ViewModelBase
    {
        [Reactive]
        private WeaponEffect? _selectedEffect;

        public ObservableCollection<WeaponEffect> Effects { get; private set; }

        public WeaponEffectsViewModel() : this([]) { }

        public WeaponEffectsViewModel(IEnumerable<WeaponEffect> effects)
        {
            Effects = new ObservableCollection<WeaponEffect>(effects);
        }

        public void SetEffects(IEnumerable<WeaponEffect> effects)
        {
            Effects.Clear();
            Effects.AddRange(effects);
        }

        public void ClearEffects() => Effects.Clear();

        public void AddEffect(WeaponEffect weaponEffect, int index = -1)
        {
            if (index >= 0 && index < Effects.Count)
            {
                Effects.Insert(index, weaponEffect);
            }
            else
            {
                Effects.Add(weaponEffect);
            }
        }

        public void RemoveEffect(WeaponEffect weaponEffect)
        {
            Effects.Remove(weaponEffect);
        }

        public List<WeaponEffect> ToList() => Effects.ToList();

        [ReactiveCommand]
        private void NewEffect()
        {
            Effects.Add(new WeaponEffect());
        }

        [ReactiveCommand(CanExecute = nameof(DeleteEffectCanExecute))]
        private void DeleteEffect()
        {
            Effects.Remove(_selectedEffect!);
        }

        private bool DeleteEffectCanExecute() => (_selectedEffect != null) && Effects.Contains(_selectedEffect);

        [ReactiveCommand]
        private void MoveEffectUp()
        {
            if (_selectedEffect != null)
            {
                var oldIndex = Effects.IndexOf(_selectedEffect);
                Effects.Move(oldIndex, Math.Max(oldIndex - 1, 0));
            }
        }

        [ReactiveCommand]
        private void MoveEffectDown()
        {
            if (_selectedEffect != null)
            {
                var oldIndex = Effects.IndexOf(_selectedEffect);
                Effects.Move(oldIndex, Math.Min(oldIndex + 1, Effects.Count - 1));
            }
        }
    }
}
