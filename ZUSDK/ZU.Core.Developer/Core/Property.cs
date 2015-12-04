using System;
using ZU.Semantic;

namespace ZU.Core
{
	///<summary>
	/// Property id/value pair //(timelined)
	///</summary>
	public partial class Property : BasePropertyChanged, ITimelined
	{
		///<summary>
		/// Property id
		///</summary>
		public string Id
		{
			get
			{
				return _id;
			}
			set
			{
				SetProperty(value, ref _id, "Id");
			}
		}

		///<summary>
		/// Property value
		///</summary>
		public object Value
		{
			get
			{
				return _value;
			}
			set
			{
				SetProperty(value, ref _value, "Value");
			}
		}

		public double Confidence
		{
			get
			{
				return _confidence;
			}
			set
			{
				SetProperty(value, ref _confidence, "Weight");
			}
		}

		public double Weight
		{
			get
			{
				return _weight;
			}
			set
			{
				SetProperty(value, ref _weight, "Value");
			}
		}

		private string _AppId;
		public string AppId
		{
			get { return _AppId; }
			set
			{
				if (value != _AppId)
				{
					_AppId = value;
					SetProperty(value, ref _AppId, "AppId");
				}
			}
		}
		

	#region Fields
		string _id = string.Empty;
		object _value = null;
		double _confidence = 1.0;
		double _weight = 1.0;
	#endregion


		///<summary>
		/// Date of Birth
		///</summary>
		public DateTime TLBirth
		{
			get
			{
				return _tlBirth;
			}
			set
			{
				SetProperty(value, ref _tlBirth, "TLBirth");
			}
		}
		///<summary>
		/// Date of Death
		///</summary>
		public DateTime TLDeath
		{
			get
			{
				return _tlDeath;
			}
			set
			{
				SetProperty(value, ref _tlDeath, "TLDeath");
			}
		}

		///<summary>
		/// User id -- created by
		///</summary>
		public EntityRef UIdOwner
		{
			get
			{
				return _uidOwner;
			}
			set
			{
				SetProperty(value, ref _uidOwner, "UIdOwner");
			}
		}

		///<summary>
		/// User id -- changed by
		///</summary>
		public EntityRef UIdChanger
		{
			get
			{
				return _uidChanger;
			}
			set
			{
				SetProperty(value, ref _uidChanger, "UIdChanger");
			}
		}

		#region Fields
		DateTime _tlBirth;
		DateTime _tlDeath = DateTime.MaxValue;
		EntityRef _uidOwner = new EntityRef();
		EntityRef _uidChanger = new EntityRef();
		#endregion

		public virtual void Merge(ITimelined newVer)
		{
			if (newVer != null)
			{
				this.TLBirth = newVer.TLBirth;
				this.TLDeath = newVer.TLDeath;
				this.UIdOwner = newVer.UIdOwner;
				this.UIdChanger = newVer.UIdChanger;
			}
		}//Merge()
	}//class
}//namespace
