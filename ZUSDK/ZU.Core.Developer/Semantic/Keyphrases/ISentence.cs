using System;
namespace ZU.Semantic.Keyphrases
{
	/// <summary>
	/// This interface supports Zet Universe and is not intended to be used directly from your code.
	/// </summary>
	public interface ISentence
	{
		System.Collections.Generic.List<IWord> Words { get; set; }
	} // interface
} // namespace
