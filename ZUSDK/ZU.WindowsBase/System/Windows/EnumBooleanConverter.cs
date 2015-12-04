using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Data;

namespace System.Windows
{
	/// <summary>
	/// from here: http://stackoverflow.com/questions/397556/how-to-bind-radiobuttons-to-an-enum
	/// </summary>
	public class EnumBooleanConverter : IValueConverter
	{
		#region IValueConverter Members
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var paramType = parameter.GetType();

			string parameterString = parameter.ToString(); // as string
			if (parameterString == null)
				return DependencyProperty.UnsetValue;

			if (Enum.IsDefined(value.GetType(), value) == false)
				return DependencyProperty.UnsetValue;

			object parameterValue = Enum.Parse(value.GetType(), parameterString);

			return parameterValue.Equals(value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string parameterString = parameter as string;
			if (parameterString == null)
				return DependencyProperty.UnsetValue;

			return Enum.Parse(targetType, parameterString);
		}
		#endregion
	} // class
} // namespace