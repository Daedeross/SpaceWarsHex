using System;

namespace SpaceWars.Helpers
{
    /// <summary>
    /// Static helepr class for dice rolling
    /// </summary>
    public static class Dice
    {
        static Dice()
        {
            _rand = new Random();
        }

        private static Random _rand;

        /// <summary>
        /// Resets the random table with a specific seed.
        /// </summary>
        /// <param name="seed">The seed to use.</param>
        public static void Seed(int seed)
        {
            _rand = new Random(seed);
        }

        /// <summary>
        /// Roll a d10
        /// </summary>
        public static int D10 => RollD10();

        private static int RollD10()
        {
            return _rand.Next(1, 11);
        }

        /// <summary>
        /// Roll one or more d10s and get the sum.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int Nd10(int n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException("n", "n must be non-negative.");
            }
            int result = 0;
            for (int i = 0; i < n; i++)
            {
                result += RollD10();
            }
            return result;
        }
    }
}
