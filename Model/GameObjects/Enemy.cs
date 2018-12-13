using System;
using System.Collections.Generic;
using UlearnGame.DataStructures;

namespace UlearnGame.Model.GameObjects
{
	public class Enemy : GameObject
	{
		public int Damage = 2;
		private readonly int fov = 4;
		protected Map map;
		private Point currentPos;
		protected int UpdCount;

		protected Enemy(Map map)
		{
			this.map = map;
		}

		public Enemy(Map map, Point currentPos)
		{
			this.map = map;
			this.currentPos = currentPos;
		}

		private void TryMove()
		{
			var pathToPlayer = FindPath()?.ToList();
			if (pathToPlayer != null)
				currentPos = Move(pathToPlayer[pathToPlayer.Count - 2]);
		}
		
		private Point Move(Point destination)
		{
			var oldPos = currentPos;
			if (IsCorrectMove(destination))
			{
				if (map[destination] is Player p)
				{
					p.Interact(this);
					return new Point(0,0);
				}
				map.SetCell(oldPos, null);
				map.SetCell(destination, this);
				return destination;
			}
			else
			{
				map.SetCell(currentPos, null);
			}

			return oldPos;
		}
		
		private bool IsCorrectMove(Point point) => !(map[point] is Wall) && !(map[point] is FakeWall);

		private SinglyLinkedList<Point> FindPath()
		{
			var visited = new HashSet<Point> {currentPos};
			var queue = new Queue<Tuple<SinglyLinkedList<Point>, int>>();
			queue.Enqueue(Tuple.Create(new SinglyLinkedList<Point>(currentPos), 0));

			while (queue.Count != 0)
			{
				var point = queue.Dequeue();
				if (map[point.Item1.Value] is Wall || map[point.Item1.Value] is FakeWall)
					continue;
				if(point.Item2 > fov) break;
				AddAllCellsAround(point, visited, queue);
				
				if (point.Item1.Value == map.PlayerPosition)
					return point.Item1;
			}

			return null;
		}

		public void Kill()
		{
			map.SetCell(currentPos, null);
		}

		public override void Update()
		{
			UpdCount += 1;
			if (UpdCount == 15)
			{
				UpdCount = 0;
				TryMove();
			}
		}

		private static void AddAllCellsAround(Tuple<SinglyLinkedList<Point>, int> point, ISet<Point> visited,
			Queue<Tuple<SinglyLinkedList<Point>, int>> queue)
		{
			for (var dx = -1; dx <= 1; dx++)
			for (var dy = -1; dy <= 1; dy++)
			{
				if (dx != 0 && dy != 0) continue;
				var nextPoint = new Point {X = point.Item1.Value.X + dx, Y = point.Item1.Value.Y + dy};
				if (visited.Contains(nextPoint)) continue;

				queue.Enqueue(Tuple.Create(new SinglyLinkedList<Point>(nextPoint, point.Item1), point.Item2 + 1));
				visited.Add(nextPoint);
			}
		}
	}
}