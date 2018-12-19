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
			MovePlayer(model, Direction.Right);
			MovePlayer(model, Direction.Right);
			MovePlayer(model, Direction.Down);
			MovePlayer(model, Direction.Down);
			model.Update();
			Assert.AreEqual(model.Player.Position, new Point(3, 3));
		}
		
		[Test]
		public void PlayerMovementIsCorrect2()
		{
			var model1 = new Game(emptyMap3x3);
			MovePlayer(model1, Direction.Down);
			MovePlayer(model1, Direction.Down);
			MovePlayer(model1, Direction.Right);
			MovePlayer(model1, Direction.Right);
			
			var model2 = new Game(emptyMap3x3);
			MovePlayer(model2, Direction.Down);
			MovePlayer(model2, Direction.Right);
			MovePlayer(model2, Direction.Down);
			MovePlayer(model2, Direction.Right);
			Assert.AreEqual(model1.PlayerPosition, model2.PlayerPosition);
		}

		[Test]
		public void PlayerDoNotMoveOnIncorrectPoint()
		{
			var model = new Game(emptyMap3x3);
			var oldPlayerPos = model.PlayerPosition;
			MovePlayer(model, Direction.Up);
			MovePlayer(model, Direction.Left);
			Assert.AreEqual(oldPlayerPos, model.PlayerPosition);
		}
		
		private void MovePlayer(Game model, Direction dir)
		{
			model.Player.Move(dir);
			model.Update();
		}
	}
}