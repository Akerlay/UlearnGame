using NUnit.Framework;
using UlearnGame.Model;

namespace UlearnGame.Tests
{
	[TestFixture]
	public class MiscTests
	{
		[Test]
		public void FovIsCorrect1x1()
		{
			var map = @"
WWW
WPW
WWW";
			var model = new Game(map);
			model.Update();
			Assert.AreEqual(model.VisiblePoints.Count, 5);
		}
		
		[Test]
		public void FovIsCorrect3x3()
		{
			var map = @"
WWWWW
W   W
W P W
W   W
WWWWW";
			var model = new Game(map);
			model.Update();
			Assert.AreEqual(model.VisiblePoints.Count, 21);
		}
		
		[Test]
		public void FovIsCorrect5x5()
		{
			var map = @"
WWWWWWWWWWWW
W          W
W          W
W    P     W
W          W
W          W
WWWWWWWWWWWW";
			var model = new Game(map);
			model.Update();
			Assert.AreEqual(model.VisiblePoints.Count, 25);
		}
	}
}