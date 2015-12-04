using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using ZU.Core;
using ZU.Core.Structured;

namespace ZU.WPF
{
	public class EntityReviewStatusConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter,
		  CultureInfo culture)
		{
			string status = "#FFFFFFFF";

			if (value == null)
			{
				// do nothing
			}
			else // we suppose value is entity
			{
				if (value.GetType() == typeof(ReviewStatus))
				{
					var reviewStatus = (ReviewStatus)value;

					//if (value.GetType() == typeof(Entity))
					//{
					//	Entity entity = (Entity)value;

					//	if (entity.PropsDict.ContainsKey("ReviewStatus"))
					//	{
					//		//var rs = (int)entity.Props.ReviewStatus;

					//		ReviewStatus reviewStatus = entity.ReviewStatus;
					//			//(ReviewStatus)rs;

					//if (reviewStatus != null)
					//{
					switch (reviewStatus)
					{
						case ReviewStatus.None:
							status = "#FFFFFFFF"; // white

							break;
						case ReviewStatus.InProgress:
							status = "#FFFF8000"; // orange like in iOS with Cydonia "#FFFFD700"; // yellow

							break;
						case ReviewStatus.ActionRequired:
							status = "#FFFF0303"; // red like in iOS //"#FFDC143C"; // red

							break;
						case ReviewStatus.Done:
							status = "#FF06B025"; // green

							break;
						default:
							break;
					}
				}
			}

			//BrushConverter bc = new BrushConverter();
			//var brush = (Brush) bc.ConvertFromString(status);

			var color = (Color) ColorConverter.ConvertFromString(status);

			return color;
		}

		public object ConvertBack(object value, Type targetType, object parameter,
		  CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class EntityUnreadStatusConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter,
		  CultureInfo culture)
		{
			FontWeight fontWeight = FontWeights.Normal;

			if (value == null)
			{
				// do nothing
			}
			else // we suppose value is boolean
			{
				if (value.GetType() == typeof(bool))
				{
					var unreadStatus = (bool)value;

					if (unreadStatus)
					{
						fontWeight = FontWeights.Bold;
					}

				}
			}

			return fontWeight;
		}

		public object ConvertBack(object value, Type targetType, object parameter,
		  CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
