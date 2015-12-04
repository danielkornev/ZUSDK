using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace System.Windows
{
	/// <summary>
	/// Base class for MarkupExtension convertors.
	/// </summary>
	// See: http://habrahabr.ru/post/141107/
	public abstract class ConvertorBaseThis<T> : MarkupExtension, IValueConverter where T : class, new()
	{
		/// <summary>
		/// Must be implemented in inheritor.
		/// </summary>
		public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
		/// <summary>
		/// Override if needed.
		/// </summary>
		public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	#region MarkupExtension members
		// Instance approach
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	#endregion
	}
}//namespace
