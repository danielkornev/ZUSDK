namespace ZU
{
	[System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	public class RelationshipMetadataAttribute : System.Attribute
	{
		// See the attribute guidelines at 
		//  http://go.microsoft.com/fwlink/?LinkId=85236
		string description;
		string descriptionFrom;

		/// <summary>
		/// 
		/// </summary>
		public string AppId { get; set; }

		/// <summary>
		/// required for derived class
		/// </summary>
		public RelationshipMetadataAttribute()
		{

		}

		// This is a positional argument
		public RelationshipMetadataAttribute(string description, string descriptionFrom, bool isSystem)
		{
			this.description = description;
			this.descriptionFrom = descriptionFrom;
			this.IsSystem = isSystem;
			// default is empty GUID
			this.AppId = System.Guid.Empty.ToString();
		}

		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		public bool IsSystem
		{
			get;
			set;
		}

		public string DescriptionFrom
		{
			get { return descriptionFrom; }
			set { descriptionFrom = value; }
		}
	} // class
} // namespace