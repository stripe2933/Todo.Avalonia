using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Todo.Views.Converters;

public abstract class NullableToBoolConverter<T> : IValueConverter
{
    public abstract T GetDefaultValue();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is not null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return null;
        }
        else
        {
            return default(T);
        }
    }
}