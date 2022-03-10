using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AMONIC_Desktop
{
    public class BusinessPriceConverter : IValueConverter
    {
        public object Convert(object value,  Type targetType, object parameter, CultureInfo cultureInfo)
        {
            return Math.Floor((decimal)value * 1.35m);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
