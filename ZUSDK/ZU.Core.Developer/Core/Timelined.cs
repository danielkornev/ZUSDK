//using System;
//using ZU.Semantic;

//namespace ZU.Core
//{
//	///<summary>
//	/// Base for object with timilined changes
//	///</summary>
//	public partial class Timelined : BasePropertyChanged, ITimelined
//	{
//		///<summary>
//		/// Date of Birth
//		///</summary>
//		public DateTime TLBirth
//		{
//			get
//			{
//				return _tlBirth;
//			}
//			set
//			{
//				SetProperty(value, ref _tlBirth, "TLBirth");
//			}
//		}
//		///<summary>
//		/// Date of Death
//		///</summary>
//		public DateTime TLDeath
//		{
//			get
//			{
//				return _tlDeath;
//			}
//			set
//			{
//				SetProperty(value, ref _tlDeath, "TLDeath");
//			}
//		}

//		///<summary>
//		/// User id -- created by
//		///</summary>
//		public EntityRef UIdOwner
//		{
//			get
//			{
//				return _uidOwner;
//			}
//			set
//			{
//				SetProperty(value, ref _uidOwner, "UIdOwner");
//			}
//		}

//		///<summary>
//		/// User id -- changed by
//		///</summary>
//		public EntityRef UIdChanger
//		{
//			get
//			{
//				return _uidChanger;
//			}
//			set
//			{
//				SetProperty(value, ref _uidChanger, "UIdChanger");
//			}
//		}

//	#region Fields
//		DateTime _tlBirth;
//		DateTime _tlDeath = DateTime.MaxValue;
//		EntityRef _uidOwner = new EntityRef();
//		EntityRef _uidChanger = new EntityRef();
//	#endregion

//		public virtual void Merge(Timelined newVer)
//		{
//			if (newVer!=null)
//			{
//				this.TLBirth = newVer.TLBirth;
//				this.TLDeath = newVer.TLDeath;
//				this.UIdOwner = newVer.UIdOwner;
//				this.UIdChanger = newVer.UIdChanger;
//			}
//		}//Merge()

//	}//class
//}//namespace
