using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZU.Telemetry
{
	public enum OperationKind
	{
		#region ZU Client App

		AppStarted,
		AppClosed,
		AppCrashed,
		FrameLoaded,

		#endregion

		#region Notification Platform
		NotificationsPlatformConnectedToCloudHub,
		NotificationsPlatformDisconnectedFromCloudHub,
		#endregion

		#region ZApp Account Management
		AppAccountAdded,
		AppAccountFailedToAdd,
		AppAccountRemoved,
		AppAccountSyncSettingsChanged,
		AppDataSourceImportCompleted,
		AppAccountSentRegisterRequestToCloudHub,
		AppAccountRegisterRequestRecievedByCloudHub,
		AppAccountRegisteredOnServiceSideWithCloudHub,
		AppAccountFailedToSendRegisterRequestToCloudHub,
		AppAccountFailedToRegisterOnServiceSideWithCloudHub,
		#endregion

		#region Space Management
		LocalSpaceAdded,
		LocalSpaceRemoved,
		DropboxSpaceAdded,
		DropboxSpaceShared,
		DropboxSpaceRemoved,

		#endregion

		#region Input Monitoring
		Mouse,
		Touch,
		Pen,
		#endregion


		#region Entity Management
		EntityCompletelyDeleted,
		EntityCopiedToClipboard,
		EntityDeleted,
		EntityPasted,
		EntityAdded,
		EntityOpened,
		EntityPreviewed,
		EntityRenamed,
		EntityVisualClusterAdded,
		EntityPropertiesShown,


		#endregion

		#region Copy/Paste & Drag-n-Drop
		ObjectDropped,
		ObjectPasted,
		#endregion

		#region Search & Filters
		Search,
		#endregion

		#region Navigation
		LoadedFirstSpace,
		LoadedSystemInformationModel,
		ModelLoaded,
		NavigatedToSpace,
		NavigatedToMetaspace,
		NavigatedToEntity,
		ZoomedUsingTouch,
		ZoomedUsingMouseWheel,
		#endregion

		#region UI
		MinimapUsed,
		#endregion

		#region Legacy
		FrameClosed,

		TopographicClustersVisualizationEffectIsEnabled,
		TopographicClustersVisualizationEffectIsDisabled,
		#endregion


		#region Deprecated


		FrameHostedInShell,
		FrameHostedInStandaloneApp,



		ShellNamespaceExtensionFileNotAvailable,
		ShellNamespaceExtensionRegistrationFailed,
		ShellNamespaceExtensionRegistrationSucceeded,

		InstallerInitialized,
		InstallationStarted,
		InstallationSucceeded,
		InstallationFailed,
		None,


		#endregion


	} // enum 
} // namespace
