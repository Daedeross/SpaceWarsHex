using System;
using System.Windows.Data;

namespace SpaceWarsHex.ShipBuilder.Converters
{
    [ValueConversion(typeof(double), typeof(int))]
    public class DoubleToIntegerConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double d)
            {
                return (int)Math.Round(d);
            }
            if (value is float f)
            {
                return (int)Math.Round(f);
            }
            return 0;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int i)
            {
                return (double)i;
            }
            return 0.0;
        }
    }
}
