using Microsoft.Extensions.DependencyInjection;
using SpaceWarsHex.Avalonia.ViewModels;

namespace SpaceWarsHex.Avalonia.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommonServices(this IServiceCollection collection)
        {
            collection.AddTransient<MainViewModel>();
        }
    }
}
