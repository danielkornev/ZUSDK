using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	public static class DirectoryInfoExtensions
	{
		public static bool IsDirectoryAccessable(string path)
		{
			if (!System.IO.Directory.Exists(path))
				return false;

			string NtAccountName = String.Concat(System.Environment.UserDomainName, @"\", System.Environment.UserName);
			WindowsPrincipal wp = Thread.CurrentPrincipal as WindowsPrincipal;

			var wi = WindowsIdentity.GetCurrent();
			//var groups = wi.Groups.ToList();

			DirectoryInfo di = new DirectoryInfo(path);

			string root = string.Empty;

			if (di.Parent == null)
			{
				// root is the path
				root = path.ToLowerInvariant();
			}
			else
			{
				// let's obtain a root via the Root property
				root = di.Root.FullName.ToLowerInvariant();
			}

			var drive = System.IO.DriveInfo.GetDrives().Where(d => d.RootDirectory.ToString().ToLowerInvariant() == root.ToLowerInvariant()).FirstOrDefault();

			if (drive == null)
				return false; // this is a crazy moment
				
			// network drives are special. we should treat them differently...
			if (drive.DriveType == DriveType.Network)
			{
				// this is a network drive
				try
				{
					// we try to get creation time from the given directory. If failed, we don't have access
					var dtCreated = Directory.GetCreationTimeUtc(path);
					return true;
				}	
				catch (Exception ex)
				{
					return false;
				}
			}
			

			DirectorySecurity acl = di.GetAccessControl(AccessControlSections.Access);
			AuthorizationRuleCollection rules = acl.GetAccessRules(true, true, typeof(NTAccount));

			//Go through the rules returned from the DirectorySecurity
			foreach (AuthorizationRule rule in rules)
			{
				var ir = rule.IdentityReference;

				string name = rule.IdentityReference.Value.ToLowerInvariant();

				//If we find one that matches the identity we are looking for
					if (name.Equals(NtAccountName, StringComparison.CurrentCultureIgnoreCase))
					{
						// let's check its rights
						if (HasReadDataRights(rule)) return true;
					}


				PrincipalType pType;
				if (!WindowsPrincipalExtensions.TryGetPrincipalType(name, out pType))
					continue;

				if (pType == PrincipalType.Group)
				{
					if (wp.IsInRole(name))
					{
						// our user is a member of this group

						// let's check its rights
						if (HasReadDataRights(rule)) return true;
					}
				}					
			}

			return false;
		}

		private static bool HasReadDataRights(AuthorizationRule rule)
		{
			//Cast to a FileSystemAccessRule to check for access rights
			if ((((FileSystemAccessRule)rule).FileSystemRights & FileSystemRights.ReadData) > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	} // class
} // namespace
