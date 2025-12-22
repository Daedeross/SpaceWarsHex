using ReactiveUI;
using SpaceWarsHex.ShipBuilder.Helpers;
using SpaceWarsHex.ShipBuilder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables.Fluent;
using System.Windows;
using System.Windows.Controls;

namespace SpaceWarsHex.ShipBuilder.Views
{
    /// <summary>
    /// Interaction logic for EnergyWeaponsView.xaml
    /// </summary>
    public partial class EnergyWeaponsView : ReactiveUserControl<EnergyWeaponsViewModel>
    {
        private const double GridViewPadding = 10d;
        private readonly Dictionary<object, GridViewColumnHeader> _headers = [];

        public EnergyWeaponsView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel,
                        vm => vm.EnergyWeapons,
                        v => v.WeaponsList.ItemsSource)
                    .DisposeWith(d);
                this.Bind(ViewModel,
                        vm => vm.SelectedWeapon,
                        v => v.WeaponsList.SelectedItem)
                    .DisposeWith(d);

                this.OneWayBind(ViewModel,
                        vm => vm.StockEnergyWeapons,
                        v => v.StockEnergyWeapons.ItemsSource)
                    .DisposeWith(d);
                this.Bind(ViewModel,
                        vm => vm.SelectedStockWeapon,
                        v => v.StockEnergyWeapons.SelectedItem)
                    .DisposeWith(d);

                //this.BindCommand(ViewModel,
                //        vm => vm.AddEnergyWeaponCommand,
                //        v => v.AddBtn)
                //    .DisposeWith(d);
                //this.BindCommand(ViewModel,
                //        vm => vm.RemoveEnergyWeaponCommand,
                //        v => v.RemoveBtn)
                //    .DisposeWith(d);
            });
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            ListView listView = (ListView)sender;
            GridView gridView = (GridView)listView.View;

            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth - GridViewPadding;

            var usedWidth = gridView.Columns
                .Where(c => !MyListBoxItemAssist.GetFillRemaining(c))
                .Sum(c => Math.Max(c.ActualWidth, MyListBoxItemAssist.FindColumnHeader(_headers, listView, c)?.ActualWidth ?? 0d));
            var remainingWidth = workingWidth - usedWidth;
            var columsToFill = gridView.Columns.Where(MyListBoxItemAssist.GetFillRemaining).ToList();

            if (columsToFill.Count != 0 && remainingWidth > 0)
            {
                var each = remainingWidth / columsToFill.Count;
                var remainder = remainingWidth % columsToFill.Count;

                foreach (var column in columsToFill)
                {
                    column.Width = each;
                }

                columsToFill.Last().Width += remainder;
            }
        }
    }
}
