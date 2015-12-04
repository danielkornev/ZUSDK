using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace System.Windows
{
	public sealed class BoolToValueConverter : IValueConverter
	{
		public object TrueValue
		{
			get;
			set;
		}

		public object FalseValue
		{
			get;
			set;
		}

		public object UnknownValue
		{
			get;
			set;
		}

		public BoolToValueConverter()
		{
			// set defaults
			TrueValue = Visibility.Visible;
			FalseValue = Visibility.Collapsed;
			UnknownValue = null;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool))
				return UnknownValue;
			return (bool)value ? TrueValue : FalseValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}//class
}//namespace
