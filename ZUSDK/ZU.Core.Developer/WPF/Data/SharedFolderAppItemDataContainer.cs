using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ZU.WPF.Data
{
	/// <summary>
	/// This class supports Zet Universe and is not intended to be used directly from your code.
	/// </summary>
	[Serializable]
	public class SharedFolderAppItemDataContainer
	{
		public string FileSystemPath { get; internal set; }
		public bool IsFolder { get; internal set; }
		public string Title { get; internal set; }
		public string RootFolder { get; internal set; }

		public string AccountId { get; internal set; }

		public string ContainerEntityKind { get; internal set; }

		public SharedFolderAppItemDataContainer(string sharedFolderItemPath, string title, string rootFolder, bool isFolder, string accountId, string containerEntityKind)
		{
			this.IsFolder = isFolder;
			this.FileSystemPath = sharedFolderItemPath;
			this.Title = title;
			this.RootFolder = rootFolder;
			this.AccountId = accountId;
			this.ContainerEntityKind = containerEntityKind;
		}
	} // class
} // namespace
