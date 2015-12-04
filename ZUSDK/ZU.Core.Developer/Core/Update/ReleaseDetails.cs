using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.Core.Update
{
	public class ReleaseDetails
	{
		public Version Version
		{ get; set; }

		public string ReleaseNotes
		{ get; set; }

		public string SizeInMB
		{ get; set; }

		public DateTime ReleaseDate
		{ get; set; }
	} // class
} // namespace
