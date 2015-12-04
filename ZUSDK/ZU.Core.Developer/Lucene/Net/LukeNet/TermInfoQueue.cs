//using Lucene.Net.Util;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Lucene.Net.LukeNet
//{
//	public sealed class TermInfoQueue<T> : PriorityQueue<T> where T : TermInfo
//	{
//		public TermInfoQueue(int size)
//		{
//			Initialize(size);
//		}

//		public override bool LessThan(T a, T b)
//		{
//			//TermInfo termInfoA = a as TermInfo;
//			//TermInfo termInfoB = b as TermInfo;

//			//if (null == termInfoA || null == termInfoB)
//			//	return false;

//			return a.DocFreq < b.DocFreq;
//		}
//	}
//}
