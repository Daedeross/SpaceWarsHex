using ReactiveUI;
using SpaceWarsHex.ShipBuilder.ViewModels;
using SpaceWarsHex.ShipBuilder.Views;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable enable

namespace SpaceWarsHex.ShipBuilder.Configuration
{
    internal class WindsorViewLocator : IViewLocator
    {
        private readonly IViewFactory _viewFactory;
        private readonly MethodInfo _resolve;
        private readonly ConcurrentDictionary<Type, Func<IViewFor?>> _cache
            = new();

        public WindsorViewLocator(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
            _resolve = typeof(WindsorViewLocator)
                .GetMethod(nameof(Resolve), BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance)
                ?? throw new InvalidOperationException();
        }

        public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
        {
            if (viewModel is null)
                return null;

            var type = viewModel.GetType();

            if (typeof(ViewModelBase).IsAssignableFrom(type))
            {
                var foo = GetView(type);
                return foo;
            }

            return null;
        }

        private IViewFor<T> Resolve<T>()
            where T : class
        {
            return _viewFactory.For<T>();
        }

        private IViewFor? GetView(Type type)
        {
            return _cache.GetOrAdd(type, CreateDelegate)();
        }

        private Func<IViewFor?> CreateDelegate(Type type)
        {
            // Determine the actual type to resolve
            // If it's a class, find the interface named I{ClassName}
            // Otherwise, use the type itself
            Type actualType;
            if (type.IsClass)
            {
                actualType = type.GetInterfaces()
                    .Where(x => string.Equals(x.Name, $"I{type.Name}"))
                    .SingleOrDefault(type);
            }
            else
            {
                actualType = type;
            }

            var mi = _resolve
                .MakeGenericMethod(actualType);

            //var param = Expression.Parameter(typeof(Type));

            return Expression.Lambda<Func<IViewFor?>>(
                Expression.Convert(
                    Expression.Call(Expression.Constant(this), mi),
                    typeof(IViewFor)
                    )
                ).Compile();
        }
    }
}
