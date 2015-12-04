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
	public class IsStringEqualOrNotConverter : IValueConverter
	{
		public static readonly IValueConverter Instance = new IsStringEqualOrNotConverter();

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool result = false;
			try
			{
				if (value.GetType() == typeof(string))
				{
					string strVal = (string)value;

					string compareToValue = parameter.AsStr();

					if (strVal.ToLowerInvariant() == compareToValue.ToLowerInvariant())
						result = true;
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.Message);
				Trace.WriteLine("value=[" + value + "]");
			}
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	} // class
} // namespace
