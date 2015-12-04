using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.Core
{
	public partial class EntityEventArgs : EventArgs
	{
		public IEntity Entity { get; set; }

		public EntityAction Action { get ; set;}

	} // class

	public enum EntityAction
	{
		Added,
		Deleted,
		PropertyChanged
	}
} // namespace
