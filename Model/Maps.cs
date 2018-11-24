using System.Reflection;

namespace UlearnGame.Model
{
	static class Maps
	{
		//TODO: Wrap gameObkect into "Map" and make ctors for Point 2 ints etc.
		public static GameObject[,] Test = MapFactory.CreateMap(@"
P    
     
    E");

		public static GameObject[,] GetMapByTitle(string mapTitle)
		{
			var mo = typeof(Maps).GetFields();
			return null;
		}


	}
}
