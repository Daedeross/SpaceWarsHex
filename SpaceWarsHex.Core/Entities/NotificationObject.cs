using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SpaceWarsHex.Entities
{
    /// <summary>
    /// Root class for entities and any other object that fires <see cref="INotifyPropertyChanged.PropertyChanged"/> events.
    /// </summary>
    public class NotificationObject : INotifyPropertyChanged
    {

        /// <inheritdoc />
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Helper method to fire <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propName">The name of the property that changed.</param>
        protected void RaisePropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Helper method to fire <see cref="PropertyChanged"/> event when a sub-cobject changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">The bubbled event args.</param>
        protected void BubblePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// Helper method to fire <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field to set.</param>
        /// <param name="value">The value to set the field to.</param>
        /// <param name="propName">The name of the property that changed.</param>
        /// <returns>True if the value was changed, otherwise False.</returns>
        protected bool RaiseAndSetIfChanged<T>(ref T field, T value, [CallerMemberName] string propName = "")
        {
            if (!Equals(field, value))
            {
                field = value;
                RaisePropertyChanged(propName);
                return true;
            }

            return false;
        }
    }
}
