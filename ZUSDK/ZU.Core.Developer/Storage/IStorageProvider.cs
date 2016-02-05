using System;
using System.Collections.Generic;
using System.IO;
using ZU.Core;
using ZU.Semantic.Core;
namespace ZU.Storage
{
	public interface IStorageProvider
	{
		bool Load();
		void SaveEntity(ZU.Core.IEntity entity, SemanticActionKinds semanticAction, bool force);


	

		#region Folder-Based Storage Providers
		bool IsFolderBased { get; }
		string StorePath { get; }
		#endregion

		ModelKinds ModelKind { get; }

		string SpaceId { get; }

		IStorageContext Context { get; set; }

		void Purge(IEntity entity, string dataFile);

		IEntity LoadEntityByKey(string storageKey);

		void LoadEntitiesByKeys(List<string> entitiesStorageKeys);
	} // interface
} // namespace
