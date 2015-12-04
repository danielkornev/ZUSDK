//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using ZU.Core;
//using ZU.Semantic.Core;

//namespace ZU.Semantic.Keyphrases
//{
//	public class Keyphrase : BasePropertyChanged, IComparable, ZU.Semantic.Keyphrases.IKeyphrase
//	{
//		public string Phrase
//		{ get; set; }

//		public string PhraseDebug
//		{ 
//			get
//			{
//				return String.Format(Phrase + " {0}", CalculatedRank);
//			}
//		}

//		public double Rank
//		{
//			get
//			{
//				if (IsClusterLevelKeyphrase)
//				{
//					return rank * DocFreq;
//				}
//				else
//					return rank;
//			}
//			set
//			{
//				rank = value;
//			}
//		}

//		[Newtonsoft.Json.JsonIgnore]
//		public double CalculatedRank
//		{
//			get
//			{
//				return Rank * DocFreq;
//			}
//		}

//		public int DocFreq
//		{ get; set; }

//		/// <summary>
//		/// Non-serializeable
//		/// </summary>
//		[Newtonsoft.Json.JsonIgnore]
//		public bool IsLive
//		{
//			get
//			{
//				//Log.Info("IsLive value is requested for phrase \"{0}\"", this.Phrase);

//				var res = false;

//				if (State == EntityFragmentState.Deleted_Entered
//					||
//					State == EntityFragmentState.Deleted_Extracted)
//					res = false;
//				else
//					res = true;

//				//Log.Info("IsLive value for phrase \"{0}\" is: {1}", this.Phrase, res);

//				return res;
//			}
//			set
//			{
//				//
//				Changed("IsLive");
//			}
//		}

//		/// <summary>
//		/// It tells two things about given keyphrase's space: deleted/not-deleted, and entered/extracted
//		/// </summary>
//		public EntityFragmentState State
//		{
//			get
//			{
//				return state;
//			}
//			set
//			{
//				this.state = value;
				
//				// whatever, right?
//				IsLive = false;
//			}
//		}

//		public bool IsClusterLevelKeyphrase
//		{
//			get;
//			set;
//		}

//		public int CompareTo(object obj)
//		{
//			if (obj != null)
//			{
//				var kp2 = obj as Keyphrase;

//				if (kp2 != null && !string.IsNullOrEmpty(this.Phrase) && !string.IsNullOrEmpty(kp2.Phrase))
//				{
//					// is phrase enough? should be enough... asking for ranking seems stupid
//					if (this.Phrase.ToLower() == kp2.Phrase.ToLower())
//					{
//						if (this.Rank == kp2.Rank)
//							return 0;
//						else
//							return Convert.ToInt32(this.Rank - kp2.Rank);
//					}
//				}
//			}
//			return -1;
//		}

//		public override bool Equals(object obj)
//		{
//			if (obj != null)
//			{
//				var kp2 = obj as Keyphrase;

//				if (kp2 != null && !string.IsNullOrEmpty(this.Phrase) && !string.IsNullOrEmpty(kp2.Phrase))
//				{
//					// is id enough? it is guid, so should be enough
//					if (this.Phrase.ToLower() == kp2.Phrase.ToLower() && this.Rank == kp2.Rank)
//						return true;
//				}

//				//if (entity2 != null)
//				//{
//				//	if (!string.IsNullOrEmpty(entity2.Id))
//				//	{
//				//		if (!string.IsNullOrEmpty(this.Id))
//				//		{
//				//			if (entity2.Id == this.Id)
//				//				result = true;
//				//		}
//				//	}
//				//}
//			}

//			return false;
//		}

//		#region Implementation
//		// extracted is default state
//		private EntityFragmentState state = EntityFragmentState.Extracted;
//		private double rank;
//		#endregion
//	} // class
//} // namespace
