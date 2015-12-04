using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ZU.Core.Structured;

namespace ZU.WPF
{
	public class EntityReviewStatusToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter,
		  CultureInfo culture)
		{
			Visibility result = Visibility.Collapsed;

			if (value == null)
			{
				// do nothing
			}
			else // we suppose value is entity
			{
				if (value.GetType() == typeof(ReviewStatus))
				{
					var reviewStatus = (ReviewStatus)value;

					switch (reviewStatus)
					{
						case ReviewStatus.None:
							result = Visibility.Collapsed;

							break;
						case ReviewStatus.InProgress:
						case ReviewStatus.ActionRequired:
						case ReviewStatus.Done:
						default:
							result = Visibility.Visible;
							break;
					}
				}
			}

			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter,
		  CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	} // class
} // namespace
