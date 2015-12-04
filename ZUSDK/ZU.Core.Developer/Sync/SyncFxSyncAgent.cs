using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.Sync
{
	public class SyncFxSyncAgent : SyncAgent
	{
		public dynamic Orchestrator { get; set; }

		public override object Synchronize()
		{
			return this.Orchestrator.Synchronize();
		}

		public SyncFxSyncAgent(dynamic SyncOrchestrator)
		{
			this.Orchestrator = SyncOrchestrator;
			this.AgentKind = SyncAgentKind.SyncFramework; // it's a Sync Framework-based Sync Agent
		}
	} // class
} // namespace
