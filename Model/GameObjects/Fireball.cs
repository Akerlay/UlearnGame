using System;
using UlearnGame.DataStructures;

namespace UlearnGame.Model.GameObjects
{
	public class Fireball : Enemy
	{
		private readonly Direction dir;
		private Point position;

		public Fireball(Map map, Point start, Direction dir) : base(map)
		{
			this.dir = dir;
			position = start;
			Damage = 250;
		}

		private void TryMove()
		{
			var oldPos = position;
			switch (dir)
			{
				case Direction.Up:
					position = Move(oldPos, new Point(position.X, position.Y - 1));
					break;
				
				case Direction.Down:
					position = Move(oldPos, new Point(position.X, position.Y + 1));
					break;
				
				case Direction.Left:
					position = Move(oldPos, new Point(position.X - 1, position.Y));
					break;
				
				case Direction.Right:
					position = Move(oldPos, new Point(position.X + 1, position.Y));
					break;
			}
		}
		
		private Point Move(Point oldPos, Point destination)
		{
			if (IsCorrectMove(destination))
			{
				if(map[destination] is Player p)
					p.Interact(this);
				map.SetCell(oldPos, null);
				map.SetCell(destination, this);
				return destination;
			}

			map.SetCell(position, null);

			return oldPos;
		}

		public override void Update()
		{
			UpdCount += 1;
			if (UpdCount == 10)
			{
				UpdCount = 0;
				TryMove();
			}
		}

		private bool IsCorrectMove(Point point) => !(map[point] is Wall);
	}
}