using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace Microsoft.Win32
{
	public class ShellUserProfileHelper
	{
		[DllImport("shell32.dll", EntryPoint = "#261",
				   CharSet = CharSet.Unicode, PreserveSig = false)]
		private static extern void GetUserTilePath(
		  string username,
		  UInt32 whatever, // 0x80000000
		  StringBuilder picpath, int maxLength);

		public static string GetUserTilePath(string username)
		{   // username: use null for current user
			var sb = new StringBuilder(1000);
			GetUserTilePath(username, 0x80000000, sb, sb.Capacity);
			return sb.ToString();
		}

		public static Uri GetUserTileUri(string username)
		{
			Uri uri = new Uri(GetUserTilePath(username));
			return uri;
		}

		private enum EXTENDED_NAME_FORMAT
		{
			NameUnknown = 0,
			NameFullyQualifiedDN = 1,
			NameSamCompatible = 2,
			NameDisplay = 3,
			NameUniqueId = 6,
			NameCanonical = 7,
			NameUserPrincipal = 8,
			NameCanonicalEx = 9,
			NameServicePrincipal = 10,
			NameDnsDomain = 12
		}

		[DllImport("secur32.dll", CharSet = CharSet.Auto)]
		private static extern int GetUserNameEx(EXTENDED_NAME_FORMAT nameFormat, StringBuilder userName,
		   ref uint userNameSize);

		public static string GetUserDisplayName()
		{
			StringBuilder username = new StringBuilder(1024);
			uint size = (uint)username.Capacity;
			GetUserNameEx(EXTENDED_NAME_FORMAT.NameDisplay, username, ref size);

			return username.ToString();
		}

		/// <summary>
		/// Undocumented approach to obtain Microsoft account. See http://stackoverflow.com/questions/19740975/get-microsoft-account-id-from-windows-8-desktop-application
		/// </summary>
		/// <returns></returns>
		private static string GetAccoutName()
		{
			try
			{
				var wi = WindowsIdentity.GetCurrent();
				var groups = from g in wi.Groups
							 select new SecurityIdentifier(g.Value)
							 .Translate(typeof(NTAccount)).Value;
				var msAccount = (from g in groups
								 where g.StartsWith(@"MicrosoftAccount\")
								 select g).FirstOrDefault();
				return msAccount == null ? wi.Name :
					  msAccount.Substring(@"MicrosoftAccount\".Length);
			}
			catch (System.Security.Principal.IdentityNotMappedException ex)
			{
				return string.Empty;
			}
		}

		public static string GetUserMicrosoftAccount()
		{
			return GetAccoutName();
		}
	} // class
} // namespace
