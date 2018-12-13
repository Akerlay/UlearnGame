using System;
using UlearnGame.DataStructures;
using UlearnGame.Model.GameObjects;


namespace UlearnGame.Model
{
	public class Map
	{
		public Point PlayerPosition;
		public Player Player => this[PlayerPosition] as Player;
		private GameObject[,] map;

		public Map(string mapString)
		{
			map = MapFactory.CreateMap(this, mapString);
			PlayerPosition = FindPlayerPosition();
		}
		
		public Map()
		{
			//Можно читать из файла, не стал усложнять
			map = MapFactory.CreateMap(this, @"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
WPW             W   E         W
W WWWW   WWWW K W   WWWWWWWWWCW
W W HW   W  WWWWWW     WWWWWWWW
W W  W   W  W    W     W   WH W
W W      W  W    W         WW W
W WWWWWW W WW    W     W      W
W           *    W     WWWW   W
WWWWWWWW W       W     W      W
W   W    W       WWWWWWW      W
W WBW    W       W     W      W
W WWW    W       W            W
W        W             W      W
WWWWWWWWWWWWWWWWWEWWWWWWWWWWWWW
WH           W      WC       HW
W            W      W         W
W            W      WWWWW     W
WWWWFWW                 WWWWEWW
W    WWWWWWWWWWWWWWWW         W
W   WK       WH               W
W   W        W                W
W   W        W                W
WBHKW        E               CW
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");


			PlayerPosition = FindPlayerPosition();
		}

		public int Width => map.GetLength(0);
		public int Height => map.GetLength(1);
		public GameObject this[int i, int j] => map[i, j];
		public GameObject this[Point point] => this[point.X, point.Y];

		public void SetCell(Point point, GameObject gameObject) => SetCell(point.X, point.Y, gameObject);

		public void SetCell(int x, int y, GameObject gameObject) => map[x, y] = gameObject;
		
		private Point FindPlayerPosition()
		{
			for (var x = 0; x < Width; x++)
			for (var y = 0; y < Height; y++)
				if (this[x, y] is Player)
					return new Point(x, y);
			throw new Exception("Invalid map");
		}
	}
}