using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ZU.Semantic.Core;

namespace ZU.WPF
{
	public class LinkedItemNetworkOperationStatusToTextConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter,
		  CultureInfo culture)
		{
			string status = "Unknown";

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
							status = "Copying...";
							break;
						case LinkedItemNetworkOperationStatus.DownloadInProgress:
							status = "Downloading...";
							break;
						case LinkedItemNetworkOperationStatus.UploadInProgress:
							status = "Uploading...";
							break;
						case LinkedItemNetworkOperationStatus.Complete:
							status = string.Empty;
							break;
						case LinkedItemNetworkOperationStatus.Failed:
							status = "Operation Failed";
							break;
						case LinkedItemNetworkOperationStatus.Unknown:
							status = "Status Unknown";
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