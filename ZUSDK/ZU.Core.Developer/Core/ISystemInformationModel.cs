using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using ZU.Configuration.Settings;
using ZU.Core.Apps;
using ZU.Semantic;
using ZU.Semantic.Core;
using ZU.Semantic.Keyphrases;
using ZU.Semantic.Processors;
using ZU.WPF.Data.Schemas;
namespace ZU.Core
{
	public partial interface ISystemInformationModel
	{
		#region Properties
		/// <summary>
		/// Sets or gets boot time of an app hosting this ISystemInformationModel implementation's instance.
		/// </summary>
		double BootTime { get; set; }
		/// <summary>
		/// Gets calculated FPS (frame-per-second) stats.
		/// </summary>
		double FPS { get; }
		/// <summary>
		/// Sets or gets a WPF Dispatcher necessary for async operations.
		/// </summary>
		System.Windows.Threading.Dispatcher Dispatcher { get; set; }
		/// <summary>
		/// Gets hosting application's Configuration Settings.
		/// </summary>
		ZU.Configuration.Settings.IConfigurationSettings ConfigurationSettings { get; set; }
		/// <summary>
		/// Gets local file system path to Full-Text Index cache.
		/// </summary>
		string FullTextIndexCache { get; }
		/// <summary>
		/// Gets initialization state.
		/// </summary>
		bool IsInitialized { get; }
		/// <summary>
		/// Sets or gets an owner for this ISystemInformationModel implementation's instance.
		/// </summary>
		dynamic Owner { get; set; }
		/// <summary>
		/// Sets or gets Root Model (Metaspace).
		/// </summary>
		IModel RootModel { get; set; }

		List<AppEntityKindDefinition> AppEntityKindDefinitions
		{
			get;
		}
		#endregion

		#region Methods
		///// <summary>
		///// Gets Root Model (Metaspace) configuration.
		///// </summary>
		///// <returns></returns>
		//RootModelConfiguration GetRootModelConfig();
		#endregion


		//Action<object, RoutedEventArgs, Entity, bool> DoEntityOpen;

		void EntityOpen(IEntity entity);


		event System.ComponentModel.PropertyChangedEventHandler SystemPropertyChanged;
		
		void Dispose();

		bool IsFileExtensionNonIndexable(string fileFullPath);


		IEntity GetEntityOfSpace(string spaceEntityId);
		System.Collections.Generic.List<IEntity> GetReferencedEntities(IEntity entity);
		System.Collections.Generic.List<IEntity> GetReferencingEntities(IEntity entity, bool currentModel);
		System.Collections.Generic.List<System.Collections.Generic.IEnumerable<object>> GetStats();
		void InitializeAppManager();
		void InitializeSemanticProcessor();
		
		void PerformCleanup();
		
	
		
		ISemanticPipelineProcessor SemanticPipelineProcessor { get; }
		
		//Task<bool> TrySaveAppSettings(IAppSettings settings);



		void UpdateHighFreqKeyphrasesForVisualCluster(IEntity entity);

		

		bool FileShouldBeProcessed(string file);

		string GetEntityKindDiplayName(string entityKind);

		string GetUserEntityTitle(Semantic.EntityRef entityRef);
		/// <summary>
		/// Returns Unknown Person
		/// </summary>
		/// <returns></returns>
		IEntity GetUnknownPerson();
		/// <summary>
		/// Returns Person for Zet Universe (analog of NT AUTHORITY\SYSTEM in Windows NT)
		/// </summary>
		/// <returns></returns>
		IEntity GetZetUniversePerson();
		/// <summary>
		/// Returns current user's entity
		/// </summary>
		/// <returns></returns>
		IEntity GetCurrentUserEntity();
		bool TryResolve(string entityId, string modelId, out IEntity result);
		bool TryResolve(ZU.Semantic.EntityRef reference, out IEntity result);
		bool TryResolve(string partialId, out IEntity person);

		IEntityView GetEntityViewFromActivityHistoryItem(IUserAction ah, IEntity parentEntity, bool skipZetUniverse = false);

		List<IEntityView> GetRelatedPeopleAsEntityViews(IEntity entity);

		List<IEntityView> GetEntitiesLinkedWithAsEntityViews(IEntity entity);

		IEntityView GetEntityViewFromEntityRef(Semantic.EntityRef entityRef);

		IEntityView GetEntityViewFromEntity(IEntity entity);

	    bool ContainsLinkToEntity(IEntity entity, string relationshipKind, string agendId, EntityFragmentState relationshipState);


        int GetChildrenEntitiesCount(IEntity entity);

		bool IsEntityContainer(IEntity entity, out ContainerKinds containerKind);
		
		string GetAccountName(string accountId);

		bool IsEntityLinkingToExternalItem(IEntity entity, out string uri);

		void GetEntitiesLinkedWithAsEntityViewsAsync(IEntity entity, IProgress<Tuple<IEntityView, bool>> progress);

		string GetValidSpacePath(string parentFolder, string proposedChildName);


		bool TryUpdateFullTextIndex(IEntity entity, bool stepByStep);



		ZU.Core.Tasks.ITaskSchedulerFactory GetTaskSchedulerFactory();

		IEntity CreateEntity(IModel model);

		IKeyphrase CreateKeyphrase(string keyphrase, double rank);

		IKeyphrase CreateKeyphrase(string keyphrase, double rank, int docFreq, EntityFragmentState entityFragmentState);

		bool CanBePartOfVisualCluster(ZU.Core.IEntity entity);

		bool CanParticipateInTopographicClusterVisualization(IEntity entity);

		string IdToShortString(EntityRef UID);

	} // interface
} // namespace
