using System;
namespace ZU.Core
{
	public interface IProperty : ITimelined
	{
		string AppId { get; set; }
		double Confidence { get; set; }
		string Id { get; set; }
		object Value { get; set; }
		double Weight { get; set; }
	} // interface
} // namespace
