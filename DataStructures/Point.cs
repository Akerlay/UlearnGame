namespace UlearnGame.DataStructures
{
	public struct Point
	{
		public int X;
		public int Y;

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override bool Equals(object obj) => obj != null && Equals((Point) obj);

		public bool Equals(Point other) => X == other.X && Y == other.Y;

		public override int GetHashCode()
		{
			unchecked
			{
				return (X * 397) ^ Y;
			}
		}

		public static bool operator ==(Point p1, Point p2) => p1.X == p2.X && p1.Y == p2.Y;
		public static bool operator !=(Point p1, Point p2) => !(p1 == p2);

		public static implicit operator System.Drawing.Point(Point point) => new System.Drawing.Point(point.X, point.Y);
		public static implicit operator Point(System.Drawing.Point point) => new Point(point.X, point.Y);
	}
}