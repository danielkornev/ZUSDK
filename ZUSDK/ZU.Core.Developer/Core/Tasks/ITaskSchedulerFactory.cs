using System;
using System.Windows.Threading;
namespace ZU.Core.Tasks
{
	public interface ITaskSchedulerFactory
	{
		IOneTimeTrigger CreateOneTimeTrigger(DateTime StartAt);
		IRepeatableTrigger CreateRepeatableTrigger(TimeSpan interval, bool runFirstTime, Func<bool> needsToRun);

		IScheduledTask CreateScheduledTask(Guid taskId, Action task, bool isAsync, object owner, DispatcherPriority priority, ITrigger trigger);
	} // interface
} // namespace
