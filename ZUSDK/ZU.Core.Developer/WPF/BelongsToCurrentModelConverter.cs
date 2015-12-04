using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ZU.Core;

namespace ZU.WPF
{
	public sealed class BelongsToCurrentModelConverter : IMultiValueConverter
	{
		#region IMultiValueConverter Members
		/// <summary>
		/// Returns true if provided list is zero.
		/// </summary>
		/// <param name="values"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var list = values.ToList();

			if (list.Count == 0)
				return true;

			var entities = list.OfType<IEntity>().ToList();
			if (entities.Count == 0)
				return true;

			var models = list.OfType<IModel>().ToList();
			if (models.Count == 0)
				return true;

			var entity = entities.FirstOrDefault();
			var currentModel = models.FirstOrDefault();

			if (currentModel.ContainsEntityWithId(entity.Id))
				return true;

			return false;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}
		#endregion

	} // class
} // namespace
