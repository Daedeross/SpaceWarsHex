using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Model;

namespace SpaceWarsHex.Helpers
{
    public static class HexObjectExtensions
    {
        public static void ApplyDamage(this IDamageable entity, DamageInstance damage, bool saveShields = false)
        {
            entity.ApplyDamage(damage.DamageKind, damage.DamageValue, saveShields);
        }
    }
}
