//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ZU.Core
//{
//	[Serializable]
//	public partial class EntityList : IComparable, ZU.Core.IEntityList
//	{
//		public IEntity ListEntity
//		{
//			get;
//			set;
//		}

//		public RangeObservableCollection<IEntity> Entities
//		{
//			get;
//			set;
//		}

//		public EntityList()
//		{
//			this.Entities = new RangeObservableCollection<IEntity>();
//		}

//		/// <summary>
//		/// Required for visual clusters labeling (effectively an on-the-fly hierarchy creation).
//		/// </summary>
//		/// <param name="obj"></param>
//		/// <returns></returns>
//		public int CompareTo(object obj)
//		{
//			if (obj != null)
//			{
//				var entity2 = obj as EntityList;

//				if (entity2 != null && this.ListEntity.ModelContext.UID!=null && entity2.ListEntity.ModelContext.UID!=null)
//				{
//					// is id enough? it is guid, so should be enough
//					if (this.ListEntity.Id == entity2.ListEntity.Id && this.ListEntity.ModelContext.Id == entity2.ListEntity.ModelContext.Id) 
//						return 0;

//					else
//						return 1;
//				}
//				else
//					return -1;
//			}
//			return -1;
//		}

//		public override bool Equals(object obj)
//		{
//			if (obj != null)
//			{
//				var entity2 = obj as EntityList;

//				if (entity2 != null && this.ListEntity.ModelContext != null && entity2.ListEntity.ModelContext != null)
//				{
//					// is id enough? it is guid, so should be enough
//					if (this.ListEntity.Id == entity2.ListEntity.Id && this.ListEntity.ModelContext.Id == entity2.ListEntity.ModelContext.Id)
//						return true;
//				}
//			}

//			return false;
//		}

//		public override int GetHashCode()
//		{
//			int hash = 13;
//			hash = (hash * 7) + this.ListEntity.Id.GetHashCode();

//			if (this.ListEntity.ModelContext!=null)
//				hash = (hash * 7) + this.ListEntity.ModelContext.Id.GetHashCode();

//			return hash;
//		}
//	} // class
//} // namespace
