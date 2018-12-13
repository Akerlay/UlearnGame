using NUnit.Framework;
using UlearnGame.DataStructures;
using UlearnGame.Model;
using UlearnGame.Model.GameObjects;

namespace UlearnGame.Tests
{
	[TestFixture]
	public class MechanicsTests
	{
		[Test]
		public void FireballKillsPlayer()
		{
			var map = @"
WWWWWWW
W P  *W
WWWWWWW";
			var model = new Game(map);
			var isDead = false;
			model.Player.Dead += () => isDead = true;
			for (var i = 0; i < 30; i++)
			{
				if(isDead) break;
				model.Update();
			}
			Assert.IsTrue(isDead);
		}

		[Test]
		public void FireBallCanDestroyChest()
		{
			var map = @"
WWWWWWW
W   C*W
W    PW
WWWWWWW";
			var model = new Game(map);
			for (var i = 0; i < 20; i++)
				model.Update();
			
			Assert.IsTrue(model.Map[4, 1] is null && model.Map[3, 1] is Fireball);
		}
		
		[Test]
		public void FireBallCanDestroyKey()
		{
			var map = @"
WWWWWWW
W   K*W
W    PW
WWWWWWW";
			var model = new Game(map);
			for (var i = 0; i < 20; i++)
				model.Update();
			
			Assert.IsTrue(model.Map[4, 1] is null && model.Map[3, 1] is Fireball);
		}
		
		[Test]
		public void FireBallCanDestroyHearth()
		{
			var map = @"
WWWWWWW
W   H*W
W    PW
WWWWWWW";
			var model = new Game(map);
			for (var i = 0; i < 20; i++)
				model.Update();
			
			Assert.IsTrue(model.Map[4, 1] is null && model.Map[3, 1] is Fireball);
		}
		
		[Test]
		public void FireBallCanDestroyBottle()
		{
			var map = @"
WWWWWWW
W   B*W
W    PW
WWWWWWW";
			var model = new Game(map);
			for (var i = 0; i < 20; i++)
				model.Update();
			
			Assert.IsTrue(model.Map[4, 1] is null && model.Map[3, 1] is Fireball);
		}
		
		[Test]
		public void FireBallCanKillEnemy()
		{
			var map = @"
WWWWWWW
W   E*W
W    PW
WWWWWWW";
			var model = new Game(map);
			for (var i = 0; i < 20; i++)
				model.Update();
			
			Assert.IsTrue(model.Map[4, 1] is null && model.Map[3, 1] is Fireball);
		}
		
		[Test]
		public void FireballHasCorrectTickrate()
		{
			var map = @"
WWWWWWW
W P  *W
WWWWWWW";
			var model = new Game(map);
			var isDead = false;
			model.Player.Dead += () => isDead = true;
			for (var i = 0; i < 29; i++)
			{
				if(isDead) break;
				model.Update();
			}
			Assert.IsFalse(isDead);
		}

		[Test]
		public void EnemyDamagesPlayerInRange()
		{
			var map = @"
WWWWWWW
W P  EW
WWWWWWW";
			var model = new Game(map);
			var initialHealth = model.Player.Health;
			for (var i = 0; i < 45; i++)
				model.Update();
			
			Assert.AreEqual(model.Player.Health, initialHealth - 2);
		}
		
		[Test]
		public void EnemiesCanKillPlayer()
		{
			var map = @"
WWWWWWW
W P EEW
WWWWWWW";
			var model = new Game(map);
			var isDead = false;
			model.Player.Dead += () => isDead = true;
			for (var i = 0; i < 45; i++)
			{
				if(isDead) break;
				model.Update();
			}

			Assert.IsTrue(isDead);
		}
		
		[Test]
		public void EnemyDoesNotDamagesPlayerOutOfRange()
		{
			var map = @"
WWWWWWWWW
W P    EW
WWWWWWWWW";
			var model = new Game(map);
			var initialHealth = model.Player.Health;
			for (var i = 0; i < 100; i++)
				model.Update();
			
			Assert.AreEqual(model.Player.Health, initialHealth);
		}
		
		[Test]
		public void EnemyHasCorrectTickrate()
		{
			var map = @"
WWWWWWW
W P  EW
WWWWWWW";
			var model = new Game(map);
			var initialHealth = model.Player.Health;
			for (var i = 0; i < 44; i++)
				model.Update();
			
			Assert.AreNotEqual(model.Player.Health, initialHealth - 2);
		}

		[Test]
		public void BottleAddsPlayerFov()
		{
			var map = @"
WWWWWWW
W P B W
WWWWWWW";
			var model = new Game(map);
			var initialFov = model.Player.FieldOfView;
			model.Player.Move(Direction.Right);
			model.Player.Move(Direction.Right);
			Assert.AreEqual(model.Player.FieldOfView, initialFov + 1);
		}

		[Test]
		public void HearthAddsPlayerHealth()
		{
			var map = @"
WWWWWWW
W P H W
WWWWWWW";
			var model = new Game(map);
			var initialHealth = model.Player.Health;
			model.Player.Move(Direction.Right);
			model.Player.Move(Direction.Right);
			Assert.AreEqual(model.Player.Health, initialHealth + 1);
		}

		[Test]
		public void KeyIncreasePlayerKeys()
		{
			var map = @"
WWWWWWW
W P K W
WWWWWWW";
			var model = new Game(map);
			var initialKeys = model.Player.Keys;
			model.Player.Move(Direction.Right);
			model.Player.Move(Direction.Right);
			Assert.AreEqual(model.Player.Keys, initialKeys + 1);
		}

		[Test]
		public void ChestCanBePickedUpWithKey()
		{
			var map = @"
WWWWWWW
WP K CW
WWWWWWW";
			var model = new Game(map);
			var initialKeys = model.Player.Keys;
			model.Player.Move(Direction.Right);
			model.Player.Move(Direction.Right);
			model.Player.Move(Direction.Right);
			model.Player.Move(Direction.Right);
			Assert.IsTrue(model.Player.Keys == initialKeys && model.Map.PlayerPosition == new Point(5, 1));
		}
		
		[Test]
		public void ChestCanNotBePickedUpWithoutKey()
		{
			var map = @"
WWWWWWW
WP   CW
WWWWWWW";
			var model = new Game(map);
			model.Player.Move(Direction.Right);
			model.Player.Move(Direction.Right);
			model.Player.Move(Direction.Right);
			model.Player.Move(Direction.Right);
			Assert.AreEqual(model.Map.PlayerPosition, new Point(4, 1));
		}

		[Test]
		public void PlayerCanDestroyFakeWall()
		{
			var map = @"
WWWWWWW
WP F  W
WWWWWWW";
			var model = new Game(map);
			model.Player.Move(Direction.Right);
			model.Player.Move(Direction.Right);
			model.Player.Move(Direction.Right);
			model.Player.Move(Direction.Right);
			Assert.AreEqual(model.Map.PlayerPosition, new Point(5, 1));
		}
		
		[Test]
		public void PlayerCanNotDestroyWall()
		{
			var map = @"
WWWWWWW
WP W  W
WWWWWWW";
			var model = new Game(map);
			model.Player.Move(Direction.Right);
			model.Player.Move(Direction.Right);
			model.Player.Move(Direction.Right);
			model.Player.Move(Direction.Right);
			Assert.AreEqual(model.Map.PlayerPosition, new Point(2, 1));
		}
	}
}