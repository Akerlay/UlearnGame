using System;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace UlearnGame.Model
{
	public class Map
	{
		private GameObject[,] map = MapFactory.CreateMap(@"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
W                             W
W  P                          W
W                             W
W                             W
W                             W
W                             W
W                             W
W                             W
W                             W
W                             W
W                            EW
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");

		public int Width => map.GetLength(0);
		public int Height => map.GetLength(1);
		public GameObject this[int i, int j] => map[i, j];
		public Point PlayerPosition;
		public Player Player;

		public Map()
		{
			PlayerPosition = FindPlayerPosition();
			Player = map[PlayerPosition.X, PlayerPosition.Y] as Player;
			var a = 0;
		}

		private Point FindPlayerPosition()
		{
			for (var x = 0; x < Width; x++)
			for (var y = 0; y < Height; y++)
				if (map[x, y] is Player)
					return new Point(x, y);
			throw new Exception("Invalid map");
		}
		
		private Point FindPlayerObj()
		{
			for (var x = 0; x < Width; x++)
			for (var y = 0; y < Height; y++)
				if (map[x, y] is Player)
					return new Point(x, y);
			throw new Exception("Invalid map");
		}

		public void MovePlayer(Direction dir)
		{
			var oldPlayerPos = PlayerPosition;
			switch (dir)
			{
				case Direction.Up:
					PlayerPosition = MovePlayer(oldPlayerPos, new Point(PlayerPosition.X, PlayerPosition.Y - 1));
					break;
				case Direction.Down:
					PlayerPosition = MovePlayer(oldPlayerPos, new Point(PlayerPosition.X, PlayerPosition.Y + 1));

					break;
				case Direction.Left:
					PlayerPosition = MovePlayer(oldPlayerPos, new Point(PlayerPosition.X - 1, PlayerPosition.Y));
	
					break;
				case Direction.Right:
					PlayerPosition = MovePlayer(oldPlayerPos, new Point(PlayerPosition.X + 1, PlayerPosition.Y));
						
					break;
			}
		}

		private Point MovePlayer(Point oldPlayerPos, Point destination)
		{
			if (IsCorrectMove(destination))
			{
				map[oldPlayerPos.X, oldPlayerPos.Y] = null;
				map[destination.X, destination.Y] = Player;
				return destination;
			}

			return oldPlayerPos;
		}

		private bool IsCorrectMove(Point point) => !(map[point.X, point.Y] is Wall);
	}
}