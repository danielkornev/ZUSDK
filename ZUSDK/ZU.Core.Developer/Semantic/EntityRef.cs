using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZU.Core;
using ZU.Semantic.Core;

namespace ZU.Semantic
{
	/// <summary>
	/// Reference to Entity. This is a 1:1 relationship.
	/// </summary>
	public partial class EntityRef : IComparable
	{
		#region Properties
		/// <summary>
		/// Entity's Model Id
		/// </summary>
		public string ModelId
		{
			get;
			set;
		}

		/// <summary>
		/// Confidence
		/// </summary>
		public double Confidence
		{ 
			get; 
			set;
		}

		/// <summary>
		/// Entity's Id
		/// </summary>
		public string EntityId
		{
			get;
			set;
		}

		/// <summary>
		/// Relation: e.g., "Is Author Of", or "Is Sent By", etc.
		/// As we want the system to be extendable, we should not specify kinds of things using hard coding, but by using strings (via the Constants)
		/// </summary>
		public string Relation
		{
			get;
			set;
		}

		public DateTime TLChange
		{
			get;
			set;
		}

		/// <summary>
		/// This allows us to cheaply identify not only if this fragment was extracted automatically or created manually,
		/// but also identify if it is deleted or not
		/// </summary>
		public EntityFragmentState State
		{
			get;
			set;
		}

		/// <summary>
		/// Direction: To or From 
		/// To means that relationship starts on this entity (THIS entity "is ..." for REFERENCED entity)
		/// From means that relationship starts on referenced entity (REFERENCED entity "is ..." for THIS entity)
		/// (e.g., "Referenced Entity" "Is Author Of" "another entity", or "another entity" "Is Sent By" "Referenced Entity")
		/// </summary>
		public RelationDirection Direction
		{
			get;
			set;
		}
		#endregion

		#region Implementation


		public override int GetHashCode()
		{
			// as per http://stackoverflow.com/questions/371328/why-is-it-important-to-override-gethashcode-when-equals-method-is-overridden
			int hash = 13;
			hash = (hash * 7) + this.EntityId.GetHashCode();

			hash = (hash * 7) + this.ModelId.GetHashCode();

			return hash;
		}
		
		public int CompareTo(object obj)
		{
			if (obj != null)
			{
				var entityRef2 = obj as EntityRef;

				if (entityRef2 != null && this.ModelId != null && entityRef2.EntityId != null)
				{
					// is id enough? it is guid, so should be enough
					if (this.EntityId == entityRef2.EntityId && this.ModelId == entityRef2.ModelId)
						return 0;

					else
						return 1;
				}
				else
					return -1;
			}
			return -1;
		}

		public bool Equals(IEntity entity)
		{
			if (entity!=null && entity.ModelContext!=null && !string.IsNullOrEmpty(entity.ModelContext.Id))
			{
				var entityRef2 = new EntityRef { EntityId = entity.Id, ModelId = entity.ModelContext.Id };

				return Equals(entityRef2);
			}

			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj != null)
			{
				var entityRef2 = obj as EntityRef;

				if (entityRef2 != null && this.ModelId != null && entityRef2.EntityId != null)
				{
					// is id enough? it is guid, so should be enough
					if (this.EntityId == entityRef2.EntityId && this.ModelId == entityRef2.ModelId)
						return true;
				}
			}

			return false;
		}
		#endregion


		//[Newtonsoft.Json.JsonIgnore]
		public bool IsEmpty()
		{
			//get
			//{
				// this check is enough, actually
				if (string.IsNullOrEmpty(this.EntityId))
					return true;

				return false;
			//}
		}

		
	} // class
} // namespace
