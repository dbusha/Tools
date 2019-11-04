using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Tools.UI.WPF.Converters
{
    public class BoolValueConverter<T> : IValueConverter
    {
        public T TrueValue { get; set; }
        public T FalseValue { get; set; }
        
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool boolValue))
                return null;
            return boolValue ? TrueValue : FalseValue;
        }

        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value == null && TrueValue == null) 
                   || (value != null && value.Equals(TrueValue));
        }
    }
    
    
    public class BoolInverter : BoolValueConverter<bool> { }
    
    
    public class BoolToVisibility : BoolValueConverter<Visibility> { }
    
    
    public class BoolToBrush : BoolValueConverter<Brush> { }
    
    
    public class BoolToString: BoolValueConverter<string> { }
    
}