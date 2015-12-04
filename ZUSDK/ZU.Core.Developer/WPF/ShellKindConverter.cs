using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;


namespace ZU.WPF
{
	public class ShellKindConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return "Generic Item";

			dynamic shellObject = value;

			try
			{
				return shellObject.Properties.System.KindText.Value;
			}
			catch
			{
				return "Generic Item";
			}
		}
			

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	} // class
} // namespace
