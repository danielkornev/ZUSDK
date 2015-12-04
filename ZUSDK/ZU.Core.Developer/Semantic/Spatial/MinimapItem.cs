//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Windows;

//namespace ZU.Semantic.Spatial
//{
//	public partial class MinimapItem : INotifyPropertyChanged
//	{
//		public double X1 
//		{
//			get
//			{
//				return _x1;
//			}
//			set
//			{
//				_x1 = value;
//				if (PropertyChanged!=null)
//				{
//					PropertyChangedEventArgs args = 
//						new PropertyChangedEventArgs("X1");

//					PropertyChanged(this, args);
//				}
//			}
//		}

//		public double Y1
//		{
//			get
//			{
//				return _y1;
//			}
//			set
//			{
//				_y1 = value;
//				if (PropertyChanged != null)
//				{
//					PropertyChangedEventArgs args =
//						new PropertyChangedEventArgs("Y1");

//					PropertyChanged(this, args);
//				}
//			}
//		}
//		public double X2
//		{
//			get
//			{
//				return _x2;
//			}
//			set
//			{
//				_x2 = value;
//				if (PropertyChanged != null)
//				{
//					PropertyChangedEventArgs args =
//						new PropertyChangedEventArgs("X2");

//					PropertyChanged(this, args);
//				}
//			}
//		}

//		public double Y2
//		{
//			get
//			{
//				return _y2;
//			}
//			set
//			{
//				_y2 = value;
//				if (PropertyChanged != null)
//				{
//					PropertyChangedEventArgs args =
//						new PropertyChangedEventArgs("Y2");

//					PropertyChanged(this, args);
//				}
//			}
//		}
//		public bool IsLive
//		{
//			get
//			{
//				return _isLive;
//			}
//			set
//			{
//				_isLive = value;
//				if (PropertyChanged != null)
//				{
//					PropertyChangedEventArgs args =
//						new PropertyChangedEventArgs("IsLive");

//					PropertyChanged(this, args);
//				}
//			}
//		}
//		public string EntityId { get; set; }
//		public double OffsetX
//		{
//			get
//			{
//				return this.X1;
//			}
//		}

//		public double OffsetY
//		{
//			get
//			{
//				return this.Y1;
//			}
//		}


//		public Rect ToRect()
//		{
//			var width = X2-X1;
//			var height = Y2-Y1;

//			Rect rect = new Rect(X1, Y1, width, height);

//			return rect;
//		}

//		#region Fields
//		double _x1 = 0;
//		double _y1 = 0;
//		double _x2 = 0;
//		double _y2 = 0;
//		bool _isLive = false;
//		#endregion

//		public event PropertyChangedEventHandler PropertyChanged;
//	}
//}
