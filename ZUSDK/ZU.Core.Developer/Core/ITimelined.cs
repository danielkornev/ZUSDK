using System;
namespace ZU.Core
{
	public interface ITimelined
	{
		void Merge(ITimelined newVer);
		DateTime TLBirth { get; set; }
		DateTime TLDeath { get; set; }
		ZU.Semantic.EntityRef UIdChanger { get; set; }
		ZU.Semantic.EntityRef UIdOwner { get; set; }
	} // interface
} // namespace
