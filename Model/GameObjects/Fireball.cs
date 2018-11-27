using System;
using System.Drawing;

namespace UlearnGame.Model.GameObjects
{
	public class Fireball : Enemy
	{
		private readonly Direction dir;
		private Point position;

		public Fireball(GameModel model, Point start, Direction dir) : base(model)
		{
			this.dir = dir;
			position = start;
			Damage = 250;
			Model.timer.Tick += TryMove;
		}

		private void TryMove(object sender, EventArgs e)
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
				if(Model.Map[destination] is Player p)
					p.Interact(this);
				Model.Map.SetCell(oldPos, null);
				Model.Map.SetCell(destination, this);
				return destination;
			}
			else
			{
				Model.Map.SetCell(position, null);
				Model.timer.Tick -= TryMove;
			}

			return oldPos;
		}
		
		private bool IsCorrectMove(Point point) => !(Model.Map[point] is Wall);
	}
}