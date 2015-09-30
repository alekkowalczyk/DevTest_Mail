using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DeveloperTest.Converters
{
    class CollectionToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var retString = "";
            if(value is IEnumerable)
            {
                var isFirst = true;
                foreach(var element in value as IEnumerable)
                {
                    if (element != null)
                    {
                        if (isFirst)
                            isFirst = false;
                        else
                            retString += "; ";
                        retString += element.ToString() ;
                    }
                }
            }
            return retString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
