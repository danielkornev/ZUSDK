using System;
namespace ZU.Semantic.Keyphrases
{
	public interface IKeyphrase
	{
		double CalculatedRank { get; }
		int CompareTo(object obj);
		int DocFreq { get; set; }
		bool Equals(object obj);
		bool IsClusterLevelKeyphrase { get; set; }
		bool IsLive { get; set; }
		string Phrase { get; set; }
		double Rank { get; set; }
		ZU.Semantic.Core.EntityFragmentState State { get; set; }
	} // interface
} // namespace
