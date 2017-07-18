using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZU.Semantic.Spatial;

namespace ZU.Semantic.Text
{
	/// <summary>
	/// Metadata for a Kind Definition.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class |
	AttributeTargets.Method | AttributeTargets.Field,
	AllowMultiple = false)]
	public class KindMetadataAttribute : System.Attribute
	{
		private bool isHidden;
		private string kindAliases;
		private string kindDescription;
		private string kindParent;
		private bool isDataOrganizationalType;
		private bool isAbstract;
		private ZoomableSpaceTemplateShapes shape;

		/// <summary>
		/// Creates a new KindMetadataAttribute object.
		/// </summary>
		/// <param name="lcKindAliases"></param>
		/// <param name="lcKindDescription"></param>
		/// <param name="lcKindParent"></param>
		/// <param name="isHidden"></param>
		/// <param name="isAbstract"></param>
		/// <param name="shape"></param>
		public KindMetadataAttribute(
			string lcKindAliases, string lcKindDescription, 
			string lcKindParent, bool isHidden = false, bool isAbstract = false, ZoomableSpaceTemplateShapes shape = ZoomableSpaceTemplateShapes.Square, bool isDataOrganizationType = false)
		{
			this.kindAliases = lcKindAliases;
			this.kindDescription = lcKindDescription;
			this.kindParent = lcKindParent;
			this.isHidden = isHidden;
			this.IsAbstract = isAbstract;
			this.Shape = shape;
			this.isDataOrganizationalType = isDataOrganizationType;
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

		public ZoomableSpaceTemplateShapes Shape
		{
			get
			{
				return shape;
			}
			set
			{
				shape = value;
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

        public bool IsDataOrganizationalType
        {
            get
            {
                return isDataOrganizationalType;
            }
            set
            {
                isDataOrganizationalType = value;
            }
        }
	} // class
} // namespace
