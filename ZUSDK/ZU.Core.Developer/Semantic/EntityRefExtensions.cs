using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.Semantic
{
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
	} // class
} // namespace
