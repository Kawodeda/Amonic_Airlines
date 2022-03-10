using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AMONIC_Session3
{
    public class PriceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetTypes, object parameter, CultureInfo cultureInfo)
        {
            switch(System.Convert.ToInt32(values[1]))
            {
                case 1:
                    return values[0];
                case 2:
                    return Math.Floor((decimal)values[0] * 1.35m);
                case 3:
                    return Math.Floor((decimal)values[0] * 1.35m * 1.3m);
                default:
                    return values[0];
            }
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
