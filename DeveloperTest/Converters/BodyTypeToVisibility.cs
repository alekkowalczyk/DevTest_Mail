using DeveloperTest.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DeveloperTest.Converters
{
    class BodyTypeToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(parameter is string && value is BodyType)
            {
                var strType = parameter.ToString();
                var type = (BodyType)value;
                if (type.ToString().ToUpper().Trim() == strType.ToUpper().Trim())
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
