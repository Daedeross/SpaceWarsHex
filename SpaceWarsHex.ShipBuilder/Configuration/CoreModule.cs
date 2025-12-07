using ReactiveUI;
using ReactiveUI.Builder;
using ReactiveUI.Wpf;
using SpaceWarsHex.ShipBuilder.ViewModels;
using SpaceWarsHex.ShipBuilder.Views;
using Splat;
using Splat.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWarsHex.ShipBuilder.Configuration
{
    public sealed class CoreModule : IModule
    {
        public void Configure(IMutableDependencyResolver resolver)
        {
            resolver.RegisterSingletonViewForViewModel<WorkspaceView, WorkspaceViewModel>();
            resolver.RegisterViewsForViewModels(typeof(CoreModule).Assembly);
        }
    }
}
