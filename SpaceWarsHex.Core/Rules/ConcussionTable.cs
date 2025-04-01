using SpaceWars.Interfaces.Rules;
using System;

namespace SpaceWars.Rules
{
    /// <summary>
    /// Class to implement <see cref="IConcussionTable"/>.
    /// TODO: Currenly hard-coding, replace with CSV import or similar.
    /// </summary>
    internal class ConcussionTable : IConcussionTable
    {
        /// <summary>
        /// Concussion table indexed [Shields, Damage].
        /// Value is final (hull) damage.
        /// </summary>
        private readonly int[,] _table = new[,] {
            /* 0  */{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 },
            /* 1  */{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 },
            /* 2  */{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 },
            /* 3  */{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 },
            /* 4  */{ 0, 1, 1, 2, 3, 4, 4, 5, 6, 7,  7,  8,  9, 10, 10, 11, 12 },
            /* 5  */{ 0, 1, 1, 2, 3, 4, 4, 5, 6, 7,  7,  8,  9, 10, 10, 11, 12 },
            /* 6  */{ 0, 0, 1, 1, 2, 3, 3, 4, 5, 6,  6,  7,  8,  9,  9, 10, 11 },
            /* 7  */{ 0, 0, 1, 1, 2, 3, 3, 4, 5, 6,  6,  7,  8,  9,  9, 10, 11 },
            /* 8  */{ 0, 0, 0, 1, 1, 2, 2, 3, 4, 5,  5,  6,  7,  8,  8,  9, 10 },
            /* 9  */{ 0, 0, 0, 1, 1, 2, 2, 3, 4, 5,  5,  6,  7,  8,  8,  9, 10 },
            /* 10 */{ 0, 0, 0, 0, 1, 1, 1, 2, 3, 4,  4,  5,  6,  7,  7,  8,  9 },
            /* 11 */{ 0, 0, 0, 0, 1, 1, 1, 2, 3, 4,  4,  5,  6,  7,  7,  8,  9 },
            /* 12 */{ 0, 0, 0, 0, 0, 1, 1, 1, 2, 3,  3,  4,  5,  6,  6,  7,  8 },
            /* 13 */{ 0, 0, 0, 0, 0, 1, 1, 1, 2, 3,  3,  4,  5,  6,  6,  7,  8 },
            /* 14 */{ 0, 0, 0, 0, 0, 0, 1, 1, 1, 2,  2,  3,  4,  5,  5,  6,  7 },
            /* 15 */{ 0, 0, 0, 0, 0, 0, 1, 1, 1, 2,  2,  3,  4,  5,  5,  6,  7 },
            /* 16 */{ 0, 0, 0, 0, 0, 0, 0, 1, 1, 1,  1,  2,  3,  4,  4,  5,  6 },
            /* 17 */{ 0, 0, 0, 0, 0, 0, 0, 1, 1, 1,  1,  2,  3,  4,  4,  5,  6 },
            /* 18 */{ 0, 0, 0, 0, 0, 0, 0, 0, 1, 1,  1,  1,  2,  3,  3,  4,  5 },
            /* 19 */{ 0, 0, 0, 0, 0, 0, 0, 0, 1, 1,  1,  1,  2,  3,  3,  4,  5 },
            /* 20 */{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,  1,  1,  1,  2,  2,  3,  4 },
            };

        public int GetHullDamage(int damageValue, int currentShields)
        {
            if (damageValue < 0 || damageValue > 16)
            {
                throw new ArgumentOutOfRangeException(nameof(damageValue));
            }

            if (currentShields < 0 || currentShields > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(currentShields));
            }

            return _table[currentShields, damageValue];
        }
    }
}