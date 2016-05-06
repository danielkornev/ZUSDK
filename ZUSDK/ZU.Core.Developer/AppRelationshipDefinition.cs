using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU
{
	public class AppRelationshipDefinition : RelationshipMetadataAttribute
	{
		public AppRelationshipDefinition()
		{
			AllowedParticipatingKinds = new List<Tuple<string, string>>();
		}

		public string Relation { get; set; }

		public List<Tuple<string, string>> AllowedParticipatingKinds
		{
			get;
			set;
		}
	} // class
} // namespace
