using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using ZU.Core;
using ZU.Core.Apps;

namespace ZU.WPF
{
	public class KindConverter : IValueConverter
	{
		public static IValueConverter Instance = new KindConverter();

		public static string GetKindDescription(IEntity entity)
		{
			return Instance.Convert(entity, typeof(IEntity), null, CultureInfo.CurrentCulture).ToString();
		}

		#region IValueConverter Members

		[HandleProcessCorruptedStateExceptions]
		[SecurityCritical]
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string kind = "Item";

			if (value == null) return kind;

			var entity = value as IEntity;

			if (entity == null) return kind;

			string entityKind = entity.Kind;

			if (entity.ModelContext.SIM == null)
				return "unknown kind (entity not attached to Zet Universe)";

			kind = entity.ModelContext.SIM.GetEntityKindDiplayName(entityKind);

			// Creates a TextInfo based on the "en-US" culture.
			TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
			kind = textInfo.ToTitleCase(kind);

			return kind;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	} // class
} // namespace
