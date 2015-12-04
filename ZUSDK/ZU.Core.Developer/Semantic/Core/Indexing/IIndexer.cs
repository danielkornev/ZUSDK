using System;
using ZU.Core;

namespace ZU.Semantic.Core.Indexing
{
	public interface IIndexer : IDisposable
	{
		bool TryBuildIndex();
		bool TryUpdateIndex(IEntity entity);
	} // interface
} // namespace
