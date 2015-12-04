using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.Core
{
	public partial interface ISystemInformationModel
	{
		#region Properties
		
		#endregion

		#region Methods
		void AddScheduledTask(ZU.Core.Tasks.IScheduledTask task);
		bool TryRemoveScheduledTask(Guid taskId);
		void StopScheduledTasksExecution();

		#endregion 
	} // interface 
} // namespace
