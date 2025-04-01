using SpaceWars.Interfaces;
using SpaceWars.Model;

namespace SpaceWars.Helpers
{
    public static class HexObjectExtensions
    {
        public static void ApplyDamage(this IDamageable entity, DamageInstance damage, bool saveShields = false)
        {
            entity.ApplyDamage(damage.DamageKind, damage.DamageValue, saveShields);
        }
    }
}
