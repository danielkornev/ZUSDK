using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZU.Configuration.Settings;
using ZU.WPF.Data;
using ZU.Core;

namespace ZU.Plugins
{
	public interface ILocalFolderApp : IFileStorageApp
	{
		Task<bool> TryWatchShellItem(string shellItemPath, string entityId, string modelId, bool blockNewShellItemsScan);
		//bool TryWatchFolder(string folderPath, string modelId);
		void OnShellItemChanged(string shellItemPath, List<Tuple<string, string, string, bool>> watchedItems);
		void OnShellItemCreated(string shellItemPath, List<Tuple<string, IEntity>> listsToUpdate);
		void OnShellItemDeleted(string shellItemPath, List<Tuple<string, string, string, bool>> watchedItems);
		void OnShellItemRenamed(string newShellItemPath, string oldShellItemPath, List<Tuple<string, string, string, bool>> watchedItems);
		bool StopWatchingShellItem(string shellItemPath, string entityId, string modelId);
		//bool StopWatchingFolder(string folderPath, string modelId);
		bool TryGetSharedFolderAppItemDataContainerFromShellItem(string shellItemPath, IAppAccount account, out SharedFolderAppItemDataContainer container);
		bool IsShellItemRootedInAppRoot(string shellItemPath, out List<IAppAccount> accounts);
	} // interface
} // namespace
