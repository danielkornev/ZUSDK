using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZU.Sync;

namespace ZU.Core
{
	public partial interface ISystemInformationModel
	{
		#region Properties
		#endregion

		#region Methods
		ISyncAgent CreateSyncAgent(SyncAgentKind agentKind);
		void AddSyncAgent(ZU.Sync.ISyncAgent syncAgent, Guid taskId);

		bool HasSyncTaskByKey(ZU.Sync.ISyncAgent syncAgent);

		Guid GetSyncTaskByKey(ZU.Sync.ISyncAgent syncAgent);
		#endregion
	} // interface
} // namespace
