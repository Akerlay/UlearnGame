using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UlearnGame.Model
{
	public static class MapFactory
	{
		private static readonly Dictionary<string, Func<GameObject>> factory = new Dictionary<string, Func<GameObject>>();

		public static GameObject[,] CreateMap(string map, string separator = "\r\n")
		{
			var rows = map.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
			if (rows.Select(z => z.Length).Distinct().Count() != 1)
				throw new Exception($"Wrong map '{map}'");
			var result = new GameObject[rows[0].Length, rows.Length];
			for (var x = 0; x < rows[0].Length; x++)
			for (var y = 0; y < rows.Length; y++)
				result[x, y] = CreateObjectBySymbol(rows[y][x]);
			return result;
		}

		private static GameObject CreateObjectByTypeName(string name)
		{
			if (!factory.ContainsKey(name))
			{
				var type = Assembly
					.GetExecutingAssembly()
					.GetTypes()
					.FirstOrDefault(z => z.Name == name);
				if (type == null)
					throw new Exception($"Can't find type '{name}'");
				factory[name] = () => (GameObject)Activator.CreateInstance(type);
			}

			return factory[name]();
		}


		private static GameObject CreateObjectBySymbol(char c)
		{
			switch (c)
			{
				case 'P':
					return CreateObjectByTypeName("Player");
				case 'E':
					return CreateObjectByTypeName("Cobold");
				case 'W':
					return CreateObjectByTypeName("Wall");
				case 'C':
					return CreateObjectByTypeName("Chest");
				case ' ':
					return null;
				default:
					throw new Exception($"wrong character for GameObject {c}");
			}
		}
	}
}