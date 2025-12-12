using System;
using System.Globalization;
using System.Windows.Data;

namespace SpaceWarsHex.ShipBuilder.Converters
{
    public class EnumValuesConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type? enumType = null;

            if (value is Type t && t.IsEnum)
                enumType = t;
            else if (value != null)
            {
                var vType = value.GetType();
                if (vType.IsEnum)
                    enumType = vType;
                else if (Nullable.GetUnderlyingType(vType) is Type underlying && underlying.IsEnum)
                    enumType = underlying;
            }

            return enumType == null ? null : Enum.GetValues(enumType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not usually needed for ItemsSource; keep default behavior.
            return Binding.DoNothing;
        }
    }
}
