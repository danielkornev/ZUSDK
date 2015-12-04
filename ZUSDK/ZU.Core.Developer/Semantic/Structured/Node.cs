using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ZU.Core.Structured
{
	public partial class Node : BasePropertyChanged
	{
		public Model Model
		{ get; set; }

		public ObservableCollection<VisualCluster> VisualClusters
		{
			get
			{
				return this._clusters;
			}
			set
			{
				SetProperty(value, ref _clusters, "VisualClusters");
			}
		}

		public IQueryable<Entity> Entities
		{
			get
			{
				if (this.Model != null)
				{
					return this.Model.Entities
					.Where(e => e.IsLive)
					.Where(e => e.Kind == Constants.Kinds.GenericFile || e.Kind == Constants.Kinds.WebPage || e.Kind == Constants.Kinds.Space)
					 .AsQueryable<Entity>();
				}
				else
					return new ObservableCollection<Entity>().AsQueryable<Entity>();
				//return _entities;
			}
			//set
			//{
			//	SetProperty(value, ref _entities, "Entities");
			//}
		}

		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				SetProperty(value, ref _title, "Title");
			}
		}


		private ObservableCollection<VisualCluster> _clusters;
		private string _title;


		public bool Filter(object item)
		{
			bool result = false;

			var entity = item as Entity;

			if (entity!=null)
			{
				if (entity.IsLive)
				{
					if (entity.Kind == Constants.Kinds.GenericFile 
						|| 
						entity.Kind == Constants.Kinds.WebPage 
						|| 
						entity.Kind == Constants.Kinds.Email
						||
						entity.Kind == Constants.Kinds.Space)
					{
						result = true;
					}
				}
			}

			return result;
		}
	}
}
