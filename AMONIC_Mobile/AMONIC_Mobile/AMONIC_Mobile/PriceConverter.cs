using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AMONIC_Mobile
{
    public class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            decimal price = 0m;

            if(decimal.TryParse(value.ToString(), out price))
            {
                if(price == 0m)
                {
                    return "Free";
                }
                else
                {
                    return price.ToString("c", new CultureInfo("en-us"));
                }
            }
            else
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
