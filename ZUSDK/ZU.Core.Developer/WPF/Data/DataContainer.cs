using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Serialization;
using ZU.Core;

namespace ZU.WPF.Data
{
	/// <summary>
	/// Used for drag-n-drop from typical lists.
	/// </summary>
	[Serializable]
	public class DataContainer
	{
		public List<string> EntitiesIds { get; internal set; }
		public DataAction Action { get; internal set; }

		public string ModelBase { get; internal set; }

		public Point Offset { get; internal set; }

		public double Scale { get; internal set; }

		public System.Windows.Input.InputType InputType { get; internal set; }

		//[XmlIgnore] // this is questionable...
		public IEntityList ParentList { get; set; }

		public DataContainer(DataAction action, List<string> entitiesIds, string modelBase, Point offset, double scale)
		{
			this.Action = action;
			this.EntitiesIds = entitiesIds;
			this.ModelBase = modelBase;
			this.Offset = offset;
			this.Scale = scale;
			// by default
			this.InputType = System.Windows.Input.InputType.Mouse;
		}

		public DataContainer(DataAction action, List<string> entitiesIds, string modelBase, Point offset, double scale, System.Windows.Input.InputType inputType)
		{
			this.Action = action;
			this.EntitiesIds = entitiesIds;
			this.ModelBase = modelBase;
			this.Offset = offset;
			this.Scale = scale;
			this.InputType = inputType;
		}
	} // class
} // namespace
