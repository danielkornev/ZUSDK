using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace System.Windows
{
	public class IsEqualOrGreaterThanConverter : IValueConverter
	{
		public static readonly IValueConverter Instance = new IsEqualOrGreaterThanConverter();

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool result = false;
			try
			{
				double doubleValue = (double)value;
				double compareToValue = parameter.AsStr().As<double>(0.0);//System.Convert.ToDouble(parameter);

				//var treshold = 0.5;

				result = doubleValue >= compareToValue;
			}
			catch (Exception ex)
			{
				Trace.WriteLine("EXCEPTION: " + ex.Message);
				Trace.WriteLine("value=["+ value + "]");
			}
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	} // class
} // namespace
