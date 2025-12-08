using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SpaceWarsHex.ShipBuilder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWarsHex.ShipBuilder.Configuration
{
    public class ViewModelInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyContaining<WorkspaceViewModel>()
                        .BasedOn<IViewModel>()
                        .WithServiceSelf()
                        //.WithServiceDefaultInterfaces()
                        .LifestyleTransient(),
                Component.For<IViewModelFactory>()
                        .AsFactory()
                    );
        }
    }
}
