using System;
using System.IO;
using ZU.Core;
using ZU.Semantic.Core;
namespace ZU.Storage
{
	public interface IStorageProvider
	{
		bool Load();
		void SaveEntity(ZU.Core.IEntity entity, SemanticActionKinds semanticAction, bool force);


		#region Watcher
		void InitWatcher();
		void DoneWatcher();
		void OnWatcherChanged(object sender, FileSystemEventArgs ea);
		void OnWatcherRenamed(object sender, RenamedEventArgs ea);
		#endregion

		#region Folder-Based Storage Providers
		bool IsFolderBased { get; }
		string FolderPath { get; }
		#endregion

		ModelKinds ModelKind { get; }

		string SpaceId { get; }

		IStorageContext Context { get; set; }

		void Purge(IEntity entity, string dataFile);

		System.Collections.Generic.List<IUserAction> GetActivityHistory(string entityId);

		IEntity GetEntityStorageRecordAt(IUserAction activityWithPreviousCluster);
	} // interface
} // namespace
