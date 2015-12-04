using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ZU.ComponentModel;
using ZU.Core;

namespace ZU.WPF.Data.Schemas
{
	public partial class EntityView : BasePropertyChanged, IComparable, IInstantiateProperty, ZU.WPF.Data.Schemas.IEntityView
	{
		private WeakReference _weakNormalBitmap = new WeakReference(null);

		#region IInstantiateProperty Members

		public void InstantiateProperty(string propertyName, System.Globalization.CultureInfo culture, SynchronizationContext callbackExecutionContext)
		{
			switch (propertyName)
			{
				case "Thumbnail":
					callbackExecutionContext.Post(
						(o) =>
							Changed(propertyName), _weakNormalBitmap.Target ?? (
							_weakNormalBitmap.Target = GetBitmap(this.Entity, false)
							)
							);

					break;

				default:
					break;
			}
		}

		#endregion

		private static ImageSource GetBitmap(IEntity entity, bool getLarge)
		{
			if (EntityThumbnailCacheFactory.Instance == null) return null;

			return EntityThumbnailCacheFactory.Instance.GetThumbnail(entity, getLarge);
		}

		/// <summary>
		/// item thumbnail, may be invoked in UI thread
		/// return DependencyProperty.UnsetValue if WeakReference.Target = null
		/// </summary>
		public object Thumbnail
		{
			get
			{
				return _weakNormalBitmap.Target ?? DependencyProperty.UnsetValue;
			}
		}

		private IEntity _EntitySelf;
		public IEntity Entity
		{
			get { return _EntitySelf; }
			set
			{
				if (value != _EntitySelf)
				{
					SetProperty(value, ref _EntitySelf, "Entity");
				}
			}
		}

		public bool IsSystem
		{
			get;
			set;
		}

		public string Kind
		{
			get
			{
				return this.Entity.Kind;
			}
		}

		private string _ActionName;
		public string ActionName
		{
			get { 
				if (string.IsNullOrEmpty(_ActionName))
				{
					return Constants.Kinds.GetKindDescription(this.Entity.Kind);
				}
				return _ActionName; }
			set
			{
				if (value != _ActionName)
				{
					_ActionName = value;
					SetProperty(value, ref _ActionName, "ActionName");
				}
			}
		}

		private DateTime _TLChange;
		public DateTime TLChange
		{
			get { return _TLChange; }
			set
			{
				if (value != _TLChange)
				{
					_TLChange = value;
					SetProperty(value, ref _TLChange, "TLChange");
				}
			}
		}


		public int CompareTo(object obj)
		{
			if (obj != null)
			{
				var entity2 = obj as EntityView;

				if (entity2 != null && this.Entity!= null && entity2.Entity != null)
				{
					// is id enough? it is guid, so should be enough
					if (this.Entity.SystemWideUniqueId == entity2.Entity.SystemWideUniqueId)
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
			if (obj != null)
			{
				var entity2 = obj as EntityView;

				if (entity2 != null && this.Entity != null && entity2.Entity != null)
				{
					// is id enough? it is guid, so should be enough
					if (this.Entity.SystemWideUniqueId == entity2.Entity.SystemWideUniqueId)
						return true;
				}
			}

			return false;
		}

		public override int GetHashCode()
		{
			int hash = 13;
			hash = (hash * 7) + this.Entity.Id.GetHashCode();

			if (this.Entity.SystemWideUniqueId != null)
				hash = (hash * 7) + this.Entity.SystemWideUniqueId.GetHashCode();

			return hash;
		}
	} // class
} // namespace
