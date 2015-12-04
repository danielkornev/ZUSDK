using System;
namespace ZU.Core.Tasks
{
	public interface IOneTimeTrigger : ITrigger
	{
		DateTime StartAt { get; set; }
	} // interface
} // class
