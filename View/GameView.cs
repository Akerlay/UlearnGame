using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using UlearnGame.Model;

namespace UlearnGame.View
{
	public partial class GameView : Form
	{
		private GameModel model;
		private const int viewWidth = 30;
		private const int viewHeight = 30;

		private const int TileHeight = 32;
		private const int TileWidth = 32;
		
		public GameView()
		{
			model = new GameModel();
			InitializeComponent();
		}
		
		private void OnLoad(object sender, EventArgs e)
		{
			model.StateChanged += OnStateChanged;

			model.Player.Dead += EndGame;
			model.Player.DamageTaken += SoundSystem.Damage;
			model.Player.ItemTaken += SoundSystem.Item;
			model.Player.ChestTaken += SoundSystem.Chest;
			
			pictureBox1.Width = model.Map.Width * TileWidth;
			pictureBox1.Height = model.Map.Height * TileHeight;
			Width = model.Map.Width * (TileWidth + 1);
			Height = model.Map.Height * (TileHeight + 2);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
		}
		
		private void OnPaint(object sender, PaintEventArgs e)
		{
			DrawVisibleCells(e.Graphics);
			DrawFog(e.Graphics);
			DrawUI(e.Graphics);
		}

		private void DrawVisibleCells(Graphics g)
		{
			var visiblePoints = GetVisiblePoints();
			
			for (var x = 0; x < model.Map.Width; x++)
			{
				for (var y = 0; y < model.Map.Height; y++)
				{
					var currentPoint = new Point(x, y);
					if (!visiblePoints.Contains(currentPoint)) continue;
					
					g.DrawImage(Sprites.CobbleFloor, new Point(x * TileWidth, y * TileHeight));
					var obj = model.Map[currentPoint];
					if(obj is null) continue;
					
					switch (obj)
					{
						case Wall _:
							g.DrawImage(Sprites.CobbleWalls, new Point(x * TileWidth, y * TileHeight));
							break;

						case Player _:
							g.DrawImage(Sprites.Player, new Point(x * TileWidth, y * TileHeight));
							break;

						case Enemy _:
							g.DrawImage(Sprites.Kobold, new Point(x * TileWidth, y * TileHeight));
							break;

						case Key _:
							g.DrawImage(Sprites.Key, new Point(x * TileWidth, y * TileHeight));
							break;

						case Bottle _:
							g.DrawImage(Sprites.Bottle, new Point(x * TileWidth, y * TileHeight));
							break;

						case Chest _:
							g.DrawImage(Sprites.Chest, new Point(x * TileWidth, y * TileHeight));
							break;
						
						case Hearth _:
							g.DrawImage(Sprites.Hearth, new Point(x * TileWidth, y * TileHeight));
							break;
						
						case FakeWall _:
							g.DrawImage(Sprites.CobbleWalls, new Point(x * TileWidth, y * TileHeight));
							break;
					}
				}
			}
		}

		private void EndGame()
		{
			model = new GameModel();
			OnLoad(new EventArgs());
		}

		private void DrawUI(Graphics g)
		{
			for (var i = 0; i < model.Player.Health; i++)
				g.DrawImage(Sprites.Hearth, new Point(i * 34, 0));
			for (var i = 0; i < model.Player.Keys; i++)
				g.DrawImage(Sprites.Key, new Point(i * 34, 34));
		}

		private void DrawFog(Graphics g)
		{
			var playerFov = model.Player.FieldOfView;
			var playerPos = model.PlayerPosition;
			for (var x = 0; x < model.Map.Width; x++)
			{
				for (var y = 0; y < model.Map.Height; y++)
				{
					if ((playerPos.X - x) * (playerPos.X - x) + (playerPos.Y - y) * (playerPos.Y - y)
					    > playerFov * playerFov  || model.Map[x, y] is Wall || model.Map[x, y] is FakeWall)
					{
						g.DrawImage(Sprites.Fog, new Point(x * TileWidth, y * TileHeight));
					}
				}
			}
		}

		private HashSet<Point> GetVisiblePoints()
		{
			var visited = new HashSet<Point>();
			var queue = new Queue<Tuple<Point, int>>();
			queue.Enqueue(Tuple.Create(model.PlayerPosition, 0));
			while (queue.Count != 0)
			{
				var point = queue.Dequeue();
				if(visited.Contains(point.Item1)) continue;
				visited.Add(point.Item1);
				if (!(model.Map[point.Item1] is Wall || model.Map[point.Item1] is FakeWall) 
				    && point.Item2 <= model.Player.FieldOfView)
				{
					for (var dx = -1; dx <= 1; dx++)
					for (var dy = -1; dy <= 1; dy++)
					{
						if (dx != 0 && dy != 0) continue;
						queue.Enqueue(Tuple.Create(new Point(point.Item1.X + dx, point.Item1.Y + dy), point.Item2 + 1));
					}

				}
			}

			return visited;
		}

		private void OnKeyPress(object sender, KeyPressEventArgs e)
		{
			switch (e.KeyChar.ToString().ToLower())
			{
				case "w":
					model.MovePlayer(Direction.Up);
					pictureBox1.Focus();
					break;
				
				case "s":
					model.MovePlayer(Direction.Down);
					break;

				case "a":
					model.MovePlayer(Direction.Left);
					break;

				case "d":
					model.MovePlayer(Direction.Right);
					break;
			}
		}

		private void OnStateChanged() => pictureBox1.Invalidate();
	}
}