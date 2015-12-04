using System;
using System.ComponentModel;
using System.Globalization;

namespace System
{
	public static class ObjExts
	{
		public static string AsStr<T>(this T @this)
		{
			string result = string.Empty;
			var converter = TypeDescriptor.GetConverter(typeof(T));
			if (converter != null)
			{
				try
				{
					result = converter.ConvertToInvariantString(@this);
				}
				catch (Exception ex)
				{
					//Log.Exception(ex);
				}
			}
			else
			{
				//Log.Warning("No converter for ({0})", typeof(T));
			}
			return result;
		}////AsStr<T>()

		public static T As<T>(this string @this)
		{
			return @this.As<T>(default(T));
		}////As<T>()

		public static T As<T>(this string @this, T defaultValue)
		{
			T result = defaultValue;
			if (!string.IsNullOrEmpty(@this))
			{
				var converter = TypeDescriptor.GetConverter(typeof(T));
				if (converter != null)
				{
					try
					{
						T converted = (T)converter.ConvertFrom(null, CultureInfo.InvariantCulture, @this);
						result = converted;
					}
					catch (Exception ex)
					{
						//-Console.Error.WriteLine(ex);
						//Log.Exception(ex);
					}
				}
				else
				{
					//Log.Warning("No converter for ({0})", typeof(T));
					//-Console.Error.WriteLine("no converted for ({0})", typeof(T));
				}
			}
			return result;
		}//As<T>(defaultValue)

	}//class
}//namespace
