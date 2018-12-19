using UlearnGame.DataStructures;

namespace UlearnGame.Model.GameObjects
{
	public class Hearth : GameObject
	{
		public int Value = 1;
		
		public Hearth(Point position) : base(position) {}
	}
}