using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using ZU.Core;
using ZU.Semantic;
using ZU.Semantic.Core;
using ZU.Semantic.Text;

namespace ZU
{
	/// <summary>
	/// This class is used to keep all constant values used through the entire platform, including but not limited to kinds and properties normalized names, system components names, URIs, and so on.
	/// </summary>
	public static partial class Constants
	{
		/// <summary>
		/// List of the built-in Kinds representing everyday things like people, documents, e-mails, media, and so on.
		/// </summary>
		public static partial class Kinds
		{
			[Description("Agent")]
			[KindMetadata("Agent", "Agent", Constants.Kinds.Kind, false, true)]
			public const string Agent = "ZetUniverse.Kinds.Agent";

			[Description("Application")]
			[KindMetadata("Application", "Application", Constants.Kinds.ZetUniverse, true)]
			public const string Application = "ZetUniverse.Kinds.System.Application";

			[Description("Appointment")]
			[KindMetadata("Appointment Calendar Event", "Appointment", Constants.Kinds.Calendar, false)]
			public const string Appointment = "ZetUniverse.Kinds.Calendar.Appointment";

			/// <summary>
			/// App Space is an organizing concept designed for keeping app-specific entities separated from other apps.
			/// </summary>
			[Description("App Space")]
			[KindMetadata("App Space", "App Space", Constants.Kinds.ProjectSpace, true, false)]
			public const string AppSpace = "ZetUniverse.Kinds.AppSpace";

			[Description("Article")]
			[KindMetadata("Article Media", "Article", Constants.Kinds.Document)]
			public const string Article = "ZetUniverse.Kinds.Documents.Article";

			[Description("Author")]
			[KindMetadata("Author Writer Creator People", "Author", Constants.Kinds.Person)]
			public const string Author = "ZetUniverse.Kinds.People.Author";

			[Description("Book")]
			[KindMetadata("Book Media", "Book", Constants.Kinds.Document)]
			public const string Book = "ZetUniverse.Kinds.Documents.Book";

			[Description("Call")]
			[KindMetadata("Call Communication", "Call", Constants.Kinds.Communication)]
			public const string Call = "ZetUniverse.Kinds.Communications.Call";

			[Description("Calendar Entry")]
			[KindMetadata("Calendar Entry", "Calendar Entry", Constants.Kinds.Kind, false, true)]
			public const string Calendar = "ZetUniverse.Kinds.Calendar";

			[Description("Communication")]
			[KindMetadata("Communication", "Communication", Constants.Kinds.Kind, false, true)]
			public const string Communication = "ZetUniverse.Kinds.Communication";

			/// <summary>
			/// Strange, but Document is a Media, actually.
			/// </summary>
			[Description("Document")]
			[KindMetadata("Document", "Document", Constants.Kinds.Media)]
			public const string Document = "ZetUniverse.Kinds.Documents.Document";

			[Description("Email")]
			[KindMetadata("Email Communication E-mail", "Email", Constants.Kinds.Communication)]
			public const string Email = "ZetUniverse.Kinds.Communications.Email";


			[Description("Folder")]
			[KindMetadata("File Folder Directory", "File Folder", Constants.Kinds.Kind, false, true)]
			public const string Folder = "ZetUniverse.Kinds.Folder";

			[Description("Generic File")]
			[KindMetadata("Generic File GenericFile", "Generic File", Constants.Kinds.Kind)]
			public const string GenericFile = "ZetUniverse.Kinds.File";

			[Description("Graph View")]
			[KindMetadata("GraphView Graph View", "Graph View", Constants.Kinds.View)]
			public const string GraphView = "ZetUniverse.Kinds.Views.GraphView";

			[Description("Instant Message")]
			[KindMetadata("Instant Message IM InstantMessage Communication", "Instant Message", Constants.Kinds.Communication)]
			public const string InstantMessage = "ZetUniverse.Kinds.Communications.InstantMessage";

			[Description("Kind")]
			[KindMetadata("Kind", "Kind", Constants.Kinds.Thing, false, true)]
			public const string Kind = "ZetUniverse.Kinds.Kind";


			/// <summary>
			/// Link is the basis of the relationship management and semantic structuring within Zet Universe.
			/// </summary>
			[Description("Link")]
			[KindMetadata("Link Relationship Relation", "Link", Constants.Kinds.Property)]
			public const string Link = "ZetUniverse.Kinds.Link";
			
			/// <summary>
			/// List is a fundamental organizing and grouping concept in Zet Universe. List represents a 1D group of things where order of things is not explicitely defined.
			/// </summary>
			[Description("List")]
			[KindMetadata("List", "List", Constants.Kinds.Kind)]
			public const string List = "ZetUniverse.Kinds.List";

			[Description("Location")]
			[KindMetadata("Location", "Location", Constants.Kinds.Kind, true, false)]
			public const string Location = "ZetUniverse.Kinds.Locations.Location";

			/// <summary>
			/// 
			/// </summary>
			[Description("Meeting")]
			[KindMetadata("Meeting Calendar Event", "Meeting", Constants.Kinds.Calendar)]
			public const string Meeting = "ZetUniverse.Kinds.Calendar.Meeting";

			[Description("Media")]
			[KindMetadata("Media", "Media", Constants.Kinds.Kind, false, true)]
			public const string Media = "ZetUniverse.Kinds.Media";

			/// <summary>
			/// This is the class of motion pictures created by recording using cameras or by creating images using animation or other special techniques.
			/// </summary>
			[Description("Movie")]
			[KindMetadata("Movie Film Video Media", "Movie", Constants.Kinds.Video)]
			public const string Movie = "ZetUniverse.Kinds.Media.Video.Movie";
			
			/// <summary>
			/// The class of artifacts that are closely related to music. Musical artifacts include compositions themselves and related artifacts such as cover art on albums, written scores, lyrics, and other things.
			/// </summary>
			[Description("Music")]
			[KindMetadata("Music Media", "Music", Constants.Kinds.Media)]
			public const string Music = "ZetUniverse.Kinds.Media.Music";
			
			/// <summary>
			/// This is the class of musical playlists that may be created by Zet Universe users. A Playlist is composed of a number of musical works. A Playlist is a kind of List.
			/// </summary>
			[Description("Music Playlist")]
			[KindMetadata("MusicalPlaylist Musical Playlist MusicPlaylist Music Playlist", "Music Playlist", Constants.Kinds.Music)]
			public const string MusicalPlaylist = "ZetUniverse.Kinds.Media.Music.MusicalPlaylist";

			[Description("Organization")]
			[KindMetadata("Organization Org", "Organization", Constants.Kinds.Agent)]
			public const string Organization = "ZetUniverse.Kinds.Organization";

			[Description("PDF Document")]
			[KindMetadata("PDF Document PDFDocument PDF Media", "PDF Document", Constants.Kinds.Document)]
			public const string PdfDocument = "ZetUniverse.Kinds.Documents.PdfDocument";

			[Description("Person")]
			[KindMetadata("Person Persona People", "Person", Constants.Kinds.Agent, true, false)]
			public const string Person = "ZetUniverse.Kinds.People.Person";

			/// <summary>
			/// Generic list of people organized into a group. 
			/// </summary>
			[Description("People Group")]
			[KindMetadata("People Group Group", "People Group", Constants.Kinds.Agent)]
			public const string PeopleGroup = "ZetUniverse.Kinds.People.Group";

			[Description("Picture")]
			[KindMetadata("Picture Image Photo Media", "Picture", Constants.Kinds.Media)]
			public const string Picture = "ZetUniverse.Kinds.Media.Picture";

			[Description("Place")]
			[KindMetadata("Place Geo Location", "Place", Constants.Kinds.Location, true, false)]
			public const string Place = "ZetUniverse.Kinds.Locations.Place";

			[Description("PowerPoint Presentation")]
			[KindMetadata("PowerPoint Presentation Slidedeck Slides Document Media", "PowerPoint Presentation", Constants.Kinds.Document)]
			public const string PptxDocument = "ZetUniverse.Kinds.Documents.PptxDocument";

			[Description("Property")]
			[KindMetadata("Property", "Property", "rdfs:Resource", true, true)]
			public const string Property = "owl:Property";

			/// <summary>
			/// (Project) Space is a fundamental organizing and grouping concept in Zet Universe. It contains individual entities grouped into visual clusters, or into lists. Every single entity always belongs to one or more visual clusters.
			/// </summary>
			[Description("Project")]
			[KindMetadata("Project Space", "Project", Constants.Kinds.Kind, true, true)]
			public const string ProjectSpace = "ZetUniverse.Kinds.Space";
			
			/// <summary>
			/// Recorded TVs are a special kind of entities that represent TVs recorded using Windows Media Center. Recorded TV is a kind of Video.
			/// </summary>
			[Description("Recorded TV")]
			[KindMetadata("Recorded TV RecordedTV Video Media", "Recorded TV", Constants.Kinds.Video)]
			public const string RecordedTV = "ZetUniverse.Kinds.Media.Video.RecordedTV";
			//[Description("Sticky Note")]
			//public const string StickyNote = "ZetUniverse.Kinds.Notes.StickyNote";

			[Description("Thing")]
			[KindMetadata("Thing", "Thing", "rdfs:Resource", true, true)]
			public const string Thing = "owl:Thing";
			
			[Description("User")]
			[KindMetadata("User", "User", Constants.Kinds.Agent, true, false)]
			public const string User = "ZetUniverse.Kinds.UserIdentificator";

			[Description("View")]
			[KindMetadata("View", "View", Constants.Kinds.Kind)]
			public const string View = "ZetUniverse.Kinds.Views.View";


			/// <summary>
			/// Visual Cluster is a fundamental organizing and grouping concept in Zet Universe. Visual Cluster represents a 2D group of things where relative location of things is explicitely defined by user or automatic layout algorithm.
			/// </summary>
			[Description("Visual Cluster")]
			[KindMetadata("Visual Cluster Topic", "Visual Cluster", Constants.Kinds.Kind)]
			public const string VisualCluster = "ZetUniverse.Kinds.Topic";

			/// <summary>
			/// Movies, television programs, YouTube videos, and any other recorded "moving picture" is classified as a video.
			/// </summary>
			[Description("Video")]
			[KindMetadata("Video Media", "Video", Constants.Kinds.Media)]
			public const string Video = "ZetUniverse.Kinds.Media.Video";

			/// <summary>
			/// Web Page is a Media
			/// </summary>
			[Description("Web Page")]
			[KindMetadata("Web Page WebPage Web", "Web Page", Constants.Kinds.Media)]
			public const string WebPage = "ZetUniverse.Kinds.WebPage";
			
			/// <summary>
			/// Zet Universe is the name of the information organization, tracking, analysis, and retrieval system.
			/// </summary>
			[Description("Zet Universe")]
			[KindMetadata("Zet Universe System", "Zet Universe", Constants.Kinds.Kind, true, false)]
			public const string ZetUniverse = "ZetUniverse.Kinds.System";

			public static string GetKindDescription(string kind)
			{
				var kindsDict = Constants.Kinds.BuiltInKindsDictionary;

				if (kindsDict.ContainsKey(kind)) return kindsDict[kind].Item1;

				return "Unknown Kind";
			}

			public static string GetKindFullTextAliases(string kind)
			{
				var kindsDict = Constants.Kinds.BuiltInKindsDictionary;

				if (kindsDict.ContainsKey(kind)) return kindsDict[kind].Item2;

				return "Unknown Kind";
			}

			/// <summary>
			/// Contains a trio of:
			/// Kind String (Kind Display Name | Kind Full-Text Aliases | Parent Kind | IsHidden | IsAbstract)
			/// Example:
			/// ZetUniverse.Kinds.Media.Video (Video | Media Video | ZetUniverse.Kinds.Media | False | False )
			/// </summary>
			public static Dictionary<string, Tuple<string, string, string, bool, bool>> BuiltInKindsDictionary
			{
				get
				{
					var dict = new Dictionary<string, Tuple<string, string, string, bool, bool>>();

					var allKinds = typeof(Constants.Kinds).GetFields().ToList();

					foreach (var kind in allKinds)
					{
						var desc = string.Empty;

						//var attrs1 = kind.GetCustomAttributes(typeof(DescriptionAttribute), false).Cast<DescriptionAttribute>().ToList();

						var attrs = kind.GetCustomAttributes(typeof(KindMetadataAttribute), false).Cast<KindMetadataAttribute>().ToList();

						if (attrs.Count == 0)
						{
							Log.Info("Skipping kind \"{0}\": missing metadata", kind.Name);
							continue;
						}

						var first = attrs.FirstOrDefault();

						dict.Add(kind.GetRawConstantValue().ToString(), 
							new Tuple<string, string, string, bool, bool>
							(first.KindDescription, first.KindAliases, first.KindParent, first.IsHidden, first.IsAbstract));


						//if (first != null)
						//	desc = first.KindDescription;
						//else
						//	desc = kind.Name;

						//dict.Add(kind.GetRawConstantValue().ToString(), desc);
					}

					return dict;
				}
			}

			public static bool IsBuiltInKind(string kind)
			{
				if (BuiltInKindsDictionary.ContainsKey(kind))
					return true;
				return false;
			}

			public static Constants.KindSelection GetKindSelection(IEntity entity)
			{
				if (entity != null)
				{
					if (!string.IsNullOrEmpty(entity.Kind))
						switch (entity.Kind)
						{
							//case Constants.Kinds.Appointment:
							//case Constants.Kinds.Article:
							//case Constants.Kinds.Author:
							//case Constants.Kinds.Book:
							//case Constants.Kinds.Call:
							//case Constants.Kinds.Document:
							//case Constants.Kinds.Email:
							//case Constants.Kinds.GenericFile:
							//case Constants.Kinds.InstantMessage:
							//case Constants.Kinds.Link:
							//case Constants.Kinds.List:
							//case Constants.Kinds.
							//case Constants.Kinds.Meeting:
							//case Constants.Kinds.Movie:
							//case Constants.Kinds.Music:
							//case Constants.Kinds.MusicalPlaylist:
							//case Constants.Kinds.Organization:
							//case Constants.Kinds.Person:
							//case Constants.Kinds.Picture:
							//case Constants.Kinds.RecordedTV:
							//case Constants.Kinds.ProjectSpace:
							//case Constants.Kinds.Video:
							//case Constants.Kinds.WebPage:
							//	return Constants.KindSelection.Entity;

							case Constants.Kinds.VisualCluster:
								return Constants.KindSelection.VisualCluster;

							default:
								return Constants.KindSelection.Entity;
						}
					return Constants.KindSelection.None;
				}
				return Constants.KindSelection.None;
			}

			public static bool CanBePartOfVisualCluster(IEntity entity)
			{
				if (entity != null)
				{
					if (!string.IsNullOrEmpty(entity.Kind))
						switch (entity.Kind)
						{
							// only Users and other Visual Clusters can't be a part of a cluster
							case Constants.Kinds.User:
							case Constants.Kinds.VisualCluster:
							case Constants.Kinds.AppSpace:
								return false;

							// but everything else can
							default:
								return true;
						}
					return false;
				}
				return false;
			}

			internal static bool CanParticipateInTopographicClusterVisualization(IEntity entity)
			{
				if (entity == null) return false;

				if (!string.IsNullOrEmpty(entity.Kind))
				{
					switch (entity.Kind)
					{
						// only Users and other Visual Clusters can't be a part of a cluster
						case Constants.Kinds.User:
						//case Constants.Kinds.Person:
						case Constants.Kinds.VisualCluster:
						case Constants.Kinds.AppSpace:
							return false;

						// but everything else can
						default:
							return true;
					}
				}

				return false;
			}
		} // class

		public static class AppsKinds
		{
			public const string FileStore = "ZetUniverse.Apps.FileStore";
		}

		public enum KindSelection
		{
			Entity = 0,
			VisualCluster = 1,
			None = 2,
			User = 3
		} // enum

		public static class KnownEntities
		{
			/// <summary>
			/// Unsorted Visual Cluster contains all elements that are not sorted yet.
			/// </summary>
			public static string Unsorted = "47584ee4-38b1-4be9-8efb-5e3ebccc0935";
			public static string DefaultUnsortedList = "e7303df0-d4a0-4148-ac2c-4cf106592460";

			internal static bool IsKnown(VisualCluster visualCluster)
			{
				// case by case

				// Unsorted 
				if (visualCluster.Topic != null && visualCluster.Topic.Id == Unsorted)
					return true;

				return false;
			}
		}

		public static class Importers
		{
			public static class FileSystemImporter
			{
				/// <summary>
				/// Property name for FileSystemDirectories - these properties are stored within space's entity in Metaspace
				/// </summary>
				public const string FileSystemDirectories = "ZetUniverse.Importers.FileSystemImporter.FileSystemDirectories";
			}

			public static class WebPageImporter
			{
				/// <summary>
				/// Maximum allowed Web Page download timeout interval (5 min, enough for slow connections).
				/// </summary>
				public const int MaximumDownloadTimeInterval = 300;
				/// <summary>
				/// This is a Readability Parser Token, registered for Daniel Kornev. We should encourage users to obtain their own tokens.
				/// </summary>
				public const string DefaultReadabilityParserToken = "b8891e5460abfb8ff0273c1550cb346c63e29245";
			}

			public static class Services
			{
				public static class Dropbox
				{
					public const string ProviderName = "Dropbox";
				}

				public static class Microsoft
				{
					public static class Office365
					{
						public const string ProviderName = "Office365";
						public const string TenantName = "TenantName";
					}

					public static class Outlook
					{
						public const string ProviderName = "Outlook.com";
						public const string LocalCacheName = "LocalCache";

						public static class ContactSchema
						{
							//public const string FirstName = "OutlookFirstName";
							//public const string LastName = "OutlookLastName";
							public const string IsFriend = "OutlookIsFriend";
							public const string IsFavorite = "OutlookIsFavorite";
							public const string UserId = "OutlookUserId";
							//public const string AccountEmail = "OutlookAccountEmail";
							//public const string BusinessEmail = "OutlookBusinessEmail";
							//public const string PersonalEmail = "OutlookPersonalEmail";
							////public const string OtherEmail = "OutlookOtherEmail";
							public const string UpdatedTime = "OutlookUpdatedTime";
							//public const string Name = "OutlookName";
							public const string Id = "OutlookId";
						}
					}

				}
			}
		} // class

		public static class Indexers
		{

			public static class FullTextSearchIndexer
			{
				public const string DocumentId = "EntityIdPlusModelId";

				public const string FullTextHash = "FullTextHash";

				public const string FullText = "FullText";

				public const string Title = "Title";

				public const string VisualClusterId = "ClusterId";

				public const string Kind = "Kind";

				public const string Keyphrases = "Keyphrases";
			}
		}

		public static class Properties
		{
			/// <summary>
			/// Required for synchronization. Rule of thumb: one entity = one sync source.
			/// </summary>
			public const string SyncSource = "SyncSource";
			public const string FullPath = "FullPath";
			public const string AppId = "AppId";
			public const string AppRootFolder = "AppRootFolder";
			public const string Uri = "Uri";

			public static class WebPage
			{
				public const string LocalPageUri = "LocalPageUri";
			}

			public static class Person
			{
				public const string FirstName = "FirstName";
				public const string LastName = "LastName";
				/// <summary>
				/// Business Email
				/// </summary>
				public const string Email1 = "Email1";
				/// <summary>
				/// Home Email
				/// </summary>
				public const string Email2 = "Email2";
				/// <summary>
				/// Other Email
				/// </summary>
				public const string Email3 = "Email3";
				/// <summary>
				/// Account Email
				/// </summary>
				public const string Email4 = "Email4";
				/// <summary>
				/// Business 2 Email
				/// </summary>
				public const string Email5 = "Email5";
				/// <summary>
				/// Home 2 Email
				/// </summary>
				public const string Email6 = "Email6";
				/// <summary>
				/// Company Email
				/// </summary>
				public const string Email7 = "Email7";
				public const string Email8 = "Email8";
				public const string Email9 = "Email9";
				public const string Email10 = "Email10";
				public const string HomePhone = "HomePhone";
				public const string MobilePhone = "MobilePhone";
				public const string WorkPhone = "WorkPhone";
				public const string WorkFax = "WorkFax";
			}
		}

		public static class Relationships
		{
			[RelationshipMetadata("authored by", "authored", false)]
			public const string IsAuthoredBy = "IsAuthoredBy";
			[RelationshipMetadata("also known as", "also known as", false)]
			public const string SameEntity = "SameEntity";
			[RelationshipMetadata("part of the list", "has", true)]
			public const string PartOfList = "IsPartOfList";
			[RelationshipMetadata("part of the visual cluster", "has", true)]
			public const string PartOfVisualCluster = "IsPartOfVisualCluster";
			[RelationshipMetadata("changed by", "changed", true)]
			public const string IsChangedBy = "IsChangedBy";
			[RelationshipMetadata("mentioned in", "mentions", false)]
			public const string IsMentionedIn = "IsMentionedIn";

			public const string CurrentZetUniverseUser = "IsCurrentZetUniverseUser";

			//public static string GetFriendlyName(EntityRef metaId, bool includeConfidenceWhenNotOne = true, bool showFrom = false)
			//{
			//	string result = "related";
			//	string confidence = string.Format("{0:0.##}", metaId.Confidence);

			//	if (!showFrom)
			//		if (BuiltInRelationshipsDictionary.ContainsKey(metaId.Relation))
			//			result = BuiltInRelationshipsDictionary[metaId.Relation];
				
			//	if (showFrom)
			//		if (BuiltInFromRelationshipsDictionary.ContainsKey(metaId.Relation))
			//			result = BuiltInFromRelationshipsDictionary[metaId.Relation];

			//	if (metaId.Confidence < 1 && includeConfidenceWhenNotOne)
			//		result = result + " (confidence: " + confidence + ")";

			//	return result;
			//}

			/// <summary>
			/// Relationship / Description (for now). Doesn't include system relations (they are used for internal purposes only).
			/// </summary>
			public static Dictionary<string, string> BuiltInRelationshipsDictionary
			{
				get
				{
					var dict = new Dictionary<string, string>();

					var allRels = typeof(Constants.Relationships).GetFields().ToList();

					foreach (var rel in allRels)
					{
						var desc = string.Empty;

						var attrs = rel.GetCustomAttributes(typeof(RelationshipMetadataAttribute), false).Cast<RelationshipMetadataAttribute>().ToList();

						if (attrs.Count == 0)
						{
							Log.Info("Skipping kind \"{0}\": missing metadata", rel.Name);
							continue;
						}

						

						var first = attrs.FirstOrDefault();

						if (first.IsSystem)
							continue;

						dict.Add(rel.GetRawConstantValue().ToString(),
							first.Description);
					}

					return dict;
				}
			}

			public static Dictionary<string, string> BuiltInFromRelationshipsDictionary
			{
				get
				{
					var dict = new Dictionary<string, string>();

					var allRels = typeof(Constants.Relationships).GetFields().ToList();

					foreach (var rel in allRels)
					{
						var desc = string.Empty;

						//var attrs1 = kind.GetCustomAttributes(typeof(DescriptionAttribute), false).Cast<DescriptionAttribute>().ToList();

						var attrs = rel.GetCustomAttributes(typeof(RelationshipMetadataAttribute), false).Cast<RelationshipMetadataAttribute>().ToList();

						if (attrs.Count == 0)
						{
							Log.Info("Skipping kind \"{0}\": missing metadata", rel.Name);
							continue;
						}

						var first = attrs.FirstOrDefault();

						dict.Add(rel.GetRawConstantValue().ToString(),
							first.DescriptionFrom);
					}

					return dict;
				}
			}
		}

		public static class Meta
		{
			public static class Confidence
			{
				public const double OneHundredPercent = 1.0; // 100% confidence
				public const double NightyFivePercent = 0.95; // 95%, often used by researchers
				public const double EightyPercent = 0.8; // 80%
				public const double SeventyFivePercent = 0.75; // 3/4
				public const double SixtySixPercent = 0.66; // 2/3
			}
		}

		public static class Application
		{
			public static class ErrorCodes
			{
				public const int UnableToCreateMetaspaceAndOrUserFolder = 101;
				public const int SettingsObjectIsNull = 102;
				public const int MainWindowIsNull = 103;
				public const int UnhandledException = 104;

				// Migration
				public const int MigrationSucceeded = 200;

				public const int FailedToExtractData = 201;
				public const int FailedToPrepareStorageRecord = 202;
				public const int FailedToMigrateData = 210;
				public const int ProvidedForMigrationDataIsNull = 211;

				public const int MigrationFailed = 220;
			}


			public static class Configuration
			{
				/// <summary>
				/// "Zet Universe"
				/// </summary>
				public const string ZetUniverseRootFolder = "Zet Universe";
				/// <summary>
				/// "Metaspace"
				/// </summary>
				public const string ZetUniverseMetaspaceFolder = "Metaspace";

				/// <summary>
				/// "user.v2.settings"
				/// </summary>
				public const string ZetUniverseUserSettingsV2File = "user.v2.settings";

				public const string ZetUniverseUserSettingsFile = "user.settings";
				public const string ZetUniverseProcessorsFolder = "Processors";

				public const string AppSpacesFolder = "AppSpaces";
				public const string SpacesFolder = "Spaces";
				/// <summary>
				/// ..\Binaries\Plugins
				/// </summary>
				public const string SALPluginsFolder = "Plugins";
				public const string SALSettingsFolder = "Plugins";
				/// <summary>File config name</summary>
				public const String AssemblyFileName = @"Assembly.config";

				public const string ZetUniverseOnlineSetupUrl = "https://zetuniversealpha.blob.core.windows.net/20150622-1345/";
				internal const string ZetUniverseLocalSetupUrl = @"C:\stuff\dev\zu_src\ZU\Releases";

				public const string ZetUniverseOnlineInsidersCommunityUrl = @"https://www.facebook.com/groups/ZetUniverseAlphaTesters/";
				public const string ZetUniverseOnlineUserVoiceCommunityUrl = @"http://zetuniverse.uservoice.com/";

				public static class StorageProviders
				{
					/// <summary>
					/// Legacy stable 
					/// </summary>
					public const string FolderStorageProvider = "FolderStorageProvider";
					/// <summary>
					/// Legacy highly unstable
					/// </summary>
					public const string CompoundFileStorageProvider = "CompoundFileStorageProvider";
					/// <summary>
					/// Current promising
					/// </summary>
					public const string NextStorageProvider = "NextStorageProvider";
				}
			}
		}

		public static class Service
		{
			public static class Roles
			{
				public const string User = "ZetUniverse.Service.Roles.User";
				public const string Admin = "ZetUniverse.Service.Roles.Admin";
			}
		}

		/// <summary>
		/// Get description of a enum value
		/// See DescriptionAttribute for enum element
		/// </summary>
		/// <remarks>
		/// Returns the human readable string set as the DescriptionAttribute
		/// for an enum element.
		/// If the DescriptionAttribute has not been set, returns the enum element name
		/// </remarks>
		/// <example>
		/// public enum MyEnum
		///    {
		///    [DescriptionAttribute("A Human readable string")]
		///    AMachineReadableEnum,
		///    }
		/// ...
		///    string s = MyEnum.AMachineReadableEnum.ToString();
		///    s += " : " + GetDescription(MyEnum.AMachineReadableEnum);
		///    MessageBox.Show(s);
		///    
		/// would display "AMachineReadableEnum : A Human readable string"
		/// </example>
		/// <seealso cref="ValueOf<T>"/>
		/// <param name="value">Enum element with human readable string</param>
		/// <returns>Human readable string for enum element</returns>
		public static string GetDescription(Enum value)
		{
			// Get information on the enum element
			FieldInfo fi = value.GetType().GetField(value.ToString());
			// Get description for elum element
			DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (attributes.Length > 0)
			{
				// DescriptionAttribute exists - return that
				return attributes[0].Description;
			}
			// No Description set - return enum element name
			return value.ToString();
		}
		/// <summary>
		/// Get an enum element from its human readable string.
		/// </summary>
		/// <remarks>
		/// Returns the enum element for a human readable string
		/// If the DescriptionAttribute has not been set, throws an
		/// ArgumentException
		/// </remarks>
		/// <example>
		/// public enum MyEnum
		///    {
		///    [DescriptionAttribute("A Human readable string")]
		///    AMachineReadableEnum,
		///    }
		/// ...
		///    MyEnum me = ValueOf<MyEnum>("A Human readable string");
		///    MyEnum me = MyEnum.AMachineReadableEnum;
		///    
		/// would be equivelent
		/// </example>
		/// <exception cref="ArgumentException if description not found to match an enum element"
		/// <seealso cref="GetDescription"/>
		/// <typeparam name="T">Enum the element will belong to</typeparam>
		/// <param name="description">Human readable string assocuiated with enum element</param>
		/// <returns>Enum element</returns>
		public static T ValueOf<T>(string description)
		{
			Type enumType = typeof(T);
			string[] names = Enum.GetNames(enumType);
			foreach (string name in names)
			{
				if (GetDescription((Enum)Enum.Parse(enumType, name)).Equals(description, StringComparison.InvariantCultureIgnoreCase))
				{
					// Found it!
					return (T)Enum.Parse(enumType, name);
				}
			}
			// No such description in this enum
			throw new ArgumentException("The string is not a description or value of the specified enum.");
		}

		public static class Topics
		{
			public static class Classifications
			{
				/// <summary>
				/// /Classification/
				/// </summary>
				public const string Name = "/Classifications/";
				/// <summary>
				/// /Classifications/Kinds/
				/// </summary>
				public const string Kinds = "/Classifications/Kinds/";

				/// <summary>
				/// /Classifications/VisualClusters/
				/// </summary>
				public const string VisualClusters = "/Classifications/VisualClusters/";
				/// <summary>
				/// /Classifications/WebPages/
				/// </summary>
				public static string WebPages = "/Classifications/WebPages/";
			}

			/// <summary>
			/// /Content/
			/// </summary>
			public static class Content
			{
				/// <summary>
				/// /Content/FullText/
				/// Required for saving text to Full-Text Search Indexing Service
				/// </summary>
				public const string FullText = "/Content/FullText/";
				public const string Title = "/Content/Title/";
				public const string Thumbnail = "/Content/Thumbnail/";
			}

			public static class Incoming
			{
				/// <summary>
				/// /Incoming/
				/// </summary>
				public const string Name = "/Incoming/";
				/// <summary>
				/// /Incoming/Files/
				/// </summary>
				public const string Files = "/Incoming/Files/";
				/// <summary>
				/// /Incoming/NormalizeText/
				/// </summary>
				public const string NormalizedText = "/Incoming/NormalizedText/";
				/// <summary>
				/// /Incoming/Keyphrases/
				/// </summary>
				public const string Keyphrases = "/Incoming/Keyphrases/";

				public const string ExtractedRelationships = "/Incoming/ExtractedRelationships/";

				public static class Kinds
				{
					public const string GenericFile = "/Incoming/Kinds/GenericFile/";
					public const string WebPage = "/Incoming/Kinds/WebPage/";
				}
			}

			public static class ActivityHistory
			{
				/// <summary>
				/// "/ActivityHistory/SemanticActionEntries/"
				/// </summary>
				public const string SemanticActionEntries = "/ActivityHistory/SemanticActionEntries/";
			}
		}

		public static class StorageHelper
		{
			/// <summary>
			///example: e.00f682a9-eb6e-40ad-9de9-ef1cd25726fb.001118,000454,000100,000100.20150104014537881.jsn
			/// identifying blocks: e.{00f682a9-eb6e-40ad-9de9-ef1cd25726fb}.{001118},{000454},{000100},{000100}.{20150104014537881}.jsn
			/// and so, our wildcard is below:
			/// Wildcard to load/watch entity with its location, height and width, specified in the file name (e.{sa}.{id}.{Sxxxxxx},{Syyyyyy},{wwwwww},{hhhhhh}.{yyyyMMddHHmmssff}.{iiiiii}.jsn)
			/// </summary>
			public const string StrFilterLocationE = "e.???.????????-????-????-????-????????????.???????,???????,??????,??????.????????????????????.jsn";


			/// <summary>
			/// yyyyMMddHHmmssffffff
			/// ????????????????????
			/// Wildcard to load/watch app source-bound List entity with its location, height and width, specified in the file name (e.list.{sa}.{id}.{Sxxxxxx},{Syyyyyy},{wwwwww},{hhhhhh}.{yyyyMMddHHmmssff}.{iiiiii}.jsn)
			/// </summary>
			public const string strFilterLocationALE = "e.alist.???.????????-????-????-????-????????????.???????,???????,??????,??????.????????????????????.jsn";

			/// <summary>
			/// Wildcard to load/watch List entity with its location, height and width, specified in the file name (e.list.{sa}.{id}.{Sxxxxxx},{Syyyyyy},{wwwwww},{hhhhhh}.{yyyyMMddHHmmssff}.{iiiiii}.jsn)
			/// </summary>
			public const string strFilterLocationLE = "e.list.???.????????-????-????-????-????????????.???????,???????,??????,??????.????????????????????.jsn";

			
			/// <summary>
			/// Wildcard to load/watch entity (e.{id}.{yyyyMMddHHmmssff}.jsn)
			/// </summary>
			public const string strFilterE = "e.????????-????-????-????-????????????.????????????????????.jsn";

			/// <summary>
			/// Wildcard to load/watch List entity (e.list.{id}.{yyyyMMddHHmmssff}.jsn)
			/// </summary>
			public const string strFilterLE = "e.list.????????-????-????-????-????????????.????????????????????.jsn";

			/// <summary>
			/// Date Format
			/// </summary>
			public const string strEDateFormat = "yyyyMMddHHmmssffffff";
			/// <summary>
			/// File name parts: Date Part
			/// </summary>
			public const string strEDatePart = "." + strEDateFormat;
			/// <summary>
			/// File name parts: Id Part
			/// </summary>
			public const string strEIdPart = ".????????-????-????-????-????????????";

			public static Rect GetBoundsFromStorageRecordKey(string recordKey)
			{
				Rect result = new Rect();

				string str = string.Empty;

				// obtains .{xxxxxx},{yyyyyy},{wwwwww},{hhhhhh} which becomes an extension for the trimmed file name
				str = Path.GetExtension
					(
						// removes: ".yyyyMMddHHmmssffffff"
						Path.GetFileNameWithoutExtension
						(
							// removes: ".jsn"
							Path.GetFileNameWithoutExtension(recordKey)
						)
					);

				// at this point, we have this: .{Sxxxxxx},{Syyyyyy},{wwwwww},{hhhhhh}

				// so: ".+002871,+000530,000100,000100"
				string strX = str.Substring(1, 7);
				if (strX.StartsWith("+"))
					strX = strX.Substring(1, 6);

				string strY = str.Substring(9, 7);
				if (strY.StartsWith("+"))
					strY = strY.Substring(1, 6);

				string strWidth = str.Substring(17, 6);
				string strHeight = str.Substring(24, 6);

				// defining
				double x = 0;
				double y = 0;
				double width = 0;
				double height = 0;

				bool failed = true;

				if (Double.TryParse(strX, out x) && Double.TryParse(strY, out y) && Double.TryParse(strWidth, out width) && Double.TryParse(strHeight, out height))
					failed = false;

				if (failed)
					return Rect.Empty;

				// forming new Rectangle
				result = new Rect(x, y, width, height);

				return result;
			}


			public static string GetIdFromStorageRecordKey(string recordKey)
			{
				string result = string.Empty;

				//// old: e.list.{id}.{yyyyMMddHHmmssff}.jsn
				//// new: e.list.{id}.{xxxxxx},{yyyyyy},{wwwwww},{hhhhhh}.{yyyyMMddHHmmssffffff}.jsn 

				// obtains .{id} which becomes an extension for the trimmed file name
				result = Path.GetExtension
					(
						// removes: {xxxxxx},{yyyyyy},{wwwwww},{hhhhhh}
						Path.GetFileNameWithoutExtension
						(
							// removes: ".yyyyMMddHHmmssffffff"
							Path.GetFileNameWithoutExtension
							(
								// removes: ".jsn"
								Path.GetFileNameWithoutExtension(recordKey)
							)
						)
					);

				if (result.Length == strEIdPart.Length)
				{
					return result.Substring(1);
				}
				return null;
			}//GetEntityIdFromFileName()

			//List<SemanticActionKinds> DetectedSemanticActionsList = new List<SemanticActionKinds>();

			public static Semantic.Core.SemanticActionKinds GetSemanticActionFromStorageRecordKey(string recordKey)
			{
				string result = string.Empty;

				//// old: e.list.{id}.{yyyyMMddHHmmssff}.jsn
				//// new: e.list.{sa}.{id}.{xxxxxx},{yyyyyy},{wwwwww},{hhhhhh}.{yyyyMMddHHmmssff}.jsn 

				// obtains .{id} which becomes an extension for the trimmed file name
				result = Path.GetExtension
					(
						// removes: {id}
						Path.GetFileNameWithoutExtension(
							// removes: {xxxxxx},{yyyyyy},{wwwwww},{hhhhhh}
							Path.GetFileNameWithoutExtension
							(
								// removes: ".yyyyMMddHHmmssffffff"
								Path.GetFileNameWithoutExtension
								(
									// removes: ".jsn"
									Path.GetFileNameWithoutExtension(recordKey)
								)
							)
						)
					);


				if (result.Length == 4)
				{
					// let's remove the dot
					result = result.Substring(1, 3);

					return (SemanticActionKinds)Enum.Parse(typeof(SemanticActionKinds), result);
				}
				return SemanticActionKinds.None;
			}//GetEntitySemanticActionFromFileName()


			// this should work as it did before, no changes
			public static DateTime GetChangeDateFromStorageRecordKey(string recordKey)
			{
				var result = DateTime.MinValue;
				var fnDate = Path.GetExtension
					(
						Path.GetFileNameWithoutExtension(recordKey)
					);
				if ((!string.IsNullOrEmpty(fnDate)) && (fnDate.Length == strEDatePart.Length))
				{
					fnDate = fnDate.Substring(1, strEDateFormat.Length);

					//result = new DateTime
					//	(
					//	// yyyyMMdd
					//		fnDate.Substring(1, 4).As<int>(1),
					//		fnDate.Substring(5, 2).As<int>(1),
					//		fnDate.Substring(7, 2).As<int>(1),
					//	// HHmmssff
					//		fnDate.Substring(9, 2).As<int>(0),
					//		fnDate.Substring(11, 2).As<int>(0),
					//		fnDate.Substring(13, 2).As<int>(0),
					//		fnDate.Substring(15, 2).As<int>(0) * 10,
					//	// Utc
					//		DateTimeKind.Utc
					//	);

					result = DateTime.ParseExact(fnDate, strEDateFormat, null);
				}
				return result;
			}//GetEntityChangeDateFromFileName()

			/// <summary>
			/// Returns in the format [UID as ShortGUID]\[StorageRecordKey]
			/// </summary>
			/// <param name="entity"></param>
			/// <param name="UID"></param>
			/// <returns></returns>
			public static string GetEntityStorageRecordKey(IEntity entity, EntityRef UID)
			{

				string sFn = String.Format("{1}\\{0}", GetEntityStorageRecordKeyWithoutUID(entity), UID.IdToShortGuid());
				
				return sFn;
			}//GetEntityFileName()

			/// <summary>
			/// Returns in the format [StorageRecordKey]
			/// </summary>
			/// <param name="entity"></param>
			/// <returns></returns>
			public static string GetEntityStorageRecordKeyWithoutUID(IEntity entity)
			{
				string sFn = string.Empty;
				int left = MyRound(entity.ZPos.Left);
				int top = MyRound(entity.ZPos.Top);
				int width = MyRound(entity.ZPos.Width);
				int height = MyRound(entity.ZPos.Height);

				string xxxxxx = left.ToString("D6");
				if (left >= 0)
					xxxxxx = "+" + xxxxxx;

				string yyyyyy = top.ToString("D6");
				if (top >= 0)
					yyyyyy = "+" + yyyyyy;

				string wwwwww = width.ToString("D6");
				string hhhhhh = height.ToString("D6");

				int saInt = (int)entity.SemanticAction;
				string sa = saInt.ToString("D3");


				if (entity.IsAppSourceBoundList)
				{
					// old: {Model.UID}\e.list.{id}.{yyyyMMddHHmmssff}.jsn
					// new: {Model.UID}\e.list.{id}.{xxxxxx},{yyyyyy},{wwwwww},{hhhhhh}.{yyyyMMddHHmmssffffff}.

					sFn = string.Format("e.alist.{7}.{0}.{3},{4},{5},{6}.{1:" + Constants.StorageHelper.strEDateFormat + "}.jsn", entity.Id, entity.TLChange, string.Empty, xxxxxx, yyyyyy, wwwwww, hhhhhh, sa);
				}
				else if (entity.IsList)
				{
					// old: {Model.UID}\e.list.{id}.{yyyyMMddHHmmssff}.jsn
					// new: {Model.UID}\e.list.{id}.{xxxxxx},{yyyyyy},{wwwwww},{hhhhhh}.{yyyyMMddHHmmssffffff}.

					sFn = string.Format("e.list.{7}.{0}.{3},{4},{5},{6}.{1:" + Constants.StorageHelper.strEDateFormat + "}.jsn", entity.Id, entity.TLChange, string.Empty, xxxxxx, yyyyyy, wwwwww, hhhhhh, sa);
				}

				else
				{
					// old: {Model.UID}\e.{id}.{yyyyMMddHHmmssff}.jsn
					// new: {Model.UID}\e.{id}.{xxxxxx},{yyyyyy},{wwwwwww},{hhhhhh}.{yyyyMMddHHmmssffffff}.
					sFn = string.Format("e.{7}.{0}.{3},{4},{5},{6}.{1:" + Constants.StorageHelper.strEDateFormat + "}.jsn", entity.Id, entity.TLChange, string.Empty, xxxxxx, yyyyyy, wwwwww, hhhhhh, sa);
				}

				return sFn;
			}

			public static int MyRound(double d)
			{
				return (int)(d + 0.5);
			}
		}
	} // class
} // namespace