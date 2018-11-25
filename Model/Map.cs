using System.Drawing;


namespace UlearnGame.Model
{
	public class Map
	{
		private GameObject[,] map = MapFactory.CreateMap(@"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
WPW             W   E         W
W WWWW   WWWW K W   WWWWWWWWWCW
W W HW   W  WWWWWW     WWWWWWWW
W W  W   W  W    W     W   WH W
W W      W  W    W         WW W
W WWWWWW W WW    W     W      W
W                W     WWWW   W
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

		public int Width => map.GetLength(0);
		public int Height => map.GetLength(1);
		public GameObject this[int i, int j] => map[i, j];
		public GameObject this[Point point] => this[point.X, point.Y];

		public void SetCell(Point point, GameObject gameObject) => SetCell(point.X, point.Y, gameObject);

		public void SetCell(int x, int y, GameObject gameObject) => map[x, y] = gameObject;
	}
}