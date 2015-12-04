using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Security.Principal
{
	public static class WindowsPrincipalExtensions
	{
		#region Interop
		private enum SID_NAME_USE
		{
			SidTypeUser = 1,
			SidTypeGroup,
			SidTypeDomain,
			SidTypeAlias,
			SidTypeWellKnownGroup,
			SidTypeDeletedAccount,
			SidTypeInvalid,
			SidTypeUnknown,
			SidTypeComputer
		}
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool LookupAccountSid(
				string lpSystemName,
				[MarshalAs(UnmanagedType.LPArray)] byte[] Sid,
				StringBuilder lpName,
				ref uint cchName,
				StringBuilder ReferencedDomainName,
				ref uint cchReferencedDomainName,
				out SID_NAME_USE peUse);
		#endregion

		public static bool IsInGroup(string user, string group)
		{
			using (var identity = new WindowsIdentity(user))
			{
				var principal = new WindowsPrincipal(identity);
				return principal.IsInRole(group);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domainQualifiedName"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool TryGetPrincipalType(string domainQualifiedName, out PrincipalType type)
		{
			var name = new StringBuilder();
			var cchName = (uint)name.Capacity;
			var referencedDomainName = new StringBuilder();
			var cchReferencedDomainName = (uint)referencedDomainName.Capacity;
			SID_NAME_USE sidType;

			var account = new NTAccount(domainQualifiedName);

			try
			{
				var id = new SecurityIdentifier(account.Translate(typeof(SecurityIdentifier)).Value);
				var sidBuffer = new byte[id.BinaryLength];
				id.GetBinaryForm(sidBuffer, 0);

				if (LookupAccountSid(null, sidBuffer, name, ref cchName, referencedDomainName,
									 ref cchReferencedDomainName, out sidType))
				{
					switch (sidType)
					{
						case SID_NAME_USE.SidTypeGroup:
						case SID_NAME_USE.SidTypeWellKnownGroup:
						case SID_NAME_USE.SidTypeAlias:
							type = PrincipalType.Group;
							return true;
						case SID_NAME_USE.SidTypeUser:
							type = PrincipalType.User;
							return true;
					}
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine("Failed to analyze given NT Account: \"" + domainQualifiedName + "\"");
			}
			type = default(PrincipalType);
			return false;
		}
		
	} // class


	public enum PrincipalType
	{
		User,
		Group
	}
} // namespace
