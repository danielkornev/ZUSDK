using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System
{
	public static class LongExtensions
	{
		// http://stackoverflow.com/questions/281640/how-do-i-get-a-human-readable-file-size-in-bytes-abbreviation-using-net
		public static String BytesToString(this long byteCount)
		{
			string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
			if (byteCount == 0)
				return "0" + suf[0];
			long bytes = Math.Abs(byteCount);
			int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
			double num = Math.Round(bytes / Math.Pow(1024, place), 1);
			return (Math.Sign(byteCount) * num).ToString() + suf[place];
		}

		//public static string GetDescription(this string str)
		//{
		//	var fi = typeof(Constants.Kinds).GetField(str);

		//	var attribs = System.Attribute.GetCustomAttributes(str);

		//	var mi = 


		//	// Get information on the enum element
		//	FieldInfo fi = str.GetType().GetField(str.ToString());
		//	// Get description for elum element
		//	DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
		//	if (attributes.Length > 0)
		//	{
		//		// DescriptionAttribute exists - return that
		//		return attributes[0].Description;
		//	}
		//	// No Description set - return enum element name
		//	return str;
		//}
	} // class
} // namespace
