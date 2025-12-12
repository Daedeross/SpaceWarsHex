using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceWarsHex
{
    public static class Extensions
    {
        public static T GetOrThrow<T>(this T? obj) where T : class
        {
            return obj ?? throw new ArgumentNullException(nameof(obj));
        }
    }
}
