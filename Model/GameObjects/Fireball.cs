using System;
using UlearnGame.DataStructures;

namespace UlearnGame.Model.GameObjects
{
	public class Fireball : Enemy
	{
		private readonly Direction dir;

		public Fireball(Point position, Direction dir, bool disposable=true) : base(position)
		{
			this.dir = dir;
			Damage = 250;
			if(disposable)
				CanNotMoveTrough.Remove(typeof(Wall));
		}

		private void TryMove()
		{
			switch (dir)
			{
				case Direction.Up:
					Destination = new Point(Position.X, Position.Y - 1);
					break;
				
				case Direction.Down:
					Destination = new Point(Position.X, Position.Y + 1);
					break;
				
				case Direction.Left:
					Destination = new Point(Position.X - 1, Position.Y);
					break;
				
				case Direction.Right:
					Destination = new Point(Position.X + 1, Position.Y);
					break;
			}
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

		public override bool Interact(GameObject other)
		{
			switch (other)
			{
					case Wall _:
						return false;
					case Player _:
						return false;
					default:
						return true;
			}
		}
	}
}