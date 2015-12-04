//using Lucene.Net.Index;
//using ZU.Semantic.Keyphrases;

//namespace Lucene.Net.LukeNet
//{
//	public class TermInfo
//	{
//		public int DocFreq
//		{
//			get;
//			private set;
//		}

//		public Term Term
//		{
//			get;
//			private set;
//		}

//		public Keyphrase ToKeyphrase()
//		{
//			Keyphrase k = new Keyphrase();
//			k.Phrase = Term.Text;
//			k.Rank = DocFreq;
//			k.DocFreq = 1; // docfreq is 1 by default

//			return k;
//		}

//		public TermInfo(Term term, int docFrequency)
//		{
//			Term = term;
//			DocFreq = docFrequency;
//		}
//	}
//}