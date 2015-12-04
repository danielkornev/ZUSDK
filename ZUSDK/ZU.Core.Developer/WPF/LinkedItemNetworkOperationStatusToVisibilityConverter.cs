using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ZU.Semantic.Core;

namespace ZU.WPF
{
	public class LinkedItemNetworkOperationStatusToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter,
		  CultureInfo culture)
		{
			Visibility status = Visibility.Collapsed;

			if (value == null)
			{
				// do nothing
			}
			else // we suppose value is entity
			{
				if (value.GetType() == typeof(LinkedItemNetworkOperationStatus))
				{
					var networkOperationStatus = (LinkedItemNetworkOperationStatus)value;

					switch (networkOperationStatus)
					{
						case LinkedItemNetworkOperationStatus.CopyInProgress:
							status = Visibility.Visible;
							break;
						case LinkedItemNetworkOperationStatus.DownloadInProgress:
							status = Visibility.Visible;
							break;
						case LinkedItemNetworkOperationStatus.UploadInProgress:
							status = Visibility.Visible;
							break;
						case LinkedItemNetworkOperationStatus.Complete:
							status = Visibility.Collapsed;
							break;
						case LinkedItemNetworkOperationStatus.Failed:
							status = Visibility.Visible;
							break;
						case LinkedItemNetworkOperationStatus.Unknown:
							status = Visibility.Visible;
							break;
						default:
							break;
					}
				}
			}

			return status;
		}

		public object ConvertBack(object value, Type targetType, object parameter,
		  CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	} // class
} // namespace