namespace UlearnGame.Model.GameObjects
{
	public class Enemy : GameObject
	{
		protected readonly GameModel Model;
		public Enemy(GameModel model)
		{
			this.Model = model;
		}
		public int Damage = 2;
	}
}