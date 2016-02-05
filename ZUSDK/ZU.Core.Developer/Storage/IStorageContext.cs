using System;
using System.Collections.Generic;
using ZU.Core;
using ZU.Semantic.Core;
namespace ZU.Storage
{
	public interface IStorageContext
	{
		/// <summary>
		/// Spatio-Temporal Index
		/// </summary>
		void InitIndex();

		bool Load();
		ZU.Core.IModel Model { get; set; } // but set shall be private
		void Purge(ZU.Core.IEntity entity, string dataFile);
		void SaveEntity(ZU.Core.IEntity entity, SemanticActionKinds semanticAction, bool force);
		IStorageProvider StorageProvider { get; set;  } // but set shall be private
		void AddStorageRecordKeyToIndex(string recordKey, StorageRecordKinds recordKind);
		/// <summary>
		/// Materializes and attaches an IEntity object from a given JSON document to an IModel
		/// </summary>
		/// <param name="entityJson"></param>
		/// <param name="enableNotifications"></param>
		/// <returns></returns>
		bool AddEntity(string entityJson, bool enableNotifications);

		/// <summary>
		/// Materializes an IEntity object from a given JSON document
		/// </summary>
		/// <param name="entityJson"></param>
		/// <returns></returns>
		IEntity LoadEntity(string entityJson);
		//void StopAfterAllJobsProcessed();


		System.Collections.Generic.List<IUserAction> GetActivityHistory(string entityId);

		
		IEntity GetEntityStorageRecordAt(IUserAction activityWithPreviousCluster);

	} // interface
} // namespace
