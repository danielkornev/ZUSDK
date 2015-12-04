using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZU.Core.Apps
{
	public partial class AppEntityKindDefinition
	{
		public string Kind { get; set; }
		public Guid AppId { get; set; }
		public string DisplayNameSingular { get; set; }
		public string DisplayNamePlural { get; set; }
		
		public DataTemplate Template { get; set; }
		public System.Windows.Media.ImageSource DefaultThumbnail { get; set; }

		//public Func<Entity, System.Windows.Media.ImageSource> GetEntityThumbnail;

		public string AppTitle { get; set; }

		public bool CanBePartOfVisualCluster { get; set; }

		public bool CanParticipateInTopographicClusterVisualization { get; set; }
	} // class
} // namespace
