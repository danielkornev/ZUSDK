using System;
namespace ZU.Core
{
	public interface IUserAction
	{
		System.Collections.Generic.Dictionary<string, string> Args { get; set; }
		string EntityUri { get; set; }
		DateTime LastModifiedTime { get; set; }
		ZU.Semantic.Core.SemanticActionKinds SemanticAction { get; set; }
		string ToShortString();
		string ToString();
		string ToString(DateTime lastModified);
		string UID { get; set; }

		
	} // interface

	public delegate void OnProjectSpaceChangedEventHandler(object sender, EntityEventArgs e);
} // namespace
