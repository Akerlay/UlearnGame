using System;
using System.Drawing;
using System.Linq;
using UlearnGame.Model.GameObjects;

namespace UlearnGame.Model
{
	public static class MapFactory
	{
		public static GameObject[,] CreateMap(GameModel model, string map, string separator = "\r\n")
		{
			var rows = map.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
			if (rows.Select(z => z.Length).Distinct().Count() != 1)
				throw new Exception($"Wrong map '{map}'");
			var result = new GameObject[rows[0].Length, rows.Length];
			for (var x = 0; x < rows[0].Length; x++)
			for (var y = 0; y < rows.Length; y++)
				result[x, y] = CreateObjectBySymbol(rows[y][x], model, new Point(x, y));
			return result;
		}

		private static GameObject CreateObjectBySymbol(char c, GameModel model, Point position)
		{
			switch (c)
			{
				case 'P':
					return new Player(model);
				case 'E':
					return new Enemy(model);
				case 'W':
					return new Wall();
				case '#':
					return new Wall();
				case 'C':
					return new Chest();
				case 'K':
					return new Key();
				case 'B':
					return new Bottle();
				case 'H':
					return new Hearth();
				case 'F':
					return new FakeWall();
				case '*': 
					return new Fireball(model, position, Direction.Up);
				case ' ':
					return null;
				default:
					throw new Exception($"wrong character for GameObject {c}");
			}
		}
	}
}