using UlearnGame.DataStructures;

namespace UlearnGame.Model.GameObjects
{
	public class Bottle : GameObject
	{
		public int Value = 1;

		public Bottle(Point position) : base(position) {}
	}
}