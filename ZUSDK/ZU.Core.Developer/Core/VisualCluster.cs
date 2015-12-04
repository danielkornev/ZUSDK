using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ZU.Core
{

	public class VisualCluster : BasePropertyChanged
	{
		//public event EventHandler Changed;

		//public string Id { get; set; }

		public IEntity Topic
		{
			get
			{
				return _topic;
			}
			set
			{
				SetProperty(value, ref _topic, "Topic");

				if (_topic != null)
				{
					Title = _topic.Title;

					_topic.PropertyChanged += _topic_PropertyChanged;
				}
			}
		}

		void _topic_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Title")
			{
				if (_topic != null)
				{
					Title = _topic.Title;
				}
			}

			// we also update TL Change
			this.LastModifiedTime = Topic.TLChange;
		}

		public bool IsTopicNew { get; set; }

		public ObservableCollection<IEntity> Entities 
		{ 
			get
			{
				return this._entities;
			}
			set
			{
				SetProperty(value, ref _entities, "Entities");
			}
		}

		public string Title
		{
			get
			{
				//if (Topic != null)
				//	return Topic.Title;

				//else
				//	if (this.Id != null)
				//		return this.Id.ToString();
				//	else
				//		return "Visual Cluster [" + Guid.NewGuid().ToString() + "]"; 
				return _title;
			}
			set
			{
				SetProperty(value, ref _title, "Title");
			}
		}

		public DateTime LastModifiedTime
		{
			get
			{
				return _tlChange;
			}
			set
			{
				SetProperty(value, ref _tlChange, "LastModifiedTime");
			}
		}

		public bool IsKnown
		{
			get
			{
				if (this.Topic!=null)
					return Constants.KnownEntities.IsKnown(this);
				return false;
			}
		}

		#region Fields
		private ObservableCollection<IEntity> _entities;
		private IEntity _topic;
		private string _title;
		private DateTime _tlChange;
		#endregion

		public VisualCluster()
		{
			this.Topic = null;
			this.Title = "Visual Cluster";
			// initializing entities collection
			this._entities = new ObservableCollection<IEntity>();
		}

		public void Add(IEntity entity)
		{
			if (entity.Kind != Constants.Kinds.VisualCluster)
				if (this.Entities != null)
				{
					var existEnt = this.Entities.FirstOrDefault(e => e.Id == entity.Id);
					if (existEnt == null)
					{
						this.Entities.Add(entity);

						// setting request to recalculate keyphrases; for that all we need is to say that Keyphrases has been changed
						
						// we do this for the given entity. This won't lead to any change of the keyphrases for the entity
						// but will trigger recalculation of keyphrases for the entire visual cluster
						entity.Changed(Constants.Indexers.FullTextSearchIndexer.Keyphrases); 

						// what if?
						// we update high-freq keyphrases
						//this.Topic.ModelContext.UID.SIM.UpdateHighFreqKeyphrasesForVisualCluster(this.Topic);

						// Updating Last Changed Value as needed
						entity.PropertyChanged +=
						(s, e) =>
						{
							if (e.PropertyName == "TLChange")
							{
								//System.Windows.MessageBox.Show("Entity '" + entity.Title + "' changed at '" + DateTime.Now + "'");
								this.LastModifiedTime = DateTime.UtcNow; // what should we do with this?
							}
						};

						// Setting Initial Last Changed Value
						var latestEntity = this.Entities.OrderBy(e1 => e1.TLChange).FirstOrDefault();

						if (latestEntity!=null & this.Topic!=null)
						if (latestEntity.TLChange>this.Topic.TLChange)
						{
							this.LastModifiedTime = this.Topic.TLChange;
						}
						else
						{
							this.LastModifiedTime = latestEntity.TLChange;
						}
					}
				}
		}

		public void Remove(IEntity entity)
		{
			if (this.Entities!=null)
			{
				var existEnts = this.Entities.Where(e => e.Id == entity.Id).ToList();

				foreach (var en in existEnts)
				{
					//en.PropertyChanged = null;
					this.Entities.Remove(en);
				}

				// we update high-freq keyphrases
				this.Topic.ModelContext.SIM.UpdateHighFreqKeyphrasesForVisualCluster(this.Topic);
			}
		}

		/// <summary>
		/// Required for visual clusters
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int CompareTo(object obj)
		{
			if (obj != null)
			{
				var vc2 = obj as VisualCluster;

				if (vc2 != null && this.Topic!=null && vc2.Topic!=null)
				{
					if (vc2.Topic.Id == this.Topic.Id)
						return 0;

					else
						return 1;
				}
				else
					return -1;
			}
			return -1;
		}

		public override bool Equals(object obj)
		{
			bool result = false;

			if (obj != null)
			{
				var vc2 = obj as VisualCluster;

				if (vc2 != null)
				{
					if (vc2.Topic != null & this.Topic != null)
					{
						if (vc2.Topic.Id == this.Topic.Id)
						{
							result = true;
						}
					}
				}
			}

			return result;
		}

		public override string ToString()
		{
			if (this.Topic != null)
				return this.Topic.ToString();
			else
				return "Unidentified Visual Cluster";
		}
	} // class
} // namespace
