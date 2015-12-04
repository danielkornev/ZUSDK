using System;
namespace ZU.Semantic.Keyphrases
{
	public interface ISentence
	{
		System.Collections.Generic.List<IWord> Words { get; set; }
	} // interface
} // namespace
