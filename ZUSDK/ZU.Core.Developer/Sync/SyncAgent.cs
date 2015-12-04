using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.Sync
{
	/// <summary>
	/// Identifeable Synchronization Agent.
	/// </summary>
	public partial class SyncAgent : IComparable, ZU.Sync.ISyncAgent
	{
		public Guid AgentId
		{
			get;
			internal set;
		}

		public SyncAgent()
		{
			this.AgentId = Guid.NewGuid();
		}

		public SyncAgentKind AgentKind
		{
			get;
			set;
		}

		public virtual object Synchronize()
		{
			return null;
		}

		public override int GetHashCode()
		{
			return this.AgentId.GetHashCode();
		}

		public int CompareTo(object obj)
		{
			if (obj != null)
			{
				var agent2 = obj as SyncAgent;

				if (agent2 != null && this.AgentId != null && agent2.AgentId != null)
				{
					// is id enough? it is guid, so should be enough
					if (this.AgentId == agent2.AgentId)
						return 0;

					else
						return 1;
				}
				else
					return -1;
			}
			return -1;
		}

		public override bool Equals(object obj)
		{
			if (obj != null)
			{
				var agent2 = obj as SyncAgent;

				if (agent2 != null && this.AgentId != null && agent2.AgentId != null)
				{
					// is id enough? it is guid, so should be enough
					if (this.AgentId == agent2.AgentId)
						return true;
				}
			}
			return false;
		}
	} // class
} // namespace
