using System;
using System.Collections.Generic;
using UlearnGame.DataStructures;
using UlearnGame.Model.GameObjects;

namespace UlearnGame.Model
{
	public class Game
	{
		public readonly Map Map;
		public Player Player => Map.Player;
		public Point PlayerPosition => Player.Position;
		public readonly HashSet<Point> VisiblePoints = new HashSet<Point>();
		public GameState State = new GameState();

		public Game()
		{
			Map = new Map(State);
		}

		public Game(string map)
		{
			Map = new Map(map, State);
		}
		
		private void UpdateVisiblePoints()
		{
			if (Map[PlayerPosition] == null) return;

			var visited = new HashSet<Point>();
			var queue = new Queue<Tuple<Point, int>>();
			queue.Enqueue(Tuple.Create(PlayerPosition, 0));
			while (queue.Count != 0)
			{
				var point = queue.Dequeue();
				if(visited.Contains(point.Item1)) continue;
				visited.Add(point.Item1);
				if (!(Map[point.Item1] is Wall || Map[point.Item1] is FakeWall) 
				    && point.Item2 <= Player.FieldOfView)
				{
					for (var dx = -1; dx <= 1; dx++)
					for (var dy = -1; dy <= 1; dy++)
					{
						if (dx != 0 && dy != 0) continue;
						var nextPoint = new Point(point.Item1.X + dx, point.Item1.Y + dy);
						if (!Map.InBounds(nextPoint)) continue;
						
						queue.Enqueue(Tuple.Create(nextPoint, point.Item2 + 1));
					}
				}
			}

			foreach (var e in visited)
				VisiblePoints.Add(e);
		}

		public void Update()
		{
			UpdateVisiblePoints();
			Map.Update();
			State.PlayerPosition = PlayerPosition;
		}
	}
}