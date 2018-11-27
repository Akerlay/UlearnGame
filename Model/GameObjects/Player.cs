using System;
using System.Drawing;

namespace UlearnGame.Model.GameObjects
{
	public class Player : GameObject
	{
		private readonly GameModel model;
		public int FieldOfView { get; private set; } = 3;
		public int Keys { get; private set; }
		public int Health { get; private set; } = 3;
		public event Action DamageTaken;
		public event Action ItemTaken;
		public event Action ChestTaken;
		public event Action Dead;

		public Player(GameModel model) => this.model = model;
		
		public void Move(Direction dir)
		{
			var oldPlayerPos = model.PlayerPosition;
			switch (dir)
			{
				case Direction.Up:
					model.PlayerPosition = Move(oldPlayerPos,
						new Point(model.PlayerPosition.X, model.PlayerPosition.Y - 1));
					break;
				
				case Direction.Down:
					model.PlayerPosition = Move(oldPlayerPos,
						new Point(model.PlayerPosition.X, model.PlayerPosition.Y + 1));
					break;
				
				case Direction.Left:
					model.PlayerPosition = Move(oldPlayerPos,
						new Point(model.PlayerPosition.X - 1, model.PlayerPosition.Y));
					break;
				
				case Direction.Right:
					model.PlayerPosition = Move(oldPlayerPos,
						new Point(model.PlayerPosition.X + 1, model.PlayerPosition.Y));
					break;
			}
			
			model.ChangeState();
		}

		private Point Move(Point oldPlayerPos, Point destination)
		{
			if (IsCorrectMove(destination))
			{
				Interact(model.Map[destination]);
				model.Map.SetCell(oldPlayerPos, null);
				model.Map.SetCell(destination, this);
				return destination;
			}

			return oldPlayerPos;
		}
		
		private bool IsCorrectMove(Point point)
		{
			return !(model.Map[point] is Wall) && !(model.Map[point] is Chest) || model.Map[point] is Chest && Keys > 0;
		}
		
		public void Interact(GameObject other)
		{
			if (other is Bottle bottle)
				IncreaseFov(bottle.Value);
			if (other is Key)
				AddKey();
			if (other is Chest)
				RemoveKey();
			if (other is Enemy enemy)
				GetDamage(enemy.Damage);
			if (other is Hearth hearth)
				Heal(hearth.Value);
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