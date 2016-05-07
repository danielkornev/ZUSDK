using System;
namespace ZU.Core
{
	/// <summary>
	/// This interface supports Zet Universe and is not intended to be used directly from your code.
	/// </summary>
	public interface ITimelined
	{
		void Merge(ITimelined newVer);
		DateTime TLBirth { get; set; }
		DateTime TLDeath { get; set; }
		ZU.Semantic.EntityRef UIdChanger { get; set; }
		ZU.Semantic.EntityRef UIdOwner { get; set; }
	} // interface
} // namespace
