using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZU.Semantic.Core;

namespace ZU.Core
{
	public partial class UserAction : ZU.Core.IUserAction
	{
		public DateTime LastModifiedTime { get; set; }
		public string UID { get; set; }
		public SemanticActionKinds SemanticAction { get; set; }
		public Dictionary<string, string> Args { get; set; }

		public string EntityUri { get; set; }

		public UserAction()
		{
			this.Args = new Dictionary<string, string>();
		}

		public override string ToString()
		{
			string s = "";
			s = Constants.GetDescription(this.SemanticAction);

			if (this.LastModifiedTime.ToLocalTime().Date == DateTime.Today.ToLocalTime().Date)
				return s + " at " + this.LastModifiedTime.ToShortTimeString();

			else
				return s + " on " + this.LastModifiedTime.ToShortDateString();
		}

		public string ToShortString()
		{
			string s = "";
			s = Constants.GetDescription(this.SemanticAction);

			return s;
		}
	}//class

	public delegate void OnProjectSpaceChangedEventHandler(object sender, EntityEventArgs e);
}//namespace
