using System;
using System.Drawing;

namespace UlearnGame.Model
{
	public class GameModel
	{
		public event Action StateChanged;
		public Map Map = new Map();
		public Point PlayerPosition;
		public Player Player;

		public GameModel()
		{
			PlayerPosition = FindPlayerPosition();
			Player = Map[PlayerPosition.X, PlayerPosition.Y] as Player;
		}

		private Point FindPlayerPosition()
		{
			for (var x = 0; x < Map.Width; x++)
			for (var y = 0; y < Map.Height; y++)
				if (Map[x, y] is Player)
					return new Point(x, y);
			throw new Exception("Invalid map");
		}

		public void MovePlayer(Direction dir)
		{
			var oldPlayerPos = PlayerPosition;
			switch (dir)
			{
				case Direction.Up:
					PlayerPosition = MovePlayer(oldPlayerPos, new Point(PlayerPosition.X, PlayerPosition.Y - 1));
					break;
				
				case Direction.Down:
					PlayerPosition = MovePlayer(oldPlayerPos, new Point(PlayerPosition.X, PlayerPosition.Y + 1));
					break;
				
				case Direction.Left:
					PlayerPosition = MovePlayer(oldPlayerPos, new Point(PlayerPosition.X - 1, PlayerPosition.Y));
					break;
				
				case Direction.Right:
					PlayerPosition = MovePlayer(oldPlayerPos, new Point(PlayerPosition.X + 1, PlayerPosition.Y));
					break;
			}
			
			StateChanged?.Invoke();
		}

		private Point MovePlayer(Point oldPlayerPos, Point destination)
		{
			if (IsCorrectMove(destination))
			{
				Player.Interact(Map[destination]);
				Map.SetCell(oldPlayerPos, null);
				Map.SetCell(destination, Player);
				return destination;
			}

			return oldPlayerPos;
		}

		private bool IsCorrectMove(Point point)
		{
			return !(Map[point] is Wall) && !(Map[point] is Chest) || (Map[point] is Chest && Player.Keys > 0);
		}
	}
}