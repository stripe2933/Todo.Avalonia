using System;

namespace Todo.Views.Converters;

public class DateTimeOffsetToBoolConverter : NullableToBoolConverter<DateTimeOffset?>
{
    public override DateTimeOffset? GetDefaultValue()
    {
        return DateTimeOffset.Now;
    }
}