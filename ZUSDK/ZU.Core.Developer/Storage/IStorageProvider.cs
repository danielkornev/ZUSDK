using System;
using System.Collections.Generic;
using System.IO;
using ZU.Core;
using ZU.Semantic;
using ZU.Semantic.Core;
using ZU.Storage.Migration;

namespace ZU.Storage
{
	public interface IStorageProvider
	{
		bool Load();
		void Save(); // saves data
		void Close();

		void SaveEntity(ZU.Core.IEntity entity, SemanticActionKinds semanticAction, bool force);
		
		#region Folder-Based Storage Providers
		bool IsFolderBased { get; }
		bool Exists { get; }
		string StorePath { get; }
		#endregion
		
		IStorageContext Context { get; set; }

		void Purge(IEntity entity, string dataFile);

		IEntity LoadEntityByKey(string storageKey);

		string LoadEntityByKeyAsJson(string storageKey);

		void LoadEntitiesByKeys(List<string> entitiesStorageKeys);

		#region Migration
		List<Tuple<string, string>> GetAllStorageRecords(IMigrationProvider migrationProv, string spaceName, ModelKind spaceKind, out bool succeeded);
		void ImportStorageRecords(List<Tuple<string, string>> storageRecords, IMigrationProvider migrationProv, string spaceName, ModelKind spaceKind, out bool succeeded);

		void UpdateUIdForEntity(IEntity entity, EntityRef uid);
		#endregion
	} // interface
} // namespace
