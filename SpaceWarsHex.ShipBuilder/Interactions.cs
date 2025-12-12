using ReactiveUI;
using System.Windows;

namespace SpaceWarsHex.ShipBuilder
{
    public static class Interactions
    {
        private static Window? _mainWindow;

        public static void Initialize(Window mainWindow)
        {
            _mainWindow = mainWindow.GetOrThrow();
            ShowEditDialog.RegisterHandler(context =>
            {
                var dialog = new DialogWindow(context.Input);
                dialog.Owner = _mainWindow;
                var ok = dialog.ShowDialog();

                context.SetOutput(
                    ok == true
                    ? dialog.ViewModel
                    : null
                );
            });
        }

        public static readonly Interaction<object, object?> ShowEditDialog = new();
    }
}
