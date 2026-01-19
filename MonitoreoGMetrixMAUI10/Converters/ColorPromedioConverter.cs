using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace MonitoreoGMetrixMAUI10.Converters
{
    public class ColorPromedioConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return Colors.Transparent;

            decimal promedio;

            switch (value)
            {
                case decimal d:
                    promedio = d;
                    break;
                case double db:
                    promedio = (decimal)db;
                    break;
                case float f:
                    promedio = (decimal)f;
                    break;
                case int i:
                    promedio = i;
                    break;
                case long l:
                    promedio = l;
                    break;
                case string s:
                    // try culture-aware parsing, invariant, and comma-to-dot fallback
                    if (!decimal.TryParse(s, NumberStyles.Any, culture ?? CultureInfo.CurrentCulture, out promedio)
                        && !decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out promedio)
                        && !decimal.TryParse(s.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out promedio))
                    {
                        return Colors.Transparent;
                    }
                    break;
                default:
                    try
                    {
                        promedio = System.Convert.ToDecimal(value, culture ?? CultureInfo.CurrentCulture);
                    }
                    catch
                    {
                        return Colors.Transparent;
                    }
                    break;
            }

            if (promedio >= 800m)
                return Color.FromArgb("#CDEFDA");
            if (promedio >= 600m)
                return Color.FromArgb("#FFD7B1");
            return Color.FromArgb("#FFCCCC");
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
