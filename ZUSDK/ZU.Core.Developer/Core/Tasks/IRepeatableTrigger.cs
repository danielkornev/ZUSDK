using System;
namespace ZU.Core.Tasks
{
	public interface IRepeatableTrigger : ITrigger
	{
		TimeSpan Interval { get; }
		bool NeedsToRun { get; }
		bool RunFirstTime { get; }
	} // interface
} // namespace
