using System;
namespace ZU.Sync
{
	public interface ISyncAgent
	{
		Guid AgentId { get; }
		SyncAgentKind AgentKind { get; set; }
		int CompareTo(object obj);
		bool Equals(object obj);
		int GetHashCode();
		object Synchronize();
	} // interface
} // namespace
