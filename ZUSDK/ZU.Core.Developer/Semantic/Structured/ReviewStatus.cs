using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZU.Core.Structured
{
	public enum ReviewStatus
	{
		None = 0, // nothing
		InProgress = 1, // yellow
		ActionRequired = 2, // red
		Done = 3// green
	}
}
