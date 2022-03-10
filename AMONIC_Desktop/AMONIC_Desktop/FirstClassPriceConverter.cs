using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMONIC_Desktop
{
    public class FirstClassPriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            return Math.Floor((decimal)value * 1.35m * 1.3m);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
