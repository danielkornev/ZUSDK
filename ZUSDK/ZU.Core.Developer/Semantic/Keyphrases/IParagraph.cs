using System;
namespace ZU.Semantic.Keyphrases
{
	public interface IParagraph
	{
		System.Collections.Generic.List<ISentence> Sentences { get; set; }
	} // interface
} // namespace
