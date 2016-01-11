using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using ZU.Core;
using ZU.Semantic.Text;

namespace ZU
{
	public static partial class Constants
	{
		public static partial class Kinds
		{
			[Description("Application")]
			[FullText("Application", "Application")]
			public const string Application = "ZetUniverse.Kinds.System.Application";

			[Description("Appointment")]
			[FullText("Appointment Calendar Event", "Appointment")]
			public const string Appointment = "ZetUniverse.Kinds.Calendar.Appointment";

			/// <summary>
			/// App Space is an organizing concept designed for keeping app-specific entities separated from other apps.
			/// </summary>
			[Description("App Space")]
			[FullText("App Space", "App Space")]
			public const string AppSpace = "ZetUniverse.Kinds.AppSpace";

			[Description("Article")]
			[FullText("Article Media", "Article")]
			public const string Article = "ZetUniverse.Kinds.Documents.Article";

			[Description("Author")]
			[FullText("Author Writer Creator People", "Author")]
			public const string Author = "ZetUniverse.Kinds.People.Author";

			[Description("Book")]
			[FullText("Book Media", "Book")]
			public const string Book = "ZetUniverse.Kinds.Documents.Book";

			[Description("Call")]
			[FullText("Call Communication", "Call")]
			public const string Call = "ZetUniverse.Kinds.Communications.Call";

			[Description("Document")]
			[FullText("Document Media", "Document")]
			public const string Document = "ZetUniverse.Kinds.Documents.Document";

			[Description("Email")]
			[FullText("Email Communication E-mail", "Email")]
			public const string Email = "ZetUniverse.Kinds.Communications.Email";

			[Description("Generic File")]
			[FullText("Generic File GenericFile", "Generic File")]
			public const string GenericFile = "ZetUniverse.Kinds.File";

			[Description("Instant Message")]
			[FullText("Instant Message IM InstantMessage Communication", "Communication")]
			public const string InstantMessage = "ZetUniverse.Kinds.Communications.InstantMessage";

			/// <summary>
			/// Link is the basis of the relationship management and semantic structuring within Zet Universe.
			/// </summary>
			[Description("Link")]
			[FullText("Link Relationship", "Link")]
			public const string Link = "ZetUniverse.Kinds.Link";
			
			/// <summary>
			/// List is a fundamental organizing and grouping concept in Zet Universe. List represents a 1D group of things where order of things is not explicitely defined.
			/// </summary>
			[Description("List")]
			[FullText("List", "List")]
			public const string List = "ZetUniverse.Kinds.List";
			
			/// <summary>
			/// 
			/// </summary>
			[Description("Meeting")]
			[FullText("Meeting Calendar Event", "Meeting")]
			public const string Meeting = "ZetUniverse.Kinds.Calendar.Meeting";
			
			/// <summary>
			/// This is the class of motion pictures created by recording using cameras or by creating images using animation or other special techniques.
			/// </summary>
			[Description("Movie")]
			[FullText("Movie Film Video Media", "Movie")]
			public const string Movie = "ZetUniverse.Kinds.Media.Video.Movie";
			
			/// <summary>
			/// The class of artifacts that are closely related to music. Musical artifacts include compositions themselves and related artifacts such as cover art on albums, written scores, lyrics, and other things.
			/// </summary>
			[Description("Music")]
			[FullText("Music Media", "Music")]
			public const string Music = "ZetUniverse.Kinds.Media.Music";
			
			/// <summary>
			/// This is the class of musical playlists that may be created by Zet Universe users. A Playlist is composed of a number of musical works. A Playlist is a kind of List.
			/// </summary>
			[Description("Music Playlist")]
			[FullText("MusicalPlaylist Musical Playlist MusicPlaylist Music Playlist", "Music Playlist")]
			public const string MusicalPlaylist = "ZetUniverse.Kinds.Media.Music.MusicalPlaylist";

			[Description("Organization")]
			[FullText("Organization Org Persona", "Organization")]
			public const string Organization = "ZetUniverse.Kinds.Organization";

			[Description("PDF Document")]
			[FullText("PDF Document PDFDocument PDF Media", "PDF Document")]
			public const string PdfDocument = "ZetUniverse.Kinds.Documents.PdfDocument";

			[Description("Person")]
			[FullText("Person Persona People", "Person")]
			public const string Person = "ZetUniverse.Kinds.People.Person";
			
			/// <summary>
			/// Generic list of people organized into a group. 
			/// </summary>
			[Description("People Group")]
			[FullText("People Group Group", "People Group")]
			public const string PeopleGroup = "ZetUniverse.Kinds.People.Group";

			[Description("Picture")]
			[FullText("Picture Image Photo Media", "Picture")]
			public const string Picture = "ZetUniverse.Kinds.Media.Picture";

			[Description("PowerPoint Presentation")]
			[FullText("PowerPoint Presentation Slidedeck Slides Document Media", "PowerPoint Presentation")]
			public const string PptxDocument = "ZetUniverse.Kinds.Documents.PptxDocument";
			
			/// <summary>
			/// (Project) Space is a fundamental organizing and grouping concept in Zet Universe. It contains individual entities grouped into visual clusters, or into lists. Every single entity always belongs to one or more visual clusters.
			/// </summary>
			[Description("Project")]
			[FullText("Project Space", "Project")]
			public const string ProjectSpace = "ZetUniverse.Kinds.Space";
			
			/// <summary>
			/// Recorded TVs are a special kind of entities that represent TVs recorded using Windows Media Center. Recorded TV is a kind of Video.
			/// </summary>
			[Description("Recorded TV")]
			[FullText("Recorded TV RecordedTV Video Media", "Recorded TV")]
			public const string RecordedTV = "ZetUniverse.Kinds.Media.Video.RecordedTV";
			//[Description("Sticky Note")]
			//public const string StickyNote = "ZetUniverse.Kinds.Notes.StickyNote";

			[Description("User")]
			[FullText("User", "User")]
			public const string User = "ZetUniverse.Kinds.UserIdentificator";/// <summary>
			
				/// Visual Cluster is a fundamental organizing and grouping concept in Zet Universe. Visual Cluster represents a 2D group of things where relative location of things is explicitely defined by user or automatic layout algorithm.
			/// </summary>
			[Description("Visual Cluster")]
			[FullText("Visual Cluster Topic", "Visual Cluster")]
			public const string VisualCluster = "ZetUniverse.Kinds.Topic";

			/// <summary>
			/// Movies, television programs, YouTube videos, and any other recorded "moving picture" is classified as a video.
			/// </summary>
			[Description("Video")]
			[FullText("Video Media", "Video")]
			public const string Video = "ZetUniverse.Kinds.Media.Video";

			[Description("Web Page")]
			[FullText("Web Page WebPage Web", "Web Page")]
			public const string WebPage = "ZetUniverse.Kinds.WebPage";
			
			/// <summary>
			/// Zet Universe is the name of the information organization, tracking, analysis, and retrieval system.
			/// </summary>
			[Description("Zet Universe")]
			[FullText("Zet Universe System", "Zet Universe")]
			public const string ZetUniverse = "ZetUniverse.Kinds.System";

			public static string GetKindDescription(string kind)
			{
				var kindsDict = Constants.Kinds.BuiltInKindsDictionary;

				if (kindsDict.ContainsKey(kind)) return kindsDict[kind].Item1;

				return "Unknown Kind";
			}

			/// <summary>
			/// Contains a trio of:
			/// Kind String (Kind Display Name | Kind Full-Text Aliases)
			/// Example:
			/// ZetUniverse.Kinds.Media.Video (Video | Media Video)
			/// </summary>
			public static Dictionary<string, Tuple<string, string>> BuiltInKindsDictionary
			{
				get
				{
					var dict = new Dictionary<string, Tuple<string, string>>();

					var allKinds = typeof(Constants.Kinds).GetFields().ToList();

					foreach (var kind in allKinds)
					{
						var desc = string.Empty;

						//var attrs1 = kind.GetCustomAttributes(typeof(DescriptionAttribute), false).Cast<DescriptionAttribute>().ToList();

						var attrs = kind.GetCustomAttributes(typeof(FullTextAttribute), false).Cast<FullTextAttribute>().ToList();

						if (attrs.Count == 0)
						{
							Log.Info("Skipping kind \"{0}\": missing metadata", kind.Name);
							continue;
						}

						var first = attrs.FirstOrDefault();

						dict.Add(kind.GetRawConstantValue().ToString(), new Tuple<string, string>(first.KindDescription, first.KindAliases));


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
							case Constants.Kinds.Appointment:
							case Constants.Kinds.Article:
							case Constants.Kinds.Author:
							case Constants.Kinds.Book:
							case Constants.Kinds.Call:
							case Constants.Kinds.Document:
							case Constants.Kinds.Email:
							case Constants.Kinds.GenericFile:
							case Constants.Kinds.InstantMessage:
							case Constants.Kinds.Link:
							case Constants.Kinds.List:
							case Constants.Kinds.Meeting:
							case Constants.Kinds.Movie:
							case Constants.Kinds.Music:
							case Constants.Kinds.MusicalPlaylist:
							case Constants.Kinds.Organization:
							case Constants.Kinds.Person:
							case Constants.Kinds.Picture:
							case Constants.Kinds.RecordedTV:
							case Constants.Kinds.ProjectSpace:
							case Constants.Kinds.Video:
							case Constants.Kinds.WebPage:
								return Constants.KindSelection.Entity;

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
							case Constants.Kinds.Person:
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
						case Constants.Kinds.Person:
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
			public const string IsAuthoredBy = "IsAuthoredBy";
			public const string SameEntity = "SameEntity";
			public const string PartOfList = "IsPartOfList";
			public const string IsChangedBy = "IsChangedBy";

			public const string CurrentZetUniverseUser = "IsCurrentZetUniverseUser";
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
	} // class
} // namespace