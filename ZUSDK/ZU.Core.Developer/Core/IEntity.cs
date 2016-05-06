using System;
using System.Collections.Generic;
using System.ComponentModel;
using ZU.Semantic;

namespace ZU.Core
{
	public interface IEntity : INotifyPropertyChanged, ITimelined
	{
		#region Basic Properties
		string AppId { get; set; }
		string Id { get; set; }
		string Kind { get; set; }
		
		string Title { get; set; }

		/// <summary>
		/// This is a way too useful property to avoid having it as the default property
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// Default URI
		/// </summary>
		string Uri { get; set; }

		/// <summary>
		/// List of related URIs
		/// </summary>
		List<string> Uris { get; set; }

		ZU.Semantic.EntityRef MetaId { get; }
		string SystemWideUniqueId { get; }

		IModel ModelContext { get; set; }
		bool IsReadOnly { get; }
		
		#endregion

		#region Time-Based Properties
		void SetDateTime(DateTime effectiveTime);
		/// <summary>
		/// Represents modification date
		/// </summary>
		DateTime TLChange { get; set; }
		/// <summary>
		/// A public property used for setting a time change
		/// </summary>
		DateTime LastModified { get; set; }

		/// <summary>
		/// Used for a Linked Entity (doesn't represent the storage record, but represents the concept's created date)
		/// </summary>
		DateTime Created { get; set; }
		
		#endregion

		#region Linked Items Operations
		bool IsLinkedItemNoLongerAvailable { get; set; }
		ZU.Semantic.Core.LinkedItemNetworkOperationStatus LinkedItemNetworkOperationStatus { get; set; }
		//DateTime LinkedItemTLChange { get; set; }
		#endregion


		#region CRUD Methods
		IEntity AddTo(IModel model);
		IEntity DeepClone();

		/// <summary>
		/// Marks given item as deleted
		/// </summary>
		void Delete();

		/// <summary>
		/// Marks given item as unpinned. 
		/// Special Case: If it is a visual cluster, it's marked as deleted, as well.
		/// </summary>
		void Unpin();
		/// <summary>
		/// Refreshes metadata
		/// </summary>
		/// <returns></returns>
		bool Refresh();
		void SaveChanges(ZU.Semantic.Core.SemanticActionKinds semanticAction);

		void Merge(ITimelined newVer);
		/// <summary>
		/// Represents semantic action for the current version of the entity (semantic action applied to this entity at this time)
		/// </summary>
		ZU.Semantic.Core.SemanticActionKinds SemanticAction { get; set; }
		#endregion

		#region Thumbnails
		object LargeThumbnail { get; }
		object Thumbnail { get; }
		string ThumbnailUri { get; set; }
		#endregion

		#region Spatial Properties
		int ZIndex { get; set; }
		int ZLod { get; set; }
		System.Windows.Rect ZPos { get; set; }
		/// <summary>
		/// Session-specific property used by the ISpatialItemsSource collection
		/// </summary>
		int Index { get; set; }
		#endregion

		#region Topic/Space Semantic View Properties: Red/Yellow Count, Read/Unread Count
		
		double RedCount { get; set; }
		double YellowCount { get; set; }
		/// <summary>
		/// Includes number of all included unread items (not just docs anymore)
		/// </summary>
		double UnreadDocumentsCount { get; set; }
		/// <summary>
		/// TBD
		/// </summary>
		double UnreadEmailCount { get; set; }
		#endregion

		#region Visual Indexing Properties
		/// <summary>
		/// If yes, then entity can participate in a visual cluster.
		/// </summary>
		bool CanBePartOfVisualCluster { get; }
		/// <summary>
		/// If yes, then entity can be drawn with topographic cluster visualization around it
		/// </summary>
		bool CanParticipateInTopographicClusterVisualization { get; }
		/// <summary>
		/// Visual Cluster's Id
		/// </summary>
		string ClusterId { get; set; }
		bool IsCluster { get; }
		/// <summary>
		/// Session-specific property used for Visual Clusters indexing
		/// </summary>
		double Distance { get; set; }

		#endregion

		#region IComparable & Equality
		int CompareTo(object obj);
		int GetHashCode();
		
		bool Equals(object obj);
		#endregion

		#region Lists
		bool IsList { get; set; }
		IEntity HoldingList { get; set; }
		string HoldingListId { get; set; }
		bool IsPartOfList { get; set; }
		#endregion

		#region App Source Bound Lists
		string AppSourceEntityListId { get; set; }
		bool AppSourceEntityListIdSpecified { get; }

		System.Collections.Generic.Dictionary<string, string> DefaultAppSourceBoundListsIds { get; set; }
		bool DefaultAppSourceBoundListsIdsSpecified { get; }
		IEntity HoldingAppSourceBoundList { get; set; }
		string HoldingAppSourceBoundListId { get; set; }
		bool IsAppSourceBoundList { get; set; }
		bool IsPartOfAppSourceBoundList { get; set; }
		bool ShouldSerializeAppSourceEntityListId();
		bool ShouldSerializeDefaultAppSourceBoundListsIds();


		#endregion

		#region Entity Property System
		void DoneProps();
		void DonePropsChanged();
		T GetProp<T>(string propName, T defaultValue = default(T));
		void InitDynamicProperties();
		IEntity InitProps();
		void InitPropsChanged();
		void InstantiateProperty(string propertyName, System.Globalization.CultureInfo culture, System.Threading.SynchronizationContext callbackExecutionContext);
		
		System.Collections.ObjectModel.ObservableCollection<Property> Properties { get; set; }
		dynamic Props { get; set; }
		System.Collections.Generic.IDictionary<string, object> PropsDict { get; set; }
		bool IsChanged { get; set; }
		void Changed(string propertyName);

		event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Entity Ref-related properties & methods
		ZU.Semantic.EntityRef ToEntityRef();
		ZU.Semantic.EntityRef ToEntityRef(string relationship, ZU.Semantic.RelationDirection relationDirection, double confidence, ZU.Semantic.Core.EntityFragmentState state);
		#endregion


		#region File Items-specific Properties
		string FullPath { get; set; }
		string Checksum { get; set; }
		#endregion

		#region Full-Text Indexing Properties
		/// <summary>
		/// This is a trigger property
		/// </summary>
		bool IsFTIndexingRequired { get; set; }
		/// <summary>
		/// Returns true if this entity is full-text indexable
		/// </summary>
		bool IsFullTextIndexable { get; }
		/// <summary>
		/// Contains temporary full-text data (TBD)
		/// </summary>
		string FullText { get; set; }
		#endregion

		#region Calendar Items-specific Properties (TBD)
		DateTime CalendarEntryDate { get; set; }
		int EventDay { get; }
		string EventMonth { get; }

		int EventYear { get; }
		//bool ShouldSerializeEventData();
		#endregion

		#region Related
		bool AddRelatedEntity(IEntity entity, string relation, string agentId, bool force = false, bool isExtracted = false, double confidence = 1.0);
		ZU.Collections.ObjectModel.IRangeObservableCollection<ZU.Semantic.EntityRef> Related { get; set; }
		bool RelatedMessagesAvailable { get; }
		int RelatedMessagesCount { get; set; }

		#endregion

		#region Semantic Properties
		/// <summary>
		/// Important/not important status
		/// </summary>
		bool IsTaggedAsImportant { get; set; }
		/// <summary>
		/// Read/Unread status
		/// </summary>
		bool UnreadStatus { get; set; }
		/// <summary>
		/// Color status
		/// </summary>
		ZU.Core.Structured.ReviewStatus ReviewStatus { get; set; }
		
		#endregion

		#region Keyphrases
		ZU.Collections.ObjectModel.IRangeObservableCollection<ZU.Semantic.Keyphrases.IKeyphrase> Keyphrases { get; set; }
		System.Collections.Generic.List<ZU.Semantic.Keyphrases.IKeyphrase> NewKeyphrases { get; set; }

		#endregion

		#region Visibility Properties
		bool IsHidden { get; }
		bool IsLive { get; set; }
		bool IsNotInSpace { get; set; }
		/// <summary>
		/// Temporary property (TBD)
		/// </summary>
		bool ShowInSpace { get; set; }
		bool IsRenderable { get; }
		
		#endregion

		#region Highlights
		bool IsActivityHighlighted { get; set; }
		bool IsSearchResult { get; set; }
		#endregion

		#region UI Action Properties
		bool IsBeingResized { get; set; }
		bool IsStylusInputInProgress { get; set; }

		#endregion

		#region Entity View Properties
		ZU.WPF.Data.Schemas.IEntityView AsEntityView { get; }
		ZU.WPF.Data.Schemas.IEntityView LastModifiedBy { get; }
	
		#endregion

		#region User-Related Properties
		bool IsCurrentUser();

		bool IsUserIdentifactor();

		#endregion

		#region Activity History
		System.Collections.Generic.List<IUserAction> GetActivityHistory();
		#endregion

		#region UI Helper Properties
		IEntity EntitySelf { get; set; }
		object Tag { get; set; }
		#endregion

		#region Property Overrides
		string ToString();
		#endregion

		#region Search-Related Properties
		/// <summary>
		/// Temporary property. Represents a score for a given entity based on the search query it's surfaced as the result of.
		/// </summary>
		double Score { get; set; }
		#endregion

		#region Consider To Remove

		bool IsKnown { get; }
		#endregion










		
		
		
		
	
		


	} // class
} // namespace
