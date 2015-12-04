using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace System.Windows
{
	public class MultipleInt : ConvertorBase<MultipleInt>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			int val = (int)value;
			int mul = parameter.AsStr().As<int>(0);
			return val * mul;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}//class
}//namespace
