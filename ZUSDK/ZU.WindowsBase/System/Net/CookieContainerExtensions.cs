using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Net
{
	public static class CookieContainerExtensions
	{
		public static List<Cookie> ToList(this CookieContainer container, Uri uri)
		{
			var list = new List<Cookie>();

			if (uri == null)
				throw new Exception("Uri is null");

			list = container.GetCookies(uri).OfType<Cookie>().ToList();

			return list;
		}
	} // class
} // namespace
