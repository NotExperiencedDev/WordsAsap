using System;
using System.Windows;
using System.Windows.Data;

namespace WordsAsap.Converters
{
    [System.Windows.Markup.MarkupExtensionReturnType(typeof(IValueConverter))]
    public class VisibilityToBooleanColapsedConverter :
        System.Windows.Markup.MarkupExtension,  
        IValueConverter
    {
        private static VisibilityToBooleanColapsedConverter _converter;
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (Visibility)value == Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if(_converter==null)
               _converter = new VisibilityToBooleanColapsedConverter();
            return _converter;
        }
    }


    [System.Windows.Markup.MarkupExtensionReturnType(typeof(IValueConverter))]
    public class BooleanToVisibilityConverter :
        System.Windows.Markup.MarkupExtension,
        IValueConverter
    {
        private static BooleanToVisibilityConverter _converter;
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool) value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null)
                _converter = new BooleanToVisibilityConverter();
            return _converter;
        }
    }


    [System.Windows.Markup.MarkupExtensionReturnType(typeof(IValueConverter))]
    public class BooleanToColapsedConverter :
        System.Windows.Markup.MarkupExtension,
        IValueConverter
    {
        private static BooleanToColapsedConverter _converter;
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null)
                _converter = new BooleanToColapsedConverter();
            return _converter;
        }
    }
}
