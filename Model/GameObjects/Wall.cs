using UlearnGame.DataStructures;

namespace UlearnGame.Model.GameObjects
{
	public class Wall : GameObject
	{
		public Wall(Point position) : base(position) {}

		public override bool Interact(GameObject other)
		{
			switch (other)
			{
					case Fireball _:
						return true;
					default:
						return false;
			}
		}
	}
}