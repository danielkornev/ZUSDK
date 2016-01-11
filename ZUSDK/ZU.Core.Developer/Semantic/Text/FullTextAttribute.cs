using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.Semantic.Text
{
	[AttributeUsage(AttributeTargets.Class |
	AttributeTargets.Method,
	AllowMultiple = false)]
	public class FullTextAttribute : System.Attribute
	{
		private string kindAliases;
		private string kindDescription;

		public FullTextAttribute(string lcKindAliases, string lcKindDescription)
		{
			this.kindAliases = lcKindAliases;
			this.kindDescription = lcKindDescription;
		}

		public string KindAliases
		{
			get
			{
				return this.kindAliases;
			}
			set
			{
				this.kindAliases = value;
			}
		}

		public string KindDescription
		{
			get
			{
				return this.kindDescription;
			}
			set
			{
				this.kindDescription = value;
			}
		}
	} // class
} // namespace
