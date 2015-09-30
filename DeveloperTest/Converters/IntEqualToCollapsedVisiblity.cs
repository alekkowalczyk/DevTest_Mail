using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DeveloperTest.Converters
{
    class IntEqualToCollapsedVisiblity : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int && parameter != null)
            {
                int i = 0;
                if (int.TryParse(parameter.ToString(), out i))
                {
                    int v = (int)value;
                    if (v == i)
                        return Visibility.Collapsed;
                    else
                        return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
