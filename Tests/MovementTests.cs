using NUnit.Framework;
using UlearnGame.DataStructures;
using UlearnGame.Model;

namespace UlearnGame.Tests
{
	[TestFixture]
	public class MovementTests
	{
		private const string emptyMap3x3 = @"
WWWWW
WP  W
W   W
W   W
WWWWW";
		
		[Test]
		public void PlayerMovementIsCorrect1()
		{
			var model = new Game(emptyMap3x3);
			model.Player.Move(Direction.Down);
			model.Player.Move(Direction.Down);
			model.Player.Move(Direction.Right);
			model.Player.Move(Direction.Right);
			Assert.AreEqual(model.PlayerPosition, new Point(3, 3));
		}
		
		[Test]
		public void PlayerMovementIsCorrect2()
		{
			var model1 = new Game(emptyMap3x3);
			model1.Player.Move(Direction.Down);
			model1.Player.Move(Direction.Down);
			model1.Player.Move(Direction.Right);
			model1.Player.Move(Direction.Right);
			
			var model2 = new Game(emptyMap3x3);
			model2.Player.Move(Direction.Down);
			model2.Player.Move(Direction.Right);
			model2.Player.Move(Direction.Down);
			model2.Player.Move(Direction.Right);
			Assert.AreEqual(model1.PlayerPosition, model2.PlayerPosition);
		}

		[Test]
		public void PlayerDoNotMoveOnIncorrectPoint()
		{
			var model = new Game(emptyMap3x3);
			var oldPlayerPos = model.PlayerPosition;
			model.Player.Move(Direction.Up);
			model.Player.Move(Direction.Left);
			Assert.AreEqual(oldPlayerPos, model.PlayerPosition);
		}
	}
}