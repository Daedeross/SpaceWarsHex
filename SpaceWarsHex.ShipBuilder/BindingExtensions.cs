using MahApps.Metro.Controls;
using ReactiveUI;
using System;

namespace SpaceWarsHex.ShipBuilder
{
    public static class BindingExtensions
    {
        public static IDisposable BindSpin<TViewModel>(this IViewFor<TViewModel> view,
            System.Linq.Expressions.Expression<Func<TViewModel, int>> vmProperty,
            NumericUpDown spinControl)
            where TViewModel : class
        {
            return view.Bind(view.ViewModel,
                vmProperty,
                v => spinControl.Value,
                vmProp => Convert.ToDouble(vmProp),
                viewProp => Convert.ToInt32(viewProp));
        }
    }
}
