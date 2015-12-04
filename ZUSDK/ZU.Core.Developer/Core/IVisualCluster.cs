using System;
namespace ZU.Core
{
	public interface IVisualCluster
	{
		void Add(Entity entity);
		int CompareTo(object obj);
		System.Collections.ObjectModel.ObservableCollection<Entity> Entities { get; set; }
		bool Equals(object obj);
		bool IsKnown { get; }
		bool IsTopicNew { get; set; }
		DateTime LastModifiedTime { get; set; }
		void Remove(Entity entity);
		string Title { get; set; }
		Entity Topic { get; set; }
		string ToString();
	} // interface
} // namespace
