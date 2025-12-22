using Castle.Facilities.Logging;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder.Inspectors;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Services.Logging.NLogIntegration;
using Castle.Windsor;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Serialization;
using SpaceWarsHex.ShipBuilder.ViewModels;
using SpaceWarsHex.ShipBuilder.Views;
using System;
using System.IO;
using System.Linq;

namespace SpaceWarsHex.ShipBuilder.Configuration
{
    public class ApplicationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var propInjector = container.Kernel.ComponentModelBuilder
                         .Contributors
                         .OfType<PropertiesDependenciesModelInspector>()
                         .Single();
            container.Kernel.ComponentModelBuilder.RemoveContributor(propInjector);

            container.AddFacility<TypedFactoryFacility>();

            container.AddFacility<LoggingFacility>(f => f.LogUsing<NLogFactory>());

            container.Register(
                Classes.FromAssemblyContaining<MainWindow>()
                    .BasedOn(typeof(ReactiveUI.IViewFor<>))
                    .WithServiceSelf()
                    .WithServiceAllInterfaces()
                    .LifestyleTransient(),
                Component.For<IViewFactory>()
                    .AsFactory(),
                Component.For<ReactiveUI.IViewLocator>()
                    .ImplementedBy<WindsorViewLocator>()
                    .Named(nameof(WindsorViewLocator)),
                Component.For<IViewContainer>()
                    .ImplementedBy<ViewContainer>()
                    .LifestyleTransient()
                    );

            container.Register(
                Classes.FromAssemblyContaining<MainWindow>()
                    .BasedOn<ReactiveUI.IBindingTypeConverter>()
                    .WithServiceBase());

            container.Register(
                Classes.FromAssemblyContaining<ReactiveUI.CommandBinderImplementation>()
                    .IncludeNonPublicTypes()
                    .BasedOn<ReactiveUI.CommandBinderImplementation>()
                    .WithServiceDefaultInterfaces()
                    .Configure(r => r.Named("ICommandBinderImplementation"))
                    .LifestyleSingleton());

            //#region Serialization

            container.Register(
                Component.For<JsonSerializer>()
                    .UsingFactoryMethod(kernel =>
                    {
                        var serializer = new JsonSerializer();
                        serializer.Converters.Add(new StringEnumConverter());
                        return serializer;
                    })
                    .LifestyleSingleton(),
                Component.For<IPrototypeCache>()
                    .ImplementedBy<PrototypeDatabase>()
                    .OnCreate(LoadWeapons)
                    .LifestyleSingleton(),
                Component.For<IPrototypeSerializer>()
                    .ImplementedBy<PrototypeSerializer>()
                    .LifestyleSingleton());

            //#endregion
        }

        private static void LoadWeapons(IKernel kernel, IPrototypeCache cache)
        {
            var ser = kernel.Resolve<IPrototypeSerializer>();
            try
            {
                foreach (var file in Directory.EnumerateFiles("Resources", "*.json"))
                {
                    using var stream = File.OpenRead(file);
                    if (stream is null) continue;
                    var weapons = ser.Deserialize<PregenWeapons>(file);
                    if (weapons.Id == Guid.Empty)
                    {
                        weapons.Id = Guid.NewGuid();
                    }
                    cache.AddOrUpdate(weapons as IPregenWeapons);
                }
            }
            finally
            {
                kernel.ReleaseComponent(ser);
            }
        }
    }
}
