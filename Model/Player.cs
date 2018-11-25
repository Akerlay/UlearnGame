using System;

namespace UlearnGame.Model
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
		
		public void Interact(GameObject other)
		{
			if (other is Bottle bottle)
				IncreaceFov(bottle.Value);
			if (other is Key)
				AddKey();
			if (other is Chest)
				RemoveKey();
			if (other is Enemy enemy)
				GetDamage(enemy.Damage);
			if (other is Hearth hearth)
				Heal(hearth.Value);
		}

		private void IncreaceFov(int amount)
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