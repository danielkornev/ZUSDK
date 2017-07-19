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
		private string kindDisplayNameSingular;
		private string kindParent;
		private bool isDataOrganizationalType;
		private bool isAbstract;
		private ZoomableSpaceTemplateShapes shape;
        private string kindDisplayNamePlural;

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
			string lcKindAliases, string lcKindDisplayNameSingular, string lcKindDisplayNamePlural, 
			string lcKindParent, bool isHidden = false, bool isAbstract = false, ZoomableSpaceTemplateShapes shape = ZoomableSpaceTemplateShapes.Square, bool isDataOrganizationType = false)
		{
			this.kindAliases = lcKindAliases;
			this.kindDisplayNameSingular = lcKindDisplayNameSingular;
            this.kindDisplayNamePlural = lcKindDisplayNamePlural;
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

		public string FullTextAliases
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
        
        public string DisplayNamePlural
        {
            get
            {
                return this.kindDisplayNamePlural;
            }
            set
            {
                this.kindDisplayNameSingular = value;
            }
        }

        public string DisplayNameSingular
        {
			get
			{
				return this.kindDisplayNameSingular;
			}
			set
			{
				this.kindDisplayNameSingular = value;
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

        public bool IsCustomOntologyType { get; set; }
    } // class
} // namespace
