using System;
using ZU.Semantic.Core;
namespace ZU.Storage
{
	public interface IStorageContext
	{
		void DoneWatcher();
		void InitWatcher();
		bool Load();
		ZU.Core.IModel Model { get; set; } // but set shall be private
		void Purge(ZU.Core.IEntity entity, string dataFile);
		void SaveEntity(ZU.Core.IEntity entity, SemanticActionKinds semanticAction, bool force);
		IStorageProvider StorageProvider { get; set;  } // but set shall be private

		//void StopAfterAllJobsProcessed();
	}
}
