using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Globalization;
using System.Windows.Data;

namespace DealBox.Helper
{
    public class DetailToTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string description = (string)value;
            string[] seperator=new string[] {"--"};
            string[] array = description.Split(seperator, StringSplitOptions.None);
            return array[0].Trim();
        }

        public object ConvertBack(object value, Type targetType,object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class DetailToSiteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string description = (string)value;
            string[] seperator = new string[] { "--" };
            string[] array = description.Split(seperator, StringSplitOptions.None);
            return array[1].Trim();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class DetailToPriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string description = (string)value;
            string[] seperator = new string[] { "--" };
            string[] array = description.Split(seperator, StringSplitOptions.None);
            return array[2].Trim();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
