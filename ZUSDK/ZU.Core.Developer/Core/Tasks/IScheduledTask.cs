using System;
namespace ZU.Core.Tasks
{
	public interface IScheduledTask
	{
		int CompareTo(object obj);
		bool Equals(object obj);
		bool IsAsync { get; set; }
		bool IsEnabled { get; set; }
		object Owner { get; }
		System.Windows.Threading.DispatcherPriority Priority { get; }
		Action Task { get; }
		Guid TaskId { get; }
		string Title { get; set; }
		ITrigger Trigger { get; }
	}
}
