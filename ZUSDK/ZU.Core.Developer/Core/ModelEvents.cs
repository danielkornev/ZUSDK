using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.Core
{
	public static class ModelEvents
	{
		public delegate void OnEntityAddedEventHandler(IEntity entity, EventArgs e);
		public delegate void OnEntityDeletedEventHandler(IEntity entity, EventArgs e);
		public delegate void OnEntityPropertyChangedEventHandler(IEntity entity, PropertyChangedEventArgs e);
		public delegate void OnEntityDynamicPropertyChangedEventHandler(IEntity entity, PropertyChangedEventArgs e);
		public delegate void OnFileBasedEntityFileChangedEventHandler(IEntity entity, EventArgs e);
		public delegate void OnEntityPositionChangedEventHandler(IEntity entity, EventArgs e);
		public delegate void OnEntityAddedToVisualClusterEventHandler(IEntity entity, EventArgs e);
		public delegate void OnVisualClusterMovedEventHandler(object sender, EventArgs e);
		public delegate void OnVisualClusterCreatedEventHandler(object sender, EventArgs e);
		public delegate void OnVisualClusterDeletedEventHandler(object sender, EventArgs e);
		public delegate void OnEntityRemovedFromVisualClusterEventHandler(IEntity entity, EventArgs e);
	} // class
} // namespace
