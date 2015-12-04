using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.Core
{
	public enum ModelKinds
	{
		// Legacy block: old file-based spaces
		/// <summary>
		/// Local space folder residing in current user's local profile
		/// </summary>
		LegacyLocalFolder,
		/// <summary>
		/// Synced space folder residing in current user's Dropbox folder
		/// </summary>
		LegacyDropbox,
		/// <summary>
		/// Local Metaspace residing in current user's local profile
		/// </summary>
		LegacyLocalMetaspace,
		/// <summary>
		/// Synced Metaspace residing in current user's Dropbox folder
		/// </summary>
		LegacyDropboxMetaspace,
		// Roaming block: file-based spaces residing in %AppData%
		/// <summary>
		/// Local Metaspace folder residing in %AppData%
		/// </summary>
		RoamingMetaspace,
		/// <summary>
		/// Local space folder residing in %AppData%
		/// </summary>
		RoamingFolder,
		// Beta block: non-file-based spaces
		/// <summary>
		/// Local non-file-based Metaspace residing in %AppData%. Exact data format details TBD.
		/// </summary>
		RoamingTBDMetaspace,
		/// <summary>
		/// Local non-file-based space residing in %AppData%. Exact data format details TBD.
		/// </summary>
		RoamingTBDWorkspace
	} // enum
} // namespace
