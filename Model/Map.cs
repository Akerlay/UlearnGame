using System;
using System.Data;
using UlearnGame.DataStructures;
using UlearnGame.Model.GameObjects;


namespace UlearnGame.Model
{
	public class Map
	{
		public Player Player { get; }
		private GameObject[,] map;

		public Map(string mapString, GameState state)
		{
			map = MapFactory.CreateMap(state, mapString);
			Player = FindPlayer();
		}
		
		public Map(GameState state)
		{
			map = MapFactory.CreateMap(state, @"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
WPW             W   E         W
W WWWW   WWWW K W   WWWWWWWWWCW
W W HW   W  WWWWWW     WWWWWWWW
W W  W   W  W    W     W   WH W
W W      W  W    W         WW W
W WW WWW W WW    W     W      W
W     WB    *    W     WWWW   W
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

			Player = FindPlayer();
		}

		public int Width => map.GetLength(0);
		public int Height => map.GetLength(1);
		public GameObject this[int i, int j] => map[i, j];
		public GameObject this[Point point] => this[point.X, point.Y];
		
		private Player FindPlayer()
		{
			for (var x = 0; x < Width; x++)
			for (var y = 0; y < Height; y++)
				if (this[x, y] is Player)
					return (Player) this[x, y];
			throw new Exception("Invalid map");
		}

		public void Update()
		{
			for (var x = 0; x < Width; x++)
			for (var y = 0; y < Height; y++)
			{
				var obj = map[x, y];
				obj?.Update();
				
				if(obj is null || obj.Destination == obj.Position) 
					continue;
				
				var destObj = map[obj.Destination.X, obj.Destination.Y];
				
				if(obj.CanNotMoveTrough.Contains(destObj?.GetType()))
					continue;
				if (destObj is null)
					destObj = obj;
				else
				{
					var winner = RaiseConflict(obj, destObj);
					destObj = winner;
				}

				map[obj.Position.X, obj.Position.Y] = null;
				map[destObj.Destination.X, destObj.Destination.Y] = destObj;
				
				obj.AcceptMove();
			}
		}

		private GameObject RaiseConflict(GameObject obj, GameObject updatedObj)
		{
			var objResult = obj.Interact(updatedObj);
			var updatedResult = updatedObj.Interact(obj);
			if (updatedResult && objResult)
				return obj;
			
			if (objResult)
				return obj;
			
			if (updatedResult)
				return updatedObj;

			return obj;
		}

		public bool InBounds(Point p) => p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height;
	}
}