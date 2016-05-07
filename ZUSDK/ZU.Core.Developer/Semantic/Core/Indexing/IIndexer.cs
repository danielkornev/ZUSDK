using System;
using ZU.Core;

namespace ZU.Semantic.Core.Indexing
{
	/// <summary>
	/// This interface supports Zet Universe and is not intended to be used directly from your code.
	/// </summary>
	public interface IIndexer : IDisposable
	{
		bool TryBuildIndex();
		bool TryUpdateIndex(IEntity entity);
	} // interface
} // namespace
