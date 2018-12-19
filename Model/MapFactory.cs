using System;
using System.Linq;
using UlearnGame.DataStructures;
using UlearnGame.Model.GameObjects;

namespace UlearnGame.Model
{
	public static class MapFactory
	{
		public static GameObject[,] CreateMap(GameState state, string map, string separator = "\r\n")
		{
			var rows = map.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
			if (rows.Select(z => z.Length).Distinct().Count() != 1)
				throw new Exception($"Wrong map '{map}'");
			var result = new GameObject[rows[0].Length, rows.Length];
			for (var x = 0; x < rows[0].Length; x++)
			for (var y = 0; y < rows.Length; y++)
				result[x, y] = CreateObjectBySymbol(state, rows[y][x], new Point(x, y));
			return result;
		}

		private static GameObject CreateObjectBySymbol(GameState state, char c, Point position)
		{
			switch (c)
			{
				case 'P':
					return new Player(position);
				case 'E':
					return new Enemy(position, state);
				case 'W':
					return new Wall(position);
				case '#':
					return new Wall(position);
				case 'C':
					return new Chest(position);
				case 'K':
					return new Key(position);
				case 'B':
					return new Bottle(position);
				case 'H':
					return new Hearth(position);
				case 'F':
					return new FakeWall(position);
				case '*': 
					return new Fireball(position, Direction.Left);
				case ' ':
					return null;
				default:
					throw new Exception($"wrong character for GameObject {c}");
			}
		}
	}
}