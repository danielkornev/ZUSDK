using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace System.Windows
{
	public class ObjectToBooleanConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter,
		  CultureInfo culture)
		{
			if (value != null)
			{
				if (value.GetType() == typeof(bool))
					return (bool)value;
				else
					return false;
			}
			else
				return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter,
		  CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
