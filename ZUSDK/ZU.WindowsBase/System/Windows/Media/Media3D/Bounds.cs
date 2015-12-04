using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Media.Media3D
{
	[Serializable]
	public struct Bounds
	{
		private Vector3D m_Center;

		private Vector3D m_Extents;
		private Size3D m_size;
		private Point3D m_location;
		private DateTime m_change;
		private DateTime m_death;

		public Point3D Location
		{
			get { return m_location; }
		}

		public double X
		{
			get
			{
				return m_location.X;
			}
		}

		public double Y
		{
			get
			{
				return m_location.Y;
			}
		}

		public double Z
		{
			get
			{
				return m_location.Z;
			}
		}

		public double SizeX
		{
			get
			{
				return this.Size.X;
			}
		}

		public double SizeY
		{
			get
			{
				return this.Size.Y;
			}
		}

		public double SizeZ
		{
			get
			{
				return this.Size.Z;
			}
		}


		public Vector3D Center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}



		public Vector3D Extents
		{
			get
			{
				return this.m_Extents;
			}
			set
			{
				this.m_Extents = value;
			}
		}

		public TimeSpan TimeLength
		{
			get
			{
				return new TimeSpan((long)this.SizeZ);
			}
		}

		public DateTime TLChange
		{
			get
			{
				return this.m_change;
			}
		}

		public DateTime TLDeath
		{
			get
			{
				return this.m_death;
			}
		}

		public Vector3D Max
		{
			get
			{
				return this.Center + this.Extents;
			}
			set
			{
				this.SetMinMax(this.Min, value);
			}
		}

		public Vector3D Min
		{
			get
			{
				return this.Center - this.Extents;
			}
			set
			{
				this.SetMinMax(value, this.Max);
			}
		}



		public Size3D Size
		{
			get
			{
				return this.m_size;
			}
			set
			{
				this.m_size = value;
			}
		}

		public Bounds(Vector3D center, Vector3D size)
		{
			// TLChange, TLDeath
			var change = center.Z - size.Z / 2;
			var death = size.Z; // death is always the max of the boundary

			if (death > 0 && change > 0 && death > change)
			{
				if (death >= DateTime.MaxValue.Ticks)
					m_death = DateTime.MaxValue;
				else
					m_death = new DateTime((long)death);

				if (change <= DateTime.MinValue.Ticks)
					m_change = DateTime.MinValue;
				else
					m_change = new DateTime((long)change);
			}
			else
			{
				m_death = DateTime.MaxValue;
				m_change = DateTime.MinValue;
			}

			this.m_Center = center;
			this.m_Extents = size * 0.5f;

			// we should also convert our 3D vector into Size3D for compatibility with Rect3D
			var size3D = (Size3D)size; // we use explicit conversion

			// saving for future use
			this.m_size = size3D;

			this.m_location = new Point3D(center.X - m_size.X / 2, center.Y - m_size.Y / 2, center.Z - m_size.Z / 2);
		}

		public Bounds(Vector3D center, Vector3D size, bool defineTime)
		{
			// TLBirth, TLDeath
			var birth = center.Z - size.Z / 2;
			var death = birth + size.Z; // death is always the max of the boundary, so it is sum of beginning and length

			if (defineTime)
			{
				if (death >= DateTime.MaxValue.Ticks)
					m_death = DateTime.MaxValue;
				else
					m_death = new DateTime((long)death);

				if (birth <= DateTime.MinValue.Ticks)
					m_change = DateTime.MinValue;
				else
					m_change = new DateTime((long)birth);
			}
			else
			{
				m_death = DateTime.MaxValue;
				m_change = DateTime.MinValue;
			}

			this.m_Center = center;
			this.m_Extents = size * 0.5f;

			// we should also convert our 3D vector into Size3D for compatibility with Rect3D
			var size3D = (Size3D)size; // we use explicit conversion

			// saving for future use
			this.m_size = size3D;

			this.m_location = new Point3D(center.X - m_size.X / 2, center.Y - m_size.Y / 2, center.Z - m_size.Z / 2);





		}

		/// <summary>
		/// Returns Rect3D
		/// </summary>
		/// <returns></returns>
		public Rect3D ToRect3D()
		{
			return new Rect3D(this.Location, this.Size);
		}

		/// <summary>
		/// Returns Rect from (X, Y) 2-dimensional part of this spatio-temporal space. 
		/// </summary>
		/// <returns></returns>
		public Rect ToRect()
		{
			return new Rect(this.Location.X, this.Location.Y, this.SizeX, this.SizeY);
		}


		public bool Intersects(Bounds bounds)
		{
			return (this.Min.X > bounds.Max.X || this.Max.X < bounds.Min.X || this.Min.Y > bounds.Max.Y || this.Max.Y < bounds.Min.Y || this.Min.Z > bounds.Max.Z ? false : this.Max.Z >= bounds.Min.Z);
		}

		public void SetMinMax(Vector3D min, Vector3D max)
		{
			this.Extents = (max - min) * 0.5f;
			this.Center = min + this.Extents;
		}


		public bool Contains(Vector3D vector3D)
		{
			return this.Contains(vector3D.X, vector3D.Y, vector3D.Z);
		}

		public bool Contains(double x, double y, double z)
		{
			return this.ContainsInternal(x, y, z);
		}

		private bool ContainsInternal(double x, double y, double z)
		{
			if (x < this.X || x > this.X + this.SizeX || y < this.Y || y > this.Y + this.SizeY || z < this.Z)
			{
				return false;
			}
			return z <= this.Z + this.SizeZ;
		}

		#region IComparable and Equals


		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (!(obj is Bounds))
				return false;

			var bounds = (Bounds)obj;

			if (!this.X.Equals(bounds.X) || !this.Y.Equals(bounds.Y) || !this.Z.Equals(bounds.Z) || !this.SizeX.Equals(bounds.SizeX) || !this.SizeY.Equals(bounds.SizeY))
			{
				return false;
			}
			return this.SizeZ.Equals(bounds.SizeZ);
		}

		public override int GetHashCode()
		{
			int hash = 13;
			hash = (hash * 7) + this.SizeX.GetHashCode();

			// then we use SizeY
			hash = (hash * 7) + this.SizeY.GetHashCode();

			// then we use SizeZ
			hash = (hash * 7) + this.SizeZ.GetHashCode();

			// then we use Location
			hash = (hash * 7) + this.Location.GetHashCode();

			// returning
			return hash;
		}
		#endregion
	} // struct
} // namespace
