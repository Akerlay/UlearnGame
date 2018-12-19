using System;
using UlearnGame.DataStructures;

namespace UlearnGame.Model.GameObjects
{
	public class Player : GameObject
	{
		public int FieldOfView { get; private set; } = 2;
		public int Keys { get; private set; }
		public int Health { get; private set; } = 3;
		public event Action DamageTaken;
		public event Action ItemTaken;
		public event Action ChestTaken;
		public event Action Dead;

		public Player(Point position) : base(position)
		{
			CanNotMoveTrough.Add(typeof(Wall));
		}

		public void Move(Direction dir)
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
		
		public override bool Interact(GameObject other)
		{
			switch (other)
			{
				case Bottle bottle:
					IncreaseFov(bottle.Value);
					break;
				case Key _:
					AddKey();
					break;
				case Chest _:
					return RemoveKey();
					break;
				case Enemy enemy:
					return GetDamage(enemy.Damage);
				case Hearth hearth:
					Heal(hearth.Value);
					break;
			}

			return true;
		}

		private void IncreaseFov(int amount)
		{
			FieldOfView += amount;
			ItemTaken?.Invoke();
		}

		private bool GetDamage(int amount)
		{
			Health -= amount;
			DamageTaken?.Invoke();
			if (Health <= 0)
			{
				Dead?.Invoke();
				return false;
			}

			return true;
		}

		private void Heal(int amount)
		{
			Health += amount;
			ItemTaken?.Invoke();
		}

		private void AddKey()
		{
			Keys++;
			ItemTaken?.Invoke();
		}

		private bool RemoveKey()
		{
			if (Keys < 1)
				return false;
			Keys--;
			ChestTaken?.Invoke();
			return true;
		}
	}
}