using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.Semantic.Text
{
	[AttributeUsage(AttributeTargets.Class |
	AttributeTargets.Method | AttributeTargets.Field,
	AllowMultiple = false)]
	public class KindMetadataAttribute : System.Attribute
	{
		private bool isHidden;
		private string kindAliases;
		private string kindDescription;
		private string kindParent;
		private bool isAbstract;

		public KindMetadataAttribute(
			string lcKindAliases, string lcKindDescription, 
			string lcKindParent, bool isHidden = false, bool isAbstract = false)
		{
			this.kindAliases = lcKindAliases;
			this.kindDescription = lcKindDescription;
			this.kindParent = lcKindParent;
			this.isHidden = isHidden;
			this.IsAbstract = isAbstract;
		}

		public bool IsHidden
		{
			get
			{
				return isHidden;
			}

			set
			{
				isHidden = value;
			}
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

		public string KindParent
		{
			get
			{
				return kindParent;
			}

			set
			{
				kindParent = value;
			}
		}

		public bool IsAbstract
		{
			get
			{
				return isAbstract;
			}

			set
			{
				isAbstract = value;
			}
		}
	} // class
} // namespace
