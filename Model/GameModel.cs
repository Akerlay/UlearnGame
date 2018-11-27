using System;
using System.Drawing;
using System.Windows.Forms;
using UlearnGame.Model.GameObjects;

namespace UlearnGame.Model
{
	public class GameModel
	{
		public event Action StateChanged;
		public Map Map;
		public Point PlayerPosition;
		public Player Player;
		public Timer timer;

		public GameModel(Timer timer)
		{
			this.timer = timer;
			timer.Tick += (sender, e) => StateChanged?.Invoke();
			Map = new Map(this);
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

		public void ChangeState() => StateChanged?.Invoke();
	}
}