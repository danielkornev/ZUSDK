using System;
using System.Collections.Generic;
using System.ComponentModel;
using ZU.Configuration.Settings;
using ZU.Core.Apps;
using ZU.Semantic;
using ZU.Semantic.Core;
using ZU.Semantic.Spatial.Collections;
using ZU.WPF.Data.Schemas;
namespace ZU.Core
{
	/// <summary>
	/// IModel is an object representation of the Project/App/Metaspaces.
	/// </summary>
	public interface IModel
	{
		/// <summary>
		/// Adds an IEntity to the IEntity Graph.
		/// </summary>
		/// <param name="IEntity"></param>
		void Add(IEntity IEntity);

		/// <summary>
		/// Adds and IEntity to another one as Related.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="target"></param>
		/// <param name="relation"></param>
		/// <param name="agentId"></param>
		/// <param name="force"></param>
		/// <param name="isExtracted"></param>
		/// <param name="confidence"></param>
		/// <returns></returns>
		bool AddRelatedEntity(IEntity source, IEntity target, string relation, string agentId, bool force, bool isExtracted, double confidence);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="filePath">If relative path is specified, accountRootFolder should be specified, as well. Otherwise, if absolute path is specified, accountRootFolder should be empty or null.</param>
		/// <param name="title"></param>
		/// <param name="accountRootFolder">Should be null or empty if file path is absolute. Otherwise, it should point to the account root folder.</param>
		/// <param name="account">Application account</param>
		/// <param name="pos">Destination position</param>
		/// <param name="dispatcher">Application dispatcher</param>
		/// <param name="listIEntity">List IEntity</param>
		/// <param name="isToBeIncludedIntoList">True if item should be included into the list, otherwise false.</param>
		/// <param name="fileNotYetExists">Specify if this file doesn't exist, yet</param>
		/// <param name="isNotInSpace">True if item shall not be shown in the space, otherwise false.</param>
		/// <returns></returns>
		IEntity AddEntityLinkToFile(string filePath, string title, string accountRootFolder, IAppAccount account, System.Windows.Rect pos, System.Windows.Threading.Dispatcher dispatcher, IEntity listIEntity, bool isToBeIncludedIntoList, bool fileNotYetExists = false, bool isNotInSpace = true, bool runInUI = false);
		string BaseFolder { get; }
		bool CanShareFolder(string fullPath);
		void Clear();
		void ClearSearchResultsHighlight();
		IEntity CreateList(string title, string listIEntityKind, System.Windows.Rect position, DateTime utcNow, System.Collections.Generic.List<IEntity> entities, string listId);
		IEntity CreateAppSourceBoundList(string title, string listIEntityKind, System.Windows.Rect position, DateTime utcNow, List<IEntity> entities, string listIEntityId, string appId);
		System.Collections.ObjectModel.ObservableCollection<IEntity> Deleted { get; set; }
		DateTime DTLastSave { get; set; }
		void Done();
		void DoneProvider();
		ISpatialRangeObservableCollection<IEntity> Entities { get; set; }
		System.Collections.Generic.List<IEntity> GetEntitiesAt(System.Windows.Point position);
		System.Collections.Generic.List<IEntity> GetEntitiesFromList(IEntity IEntityList);
		string GetFullModelPath(string path);
		string GetSyncRootFolder();
		System.Collections.Generic.List<IEntity> HighlihgtedEntites { get; set; }
		string Id { get; }
		void Init();
		void InitProvider();
		bool IsFolderShared(string fullPath);
		bool IsPartOfList(IEntity IEntity);
		bool IsPartOfAppSourceBoundList(IEntity IEntity);
		bool IsRoot { get; set; }
		bool IsSaveNeeded(IEntity ent);
		bool IsSyncFolder(string fullPath);
		System.Collections.ObjectModel.ObservableCollection<IEntity> KnownEntities { get; }
		System.Collections.ObjectModel.ObservableCollection<IEntityList> Lists { get; set; }
		System.Collections.ObjectModel.ObservableCollection<IAppSourceBoundList> AppSourceBoundLists { get; set; }
		void Load(ZU.Storage.IStorageProvider storageProvider);
		bool LoadEntityJson(string sJson, bool enableNotifications);
		double Lod { get; set; }
		string Message { get; set; }
		IEntity MessageEntity { get; set; }


		event ZU.Core.ModelEvents.OnEntityAddedEventHandler OnEntityAdded; 
		event ZU.Core.ModelEvents.OnEntityDeletedEventHandler OnEntityDeleted;
		event ZU.Core.ModelEvents.OnEntityDynamicPropertyChangedEventHandler OnEntityDynamicPropertyChanged;
		event ZU.Core.ModelEvents.OnEntityPositionChangedEventHandler OnEntityPositionChanged;
		event ZU.Core.ModelEvents.OnEntityPropertyChangedEventHandler OnEntityPropertyChanged;

		//Action<IEntity, VisualCluster> IEntityAddedToVisualCluster;
		//Action<IEntity, VisualCluster> IEntityRemovedFromVisualCluster;

		void NotifyEntityAddedToVisualCluster(IEntity IEntity, VisualCluster vc);
		void NotifyEntityRemovedFromVisualCluster(IEntity IEntity, VisualCluster vc);

		
		void OnEntityChange(object sender, System.ComponentModel.PropertyChangedEventArgs ea);
		
		void OnEntityDynamicPropChanged(IEntity IEntity, PropertyChangedEventArgs e);
		
		void PerformCleanup();
		
		void Purge(IEntity IEntity);
		void Purge(IEntity IEntity, string dataFile);
		void Remove(IEntity IEntity, bool useMessage = true);
		void Save(bool force = false);
		void SaveEntity(IEntity IEntity, SemanticActionKinds semanticAction, bool force = false);
		void ShareFolder(string fullPath);
		ISystemInformationModel SIM { get; set; }
		IEntity SpaceEntity { get; set; }
		ZU.Storage.IStorageContext StorageContext { get; }
		string Title { get; set; }
		bool TryAddEntitiesToList(IEntity IEntityList, System.Collections.Generic.List<IEntity> entitiesToAdd);
		bool TryAddEntityToList(IEntity IEntityList, IEntity IEntityToAdd);
		bool TryAddEntitiesToAppSourceBoundList(IEntity IEntityList, System.Collections.Generic.List<IEntity> entitiesToAdd, bool isNotInSpace = true);
		bool TryAddEntityToAppSourceBoundList(IEntity IEntityList, IEntity IEntityToAdd, bool isNotInSpace = true);
		bool TryBuildVisualClustersIndex();
		bool TryGetEntitiesForUri(string uri, out System.Collections.Generic.List<IEntity> entities);
		bool TryGetVisualCluster(IEntity IEntity, out VisualCluster vc);
		bool TryHighlightEntityAsActivity(IEntity IEntity, bool value);
		bool TryHighlightEntityAsSortResult(IEntity IEntity, bool value);
		bool TryReadVisualClustersIndex();
		bool TryRemoveEntityFromList(IEntity IEntityList, IEntity IEntityToRemove);
		bool TryRemoveEntityFromAppSourceBoundList(IEntity IEntityList, IEntity IEntityToRemove);
		bool TrySetEntityAsSearchResult(IEntity IEntity, bool value);
		bool TryUpdateSpatialIndex(IEntity newVersion);
		bool TryUpdateVisualClustersIndex(IEntity IEntity);
		System.Collections.ObjectModel.ObservableCollection<VisualCluster> VisualClusters { get; }
		ZU.Semantic.Spatial.Indexing.IVisualClustersIndexer VisualClustersIndexer { get; set; }

		EntityRef UID { get; set; }

		/// <summary>
		/// LookupIdIEntity.ContainsKey
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		bool ContainsEntityWithId(string id);

		IEntity GetEntityById(string id);

		bool TryResolve(string IEntityId, out IEntity IEntity);

		bool ContainsKeyInLookupUriEntity(string sourceFileOrDirectory);

		/// <summary>
		/// LookupIdIEntity.TryGetValue(string id, out IEntity oldVersion)
		/// </summary>
		/// <param name="id"></param>
		/// <param name="oldVersion"></param>
		/// <returns></returns>
		bool TryGetValue(string id, out IEntity oldVersion);

		List<IUserAction> GetActivityHistory(IEntity IEntity);

		IEntity GetEntityStorageRecordAt(IUserAction activityWithPreviousCluster);

		IEntity LoadEntityJson(string sJson);

		ModelKind ModelKind { get; }

		bool IsAppSpace { get; }

		IEntityView AsEntityView { get; }

		double UnreadEntitiesCount { get; }

		void UpdateUnreadIndex(IEntity IEntity);
	} // interface
} // namespace