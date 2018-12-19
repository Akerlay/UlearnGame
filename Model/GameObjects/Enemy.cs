using System;
using System.Collections.Generic;
using UlearnGame.DataStructures;

namespace UlearnGame.Model.GameObjects
{
	public class Enemy : GameObject
	{
		public int Damage = 2;
		private readonly int fov = 4;
		protected int UpdCount;
		private GameState state;

		public Enemy(Point position) : base(position)
		{
			
		}

		public Enemy(Point position, GameState state) : base(position)
		{
			this.state = state;
			CanNotMoveTrough.Add(typeof(Wall));
			CanNotMoveTrough.Add(typeof(FakeWall));
		}

		private void TryMove()
		{
			var pathToPlayer = FindPath()?.ToList();
			if (pathToPlayer != null)
				Destination = pathToPlayer[pathToPlayer.Count - 2];
		}

		private SinglyLinkedList<Point> FindPath()
		{
			var visited = new HashSet<Point> {Position};
			var queue = new Queue<Tuple<SinglyLinkedList<Point>, int>>();
			queue.Enqueue(Tuple.Create(new SinglyLinkedList<Point>(Position), 0));

			while (queue.Count != 0)
			{
				var point = queue.Dequeue();
				if(point.Item2 > fov) break;
				AddAllCellsAround(point, visited, queue);
				
				if (point.Item1.Value == state.PlayerPosition)
					return point.Item1;
			}

			return null;
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
				var nextPoint = new Point (point.Item1.Value.X + dx, point.Item1.Value.Y + dy);
				if (visited.Contains(nextPoint)) continue;

				queue.Enqueue(Tuple.Create(new SinglyLinkedList<Point>(nextPoint, point.Item1), point.Item2 + 1));
				visited.Add(nextPoint);
			}
		}

		public override bool Interact(GameObject other)
		{
			return false;
		}
	}
}