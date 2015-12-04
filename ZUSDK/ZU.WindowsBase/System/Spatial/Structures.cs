using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZU.Semantic.Spatial
{
	#region Structures
	public struct Segment
	{
		public Vector2 p;
		public Vector2 q;

		public bool Contains(SuperPoint point)
		{
			if (p.Equals(point.P) || q.Equals(point.P))
				return true;
			return false;
		}

		public bool ContainsY(float y)
		{
			if (p.Y.Equals(y) || q.Y.Equals(y))
				return true;
			return false;
		}

		public bool ContainsX(float x)
		{
			if (p.X.Equals(x) || q.X.Equals(x))
				return true;
			return false;
		}

		public Vector2 GetCenter()
		{
			var centerPoint = new Vector2((p.X + q.X) / 2, (p.Y + q.Y) / 2);

			return centerPoint;
		}

		public System.Windows.Point GetCenterAsPoint()
		{
			return GetCenter().ToPoint();
		}

		

		public bool IsLowerInY(Segment otherSegment)
		{
			// we could have only two cases: either p1 is lower than p2, and q1 is lower than q2,
			// or q1 is lower than p2, and p1 is lower than q2 (reverse situation)

			if (otherSegment.p.Y > this.p.Y)
			{
				// ok, this.p.Y is lower than the other segment's p.Y
 				if (otherSegment.q.Y > this.q.Y)
				{
					return true;
				}
			}
			// reverse situation
			if (otherSegment.p.Y > this.q.Y)
			{
				// ok, this.q.Y is lower than the other segment's p.Y
				{
					if (otherSegment.q.Y > this.p.X)
					{
						return true;
					}
				}
			}

			return false;
		}

		public override string ToString()
		{
			return string.Format("Segment P: {0}, Q {1}", p, q);
		}

	}

	public struct SuperPoint
	{
		public Vector2 P;
		public int ID;

		public SuperPoint(Vector2 p, int id)
		{
			P = p;
			ID = id;
		}

		public override string ToString()
		{
			return string.Format("SuperPoint P {0}, ID {1}", P, ID);
		}
	}


	#endregion
	public struct Vector2
	{
		public float X;
		public float Y;

		public Vector2(float x, float y)
		{
			this.X = x;
			this.Y = y;
		}

		public bool Equals(Vector2 ob)
		{
			Vector2 c = (Vector2)ob;
			return X == c.X && Y == c.Y;
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("Vector2 X: {0}, Y {1}", X, Y);
		}

		public System.Windows.Point ToPoint()
		{
			return new System.Windows.Point(
				(double)X,
				(double)Y);
		}
	}

	public struct Vector3
	{
		public float X;
		public float Y;
		public float Z;

		public Vector3(float x, float y, float Z)
		{
			this.X = x;
			this.Y = y;
			this.Z = Z;
		}

		public override string ToString()
		{
			return string.Format("Vector3: X {0}, Y {1}, Z {2}.", X, Y, Z);
		}
	}
}
