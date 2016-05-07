using System;
namespace ZU.Semantic.Keyphrases
{
	/// <summary>
	/// This interface supports Zet Universe and is not intended to be used directly from your code.
	/// </summary>
	public interface IParagraph
	{
		System.Collections.Generic.List<ISentence> Sentences { get; set; }
	} // interface
} // namespace
