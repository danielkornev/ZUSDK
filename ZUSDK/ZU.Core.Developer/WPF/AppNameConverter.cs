using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ZU.Core;

namespace ZU.WPF
{
	public sealed class AppNameConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var entity = value as IEntity;

			if (entity == null)
				return string.Empty;

			return entity.ModelContext.SIM.GetAccountName(entity.AppId);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		#endregion
	} // class
} // namespace
