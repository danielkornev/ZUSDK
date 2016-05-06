using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.Semantic.Core
{
	public enum SemanticActionKinds
	{
		#region Internal Operations: 0-49
		[Description("Internal operation was made")]
		None = 0,
		#endregion

		#region Visual Clusters and Zoomable Space: 50-69
		[Description("Removed this item from visual cluster")]
		EntityRemovedFromVisualCluster = 50,
		[Description("Added this item to visual cluster")]
		EntityAddedToVisualCluster = 51,
		[Description("System removed this item from visual cluster")]
		EntityRemovedFromVisualClusterByHealer = 52,
		[Description("Created a visual cluster")]
		VisualClusterAdded = 53,
		[Description("Moved visual cluster in the space")]
		VisualClusterMoved = 54,
		[Description("Deleted a visual cluster")]
		VisualClusterDeleted = 55,
		[Description("Unpinned from a project space")]
		UnpinnedFromVisualSpace = 56,

		#endregion

		#region Entity CRUD Operations: 70-89
		[Description("Created this item")]
		EntityCreated = 70,
		[Description("Changed")]
		EntityChanged = 71,
		[Description("Deleted this item")]
		UserMarkedEntityAsDeleted = 72,
		[Description("Renamed this item")]
		EntityRenamed = 73,
		[Description("Moved in the space")]
		EntityMoved = 74,
		[Description("Changed a property")]
		EntityDynamicPropertyChanged = 75,
		[Description("System assigned to an application account")]
		EntityAssignedToAppAccount = 76,
		[Description("Thumbnail extracted and saved")]
		EntityThumbnailSaved = 77,
		[Description("Description updated")]
		EntityDescriptionUpdated = 78,

		#endregion

		#region Project Operations: 90-149
		[Description("New project was detected")]
		NewProjectDetected = 90,
		[Description("Deleted a project space")]
		ProjectDeleted = 91,
		[Description("Joined project space")]
		ProjectJoined = 92,
		[Description("Left project space")]
		ProjectLeft = 93,
		[Description("Moved an entity within the project space")]
		ChildEntityMoved = 94,
		[Description("Deleted an entity from the project space")]
		ChildEntityDeleted = 95,
		[Description("Added new entity to the project space")]
		ChildEntityAdded = 96,
		[Description("System marked this project space as the last visited")]
		ProjectMarkedAsLastVisited = 97,

		#endregion

		#region User Management: 150-169
		[Description("Entered this project space")]
		UserEnteredSpace = 150,
		[Description("Left this project space")]
		UserLeftSpace = 151,
		[Description("Moved within this project space")]
		UserMovedInSpace = 152,
		#endregion

		#region Linked Data Changes: 170-199
		[Description("Linked file was renamed or moved")]
		LinkedFileMoved = 170,
		[Description("Linked file is no longer available")]
		LinkedFileNoLongerAvailable = 171,
		[Description("Started copying file to local cache or linked app folder")]
		EntityContentLocalCopyInProgress = 172,
		[Description("Finished copying file to local cache or linked app folder")]
		EntityContentLocalCopyFinished = 173,
		[Description("Started uploading of file to linked app remote data source")]
		EntityContentUploadToAppDataSourceInProgress = 174,
		[Description("Finished uploading of file to linked app remote data source")]
		EntityContentUploadToAppDataSourceFinished = 175,
		[Description("Started downloading web page")]
		EntityWebPageStartedDownloading = 180,
		[Description("Finished downloading web page")]
		EntityWebPageDownloadingFinished = 181,
		[Description("Failed to download web page")]
		EntityWebPageDownloadingFailed = 182,
		[Description("Contents changed")]
		LinkedItemContentsModified = 185,
		#endregion

		#region Full-Text Search Indexing: 200-209
		[Description("System made this item ready for full-text search")]
		EntityContentFullTextIndexingSucceeded = 200,
		[Description("System failed to make this item ready for full-text search")]
		EntityContentFullTextIndexingFailed = 201,
		#endregion

		#region List Management: 210-119
		[Description("Added to a list")]
		EntityAddedToList = 210,

		[Description("Added to a project space")]
		EntityAddedToAppSourceBoundList = 211,
		[Description("Removed from a project space")]
		EntityRemovedFromVisualSpaceToAppSourceBoundList = 218,
		[Description("Removed from a list")]
		EntityRemovedFromList = 219,
		#endregion

		#region Linking and Meta Linking: 220-229
		[Description("Linked to Meta entity")]
		EntityLinkedToMetaEntity = 220,
		[Description("Linked to another entity")]
		EntityLinkedToAnotherEntity = 221,
		[Description("Entities extracted and linked")]
		EntitiesExtractedAndLinked = 222,
		[Description("System fixed meta id and re-bound entity to its holding project")]
		EntityReboundToCurrentModelContext = 223,
		[Description("Link to another entity removed")]
		LinkToAnotherEntityRemoved = 224,
		#endregion

		#region Keyphrases Extraction: 230-239
		[Description("Added a keyphrase")]
		EntityKeyphraseAdded = 230,
		[Description("Deleted a keyphrase")]
		EntityKeyphraseMarkedAsDeleted = 231,
		[Description("All keyphrases removed")]
		EntityKeyphrasesCleaned = 232,
		[Description("System updated keyphrases")]
		EntityKeyphrasesUpdated = 233,
		[Description("System extracted keyphrases")]
		EntityKeyphrasesExtracted = 234,
		[Description("System recalculated keyphrases")]
		EntityKeyphrasesRecalculated = 235,

		#endregion

		#region Kinds: 240-249
		[Description("System identified item's kind")]
		EntityKindIdentified = 240,
		#endregion

		#region Same Entities: 250-259
		[Description("Same entity was found")]
		SameEntityResolved = 250,
		[Description("Same person was found")]
		SamePersonResolved = 251,
		#endregion

		#region Entity Semantic Activities: 300-500
		[Description("Opened")]
		EntityOpened = 300,
		[Description("Marked as read")]
		UserMarkedEntityAsRead = 301,
		[Description("Marked as unread")]
		UserMarkedEntityAsUnread = 302,
		[Description("Edited")]
		AnotherUserMarkedEntityAsUnread = 303,
		[Description("System marked it as unread")]
		SystemMarkedEntityAsUnread = 304,
		[Description("Cleared color status")]
		EntityReviewStatusCleared = 310,
		[Description("Marked as \"in progress\"")]
		EntityReviewStatusSetAsInProgress = 311,
		[Description("Marked as \"action required\"")]
		EntityReviewStatusSetAsActionRequired = 312,
		[Description("Marked as \"done\"")]
		EntityReviewStatusSetAsDone = 313,
		[Description("Flagged as important")]
		UserMarkedEntityAsImportant = 314,
		[Description("Flagged as not important")]
		UserMarkedEntityAsNotImportant = 315,
		[Description("Resized")]
		EntityTemplateResized = 316,
		[Description("Changed privacy status")]
		EntityIsPrivatePropertyChanged =317

		#endregion




	} // enum
} // namespace
