using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Net
{
	public static class Host
	{
		/// <summary>
		/// Source: http://stackoverflow.com/questions/354477/method-to-determine-if-path-string-is-local-or-remote-machine
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static bool IsLocalHost(string input)
		{
			IPAddress[] host;
			//get host addresses
			try { host = Dns.GetHostAddresses(input); }
			catch (Exception) { return false; }
			//get local adresses
			IPAddress[] local = Dns.GetHostAddresses(Dns.GetHostName());
			//check if local
			return host.Any(hostAddress => IPAddress.IsLoopback(hostAddress) || local.Contains(hostAddress));
		}
	} // class
} // namespace
