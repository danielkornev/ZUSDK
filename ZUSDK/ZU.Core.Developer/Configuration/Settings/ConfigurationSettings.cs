using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZU.Configuration.Settings
{
	public class ConfigurationSettings : BasePropertyChanged, ZU.Configuration.Settings.IConfigurationSettings
	{
		public ConfigurationSettings()
		{
			_SpiralVisualizationWidth = 125;
			ShowImageTitles = true;
		}

		#region Fields
		private DateTime _dateCreated;
		private DateTime _dateChanged;
		private string _userId;
		private string _readabilityParserToken;
		private Dictionary<string, string> _exchangeAccounts = new Dictionary<string, string>();
		private string _microsoftAccountToken;
		private int _maximumDownloadTimeInterval;
		private string _metaspaceFolder;
		private string _settingsFilePath;
		private bool _loadTerminal = false;
		private string _microsoftAccountId;
		private string _microsoftAccountDisplayName;
		private bool _showPDFThumbnails;
		#endregion

		#region Product Properties
		/// <summary>
		/// AccountId (on the Service Side)
		/// </summary>
		public string UserId
		{
			get
			{
				return _userId;
			}
			set
			{
				SetProperty(value, ref _userId, "UserId");
			}
		}


		public bool LoadTerminal
		{

			get
			{
				return _loadTerminal;
			}
			set
			{
				SetProperty(value, ref _loadTerminal, "LoadTerminal");
			}
		}

		private bool _ShowQueryPane;
		public bool ShowQueryPane
		{
			get { return _ShowQueryPane; }
			set
			{
				if (value != _ShowQueryPane)
				{
					_ShowQueryPane = value;
					SetProperty(value, ref _ShowQueryPane, "ShowQueryPane");
				}
			}
		}
		


		public bool ShowPDFThumbnails
		{
			get
			{
				return _showPDFThumbnails;
			}
			set
			{
				SetProperty(value, ref _showPDFThumbnails, "ShowPDFThumbnails");
			}
		}

		private bool _ShowMinimap;
		public bool ShowMinimap
		{
			get { return _ShowMinimap; }
			set
			{
				if (value != _ShowMinimap)
				{
					_ShowMinimap = value;
					SetProperty(value, ref _ShowMinimap, "ShowMinimap");
				}
			}
		}
		

		private bool _SupportSavingFiles;
		public bool SupportSavingFiles
		{
			get { return _SupportSavingFiles; }
			set
			{
				if (value != _SupportSavingFiles)
				{
					_SupportSavingFiles = value;
					SetProperty(value, ref _SupportSavingFiles, "SupportSavingFiles");
				}
			}
		}

		private bool _UseSemanticProcessing;
		public bool UseSemanticProcessing
		{
			get { return _UseSemanticProcessing; }
			set
			{
				if (value != _UseSemanticProcessing)
				{
					_UseSemanticProcessing = value;
					SetProperty(value, ref _UseSemanticProcessing, "UseSemanticProcessing");
				}
			}
		}

		private bool _SaveChangesHistory;
		public bool SaveChangesHistory
		{
			get { return _SaveChangesHistory; }
			set
			{
				if (value != _SaveChangesHistory)
				{
					_SaveChangesHistory = value;
					SetProperty(value, ref _SaveChangesHistory, "SaveChangesHistory");
				}
			}
		}
		

		private bool _ShowRulesAlerts;
		public bool ShowRulesAlerts
		{
			get { return _ShowRulesAlerts; }
			set
			{
				if (value != _ShowRulesAlerts)
				{
					_ShowRulesAlerts = value;
					SetProperty(value, ref _ShowRulesAlerts, "ShowRulesAlerts");
				}
			}
		}

		private bool _ShowPeople;
		public bool ShowPeople
		{
			get { return _ShowPeople; }
			set
			{
				if (value != _ShowPeople)
				{
					_ShowPeople = value;
					SetProperty(value, ref _ShowPeople, "ShowPeople");
				}
			}
		}

		private bool _ShowViewsPane;
		public bool ShowViewsPane
		{
			get { return _ShowViewsPane; }
			set
			{
				if (value != _ShowViewsPane)
				{
					_ShowViewsPane = value;
					SetProperty(value, ref _ShowViewsPane, "ShowViewsPane");
				}
			}
		}

		private bool _enableEmptyProjectsCreation;
		public bool EnableEmptyProjectsCreation
		{
			get { return _enableEmptyProjectsCreation; }
			set
			{
				if (value != _enableEmptyProjectsCreation)
				{
					_enableEmptyProjectsCreation = value;
					SetProperty(value, ref _enableEmptyProjectsCreation, "EnableEmptyProjectsCreation");
				}
			}
		}
		
		

		[Newtonsoft.Json.JsonIgnore]
		public string MetaspaceFolder
		{
			get
			{
				return _metaspaceFolder;
			}
			set
			{
				SetProperty(value, ref _metaspaceFolder, "MetaspaceFolder");
			}
		}

		[Newtonsoft.Json.JsonIgnore]
		public string SettingsFilePath
		{
			get
			{
				return _settingsFilePath;
			}
			set
			{
				SetProperty(value, ref _settingsFilePath, "SettingsFilePath");
			}
		}
		#endregion

		#region Date
		public DateTime DateCreated
		{
			get
			{
				return _dateCreated;
			}
			set
			{
				SetProperty(value, ref _dateCreated, "DateCreated");
			}
		}
		public DateTime DateChanged
		{
			get
			{
				return _dateChanged;
			}
			set
			{
				SetProperty(value, ref _dateChanged, "DateChanged");
			}
		}
		#endregion

		#region Out-of-Box Services Settings
		public string ReadabilityParserToken
		{
			get
			{
				return _readabilityParserToken;
			}
			set
			{
				SetProperty(value, ref _readabilityParserToken, "ReadabilityParserToken");
			}
		}		
		#endregion

		#region Web Page Importer Settings
		public int MaximumDownloadTimeInterval
		{
			get
			{
				return _maximumDownloadTimeInterval;
			}
			set
			{
				SetProperty(value, ref _maximumDownloadTimeInterval, "MaximumDownloadTimeInterval");
			}
		}
		#endregion


		private bool _ShowCurrentVisualClusterNameInBreadcrumbBar;
		public bool ShowCurrentVisualClusterNameInBreadcrumbBar
		{
			get { return _ShowCurrentVisualClusterNameInBreadcrumbBar; }
			set
			{
				if (value != _ShowCurrentVisualClusterNameInBreadcrumbBar)
				{
					_ShowCurrentVisualClusterNameInBreadcrumbBar = value;
					SetProperty(value, ref _ShowCurrentVisualClusterNameInBreadcrumbBar, "ShowCurrentVisualClusterNameInBreadcrumbBar");
				}
			}
		}

		/// <summary>
		/// It's EntityId is User.Id on the Service Side.
		/// </summary>
		public Semantic.EntityRef UID { get; set; }

		public System.Windows.WindowState MainWindowState { get; set; }

		public double MainWindowLeft { get; set; }

		public double MainWindowWidth { get; set; }
		public double MainWindowHeight { get; set; }

		public double MainWindowTop { get; set; }
		public string LoginProvider { get; set; }

		public bool PopupTrackYourProjectsShown { get; set; }
		public bool PopupTrackYourProjectFilesShown { get; set; }
		public bool PopupUseFiltersShown { get; set; }
		public bool PopupDontShutdownAppShown { get; set; }

		public bool NotificationAppRunsInBackgroundShown { get; set; }

		public string UserTitle { get; set; }

		[Newtonsoft.Json.JsonIgnore]
		public bool IsRegistered
		{
			get
			{
				if (string.IsNullOrEmpty(this.UserId))
					return false;

				if (string.IsNullOrEmpty(this.LoginProvider))
					return false;

				if (this.UID == null)
					return false;

				if (string.IsNullOrEmpty(this.UserTitle))
					return false;

				return true;
			}
		}

		[Newtonsoft.Json.JsonIgnore]
		public string ZetInstallRoot
		{
			get;
			set;
		}

		public DateTime LastTimeUpdateChecked
		{
			get;
			set;
		}

		public ZU.Core.Update.UpdateCheckStates LastUpdateCheckResult
		{
			get;
			set;
		}

		public string LastUpdateCheckErrorMessage
		{ get; set; }

		[Newtonsoft.Json.JsonIgnore]
		public string SetupUrl
		{ get; internal set; }

		/// <summary>
		/// Used for debugging purposes only
		/// </summary>
		public AppMode AppMode
		{
			get;
			set;
		}


		private bool _ShowImageTitles;
		public bool ShowImageTitles
		{
			get { return _ShowImageTitles; }
			set
			{
				if (value != _ShowImageTitles)
				{
					SetProperty(value, ref _ShowImageTitles, "ShowImageTitles");
				}
			}
		}

		private double _SpiralVisualizationWidth;
		public double SpiralVisualizationWidth
		{
			get { return _SpiralVisualizationWidth; }
			set
			{
				if (value != _SpiralVisualizationWidth)
				{
					SetProperty(value, ref _SpiralVisualizationWidth, "SpiralVisualizationWidth");
				}
			}
		}

		public bool SkipDiskThumbnailCache { get; set; }
		public bool AvoidMemoryCleansing { get; set; }
	} // class
} // namespace
