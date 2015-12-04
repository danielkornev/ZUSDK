using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace System.Windows
{
	[ValueConversion(typeof(bool), typeof(Visibility))]
	public sealed class BoolToVisibilityConverter : IValueConverter
	{
		public Visibility TrueValue
		{
			get;
			set;
		}

		public Visibility FalseValue
		{
			get;
			set;
		}

		public object UnknownValue
		{
			get;
			set;
		}

		public BoolToVisibilityConverter()
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
			if (Equals(value, TrueValue))
				return true;
			if (Equals(value, FalseValue))
				return false;
			return null;
		}
	}
}
