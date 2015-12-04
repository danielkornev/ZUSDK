using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.Core.Update
{
	public enum UpdateCheckStates
	{
		NeverChecked,
		CheckFailed,
		UpdateAvailable,
		NoUpdatesFound,
		RunningLatestVersion,
		UpdateFailed
	} // enum
} // namespace
