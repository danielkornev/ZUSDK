using System;
using System.Windows;
using ZU.Semantic.Spatial;

namespace ZU.Core.Apps
{
	public partial class AppEntityKindDefinition
	{
		public string Kind { get; set; }
		public string ParentKind { get; set; }
		public Guid AppId { get; set; }
		public string DisplayNameSingular { get; set; }
		public string DisplayNamePlural { get; set; }
		public string FullTextAliases { get; set; }

        public bool IsDataOrganizationalType { get; set; }

		public ZoomableSpaceTemplateShapes Shape { get; set; }

		public DataTemplate Template { get; set; }
		public System.Windows.Media.ImageSource DefaultThumbnail { get; set; }

        /// <summary>
        /// Used to register custom kinds' default thumbnails
        /// </summary>
        public string DefaultThumbnailUri { get; set; }

		//public Func<Entity, System.Windows.Media.ImageSource> GetEntityThumbnail;

		public string AppTitle { get; set; }

		public bool CanBePartOfVisualCluster { get; set; }

		public bool CanParticipateInTopographicClusterVisualization { get; set; }
	} // class
} // namespace
