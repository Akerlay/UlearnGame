using System;

namespace UlearnGame.Model
{
	public class GameModel
	{
		public event Action StateChanged;
		public Map Map = new Map();

		public GameModel()
		{
			
		}

		public void MovePlayer(Direction dir)
		{
			switch (dir)
			{
					case Direction.Up:
						break;
					case Direction.Down:
						break;
					case Direction.Left:
						break;
					case Direction.Right:
						break;
			}
			Map.MovePlayer(dir);
			StateChanged?.Invoke();
		}
		
	}
}