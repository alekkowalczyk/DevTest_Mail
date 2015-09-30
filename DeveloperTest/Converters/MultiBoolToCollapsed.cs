using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DeveloperTest.Converters
{
    public class MultiBoolToCollapsed : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string oper = "AND";
            if (parameter != null && !string.IsNullOrWhiteSpace(parameter.ToString()))
            {
                if (parameter.ToString().Trim().ToUpper() == "OR")
                    oper = "OR";
            }

            if (oper == "AND")
            {
                foreach (var obj in values)
                {
                    if (!(obj is bool))
                        return Visibility.Collapsed;
                    var b = (bool)obj;
                    if (!b)
                        return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
            else
            {
                foreach (var obj in values)
                {
                    if ((obj is bool) && ((bool)obj))
                        return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
