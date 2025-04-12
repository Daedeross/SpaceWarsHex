using SpaceWarsHex.States;
using System.Diagnostics.CodeAnalysis;

namespace SpaceWarsHex.Serialization.Tests
{
    public class RuntimeEqualityComparer : IEqualityComparer<StateBase>
    {
        public static readonly RuntimeEqualityComparer Instance = new();

        public bool Equals(StateBase? x, StateBase? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            var type = x.GetType();
            if (type != y.GetType()) return false;

            var properties = type.GetProperties();
            var fields = type.GetFields();

            foreach (var prop in properties)
            {
                if (prop.PropertyType.IsAssignableTo(typeof(StateBase)))
                {
                    if (!Equals((StateBase?)prop.GetValue(x), (StateBase?)prop.GetValue(y)))
                    {
                        return false;
                    }
                }
                else if (prop.PropertyType.IsAssignableTo(typeof(IEnumerable<StateBase>)))
                {
                    var l1 = (prop.GetValue(x) as IEnumerable<StateBase>);
                    var l2 = (prop.GetValue(y) as IEnumerable<StateBase>);
                    if(!ListsEquals(l1, l2)) return false;
                }
                else
                {
                    if (!Equals(prop.GetValue(x), prop.GetValue(y)))
                    {
                        return false;
                    }
                }
            }

            foreach (var field in fields)
            {
                if (field.FieldType.IsAssignableTo(typeof(StateBase)))
                {
                    if (!Equals((StateBase?)field.GetValue(x), (StateBase?)field.GetValue(y))) return false;
                }
                else
                {
                    if (!Equals(field.GetValue(x), field.GetValue(y)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool ListsEquals(IEnumerable<StateBase>? x, IEnumerable<StateBase>? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            var type = x.GetType();
            if (type != y.GetType()) return false;

            var x_iter = x.GetEnumerator();
            var y_iter = y.GetEnumerator();

            while (x_iter.MoveNext())
            {
                if (!y_iter.MoveNext()) // y is shorter than x
                {
                    return false;
                }
                if (!Equals(x_iter.Current, y_iter.Current))
                {
                    return false;
                }
            }

            return !y_iter.MoveNext(); // check if y is longer than x
        }

        public int GetHashCode([DisallowNull] StateBase obj)
        {
            return obj.Hash;
        }
    }
}
