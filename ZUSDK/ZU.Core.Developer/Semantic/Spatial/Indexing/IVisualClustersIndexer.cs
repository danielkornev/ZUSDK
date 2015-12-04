using System;
using System.Windows;
using ZU.Core;
using ZU.Semantic.Core.Indexing;
namespace ZU.Semantic.Spatial.Indexing
{
	public interface IVisualClustersIndexer : IIndexer
	{
		void CleanupAfterEntityWasDeleted(ZU.Core.IEntity deletedEntity);
		double Epsilon { get; }
		System.Collections.Generic.List<ZU.Core.IEntity> GetNearestEntities(ZU.Core.IEntity entity);
		System.Collections.Generic.List<ZU.Core.IEntity> GetNearestEntitiesWithinRadius(ZU.Core.IEntity entity, System.Collections.Generic.List<ZU.Core.IEntity> allEntities, System.Collections.Generic.List<ZU.Core.IEntity> except, double radius);
		System.Collections.Generic.List<ZU.Core.IEntity> GetNearestEntitiesWithinRadius(ZU.Core.IEntity entity, string kindFilter, System.Collections.Generic.List<ZU.Core.IEntity> allEntities, double radius);
		bool IsInitialized { get; set; }
		System.Collections.ObjectModel.ReadOnlyObservableCollection<ZU.Core.VisualCluster> KnownVisualClusters { get; }
		bool LogResults { get; set; }
		ZU.Core.IModel Model { get; }
		bool TryBuildIndex();
		//bool TryBuildIndexOLD();
		bool TryGetVisualCluster(ZU.Core.IEntity entity, out ZU.Core.VisualCluster oldCluster);
		bool TryUpdateIndex(ZU.Core.IEntity entity);
		EntityRef UID { get; }
		event EventHandler VisualClusterChanged;
		System.Collections.ObjectModel.ObservableCollection<ZU.Core.VisualCluster> VisualClusters { get; set; }
		/// <summary>
		/// Simplified automatic label placement for visual cluster's entity given the top entity
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="topicEntityWidth"></param>
		/// <param name="topicEntityHeight"></param>
		/// <param name="TopicToEntityDistance"></param>
		/// <param name="position"></param>
		/// <returns></returns>
		bool TryGetVisualClusterPositionForTopEntity(IEntity entity, double topicEntityWidth, double topicEntityHeight, double TopicToEntityDistance, out Rect position);

		bool TryReadIndex();
	} // interface
} // namesapace
