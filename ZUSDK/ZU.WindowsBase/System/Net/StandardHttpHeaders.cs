using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Net
{
	public static class StandardHttpHeaders
	{
		public static bool IsStandardHeader(string headerName)
		{
			if (headerName == "Accept" ||
				headerName == "Connection" ||
				headerName == "Content-Length" ||
				headerName == "Content-Type" ||
				headerName == "Expect" ||
				headerName == "Date" ||
				headerName == "Host" ||
				headerName == "If-Modified-Since" ||
				headerName == "Range" ||
				headerName == "Referer" ||
				headerName == "Transfer-Encoding" ||
				headerName == "User-Agent")
				return true;

			return false;
		}


	} // class
} // namespace
