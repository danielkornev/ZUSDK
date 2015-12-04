using System;
namespace ZU.Semantic.Processors
{
	interface ITopicEntity
	{
		ZU.Core.IEntity Entity { get; set; }
		string Topic { get; set; }
	} // interface
} // namespace
