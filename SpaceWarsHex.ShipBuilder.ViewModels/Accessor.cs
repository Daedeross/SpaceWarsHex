using System.Linq.Expressions;
using System.Reflection;

namespace SpaceWarsHex.ShipBuilder.ViewModels
{
    public class Accessor
    {
        public required Type Type { get; init; }
        public required string Name { get; set; }
        public required Func<object, object?> GetValue { get; init; }
        public required Action<object, object?> SetValue { get; init; }

        public static Accessor Create(PropertyInfo pi)
        {
            var objParam = Expression.Parameter(typeof(object), "obj");
            Func<object, object?> getter = Expression.Lambda<Func<object, object?>>(
                Expression.Convert(
                    Expression.Call(
                        Expression.Convert(
                            objParam,
                            pi.DeclaringType!),
                        pi.GetMethod!),
                    typeof(object)),
                objParam)
                .Compile()
                ?? throw new InvalidOperationException();

            var valueParam = Expression.Parameter(typeof(object), "value");
            Action<object, object?> setter = Expression.Lambda<Action<object, object?>>(
                Expression.Call(
                    Expression.Convert(
                        objParam, pi.DeclaringType!),
                    pi.SetMethod!,
                    Expression.Convert(
                        valueParam, pi.PropertyType)),
                objParam,
                valueParam)
                .Compile()
                ?? throw new InvalidOperationException();

            return new Accessor
            {
                Name = pi.Name,
                Type = pi.PropertyType,
                GetValue = getter,
                SetValue = setter
            };
        }
    }
}
