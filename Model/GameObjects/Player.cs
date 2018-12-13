using System;
using UlearnGame.DataStructures;

namespace UlearnGame.Model.GameObjects
{
	public class Player : GameObject
	{
		private Map map;
		public int FieldOfView { get; private set; } = 2;
		public int Keys { get; private set; }
		public int Health { get; private set; } = 3;
		public event Action DamageTaken;
		public event Action ItemTaken;
		public event Action ChestTaken;
		public event Action Dead;

		public Player(Map map) => this.map = map;
		
		public void Move(Direction dir)
		{
			var oldPlayerPos = map.PlayerPosition;
			switch (dir)
			{
				case Direction.Up:
					map.PlayerPosition = Move(oldPlayerPos, new Point(map.PlayerPosition.X, map.PlayerPosition.Y - 1));
					break;
				
				case Direction.Down:
					map.PlayerPosition = Move(oldPlayerPos, new Point(map.PlayerPosition.X, map.PlayerPosition.Y + 1));
					break;
				
				case Direction.Left:
					map.PlayerPosition = Move(oldPlayerPos, new Point(map.PlayerPosition.X - 1, map.PlayerPosition.Y));
					break;
				
				case Direction.Right:
					map.PlayerPosition = Move(oldPlayerPos, new Point(map.PlayerPosition.X + 1, map.PlayerPosition.Y));
					break;
			}
		}

		private Point Move(Point oldPlayerPos, Point destination)
		{
			if (IsCorrectMove(destination))
			{
				Interact(map[destination]);
				map.SetCell(oldPlayerPos, null);
				map.SetCell(destination, this);
				return destination;
			}

			return oldPlayerPos;
		}
		
		private bool IsCorrectMove(Point point)
		{
			return !(map[point] is Wall) && !(map[point] is Chest) || map[point] is Chest && Keys > 0;
		}
		
		public override void Interact(GameObject other)
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
					RemoveKey();
					break;
				case Enemy enemy:
					GetDamage(enemy.Damage);
					enemy.Kill();
					break;
				case Hearth hearth:
					Heal(hearth.Value);
					break;
			}
		}

		private void IncreaseFov(int amount)
		{
			FieldOfView += amount;
			ItemTaken?.Invoke();
		}

		private void GetDamage(int amount)
		{
			Health -= amount;
			DamageTaken?.Invoke();
			if (Health <=0)
				Dead?.Invoke();
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

		private void RemoveKey()
		{
			Keys--;
			ChestTaken?.Invoke();
		}
	}
}