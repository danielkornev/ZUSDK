using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.Semantic
{   
	/// <summary>
	/// This class supports Zet Universe and is not intended to be used directly from your code.
	/// </summary>
	public static class EntityRefExtensions
	{
		public static string IdToShortString(this EntityRef er)
		{
			var id = Guid.Parse(er.EntityId);
			return new ShortGuid(id).ToString();
		}

		public static ShortGuid IdToShortGuid(this EntityRef er)
		{
			var id = Guid.Parse(er.EntityId);
			return new ShortGuid(id);
		}

		public static bool IsLive(this EntityRef er)
		{
			switch (er.State)
			{
				case Core.EntityFragmentState.Deleted_Extracted:
					return false;
				case Core.EntityFragmentState.Deleted_Entered:
					return false;
				case Core.EntityFragmentState.Entered:
					return true;
				case Core.EntityFragmentState.Extracted:
					return true;
				default:
					return false;
			}
		}
	} // class
} // namespace
