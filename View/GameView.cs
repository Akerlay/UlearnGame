using System;
using System.Drawing;
using System.Windows.Forms;
using UlearnGame.Model;
using UlearnGame.Model.GameObjects;

namespace UlearnGame.View
{
	public partial class GameView : Form
	{
		private Timer timer;
		private Game game;
		private const int TileHeight = 32;
		private const int TileWidth = 32;
		
		public GameView()
		{
			timer = new Timer {Interval = 33};
			timer.Tick += UpdateState;
			timer.Start();
			game = new Game();
			InitializeComponent();
		}

		private async void UpdateState(object sender, EventArgs e)
		{
			game.Update();
			pictureBox1.Invalidate();
		}

		private void OnLoad(object sender, EventArgs e)
		{
			game.Player.Dead += EndGame;
			game.Player.DamageTaken += SoundSystem.Damage;
			game.Player.ItemTaken += SoundSystem.Item;
			game.Player.ChestTaken += SoundSystem.Chest;
			
			pictureBox1.Width = game.Map.Width * TileWidth;
			pictureBox1.Height = game.Map.Height * TileHeight;
			Width = game.Map.Width * (TileWidth + 1);
			Height = game.Map.Height * (TileHeight + 2);
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
			var visiblePoints = game.VisiblePoints;
			
			for (var x = 0; x < game.Map.Width; x++)
			{
				for (var y = 0; y < game.Map.Height; y++)
				{
					var currentPoint = new Point(x, y);
					if (!visiblePoints.Contains(currentPoint)) continue;
					
					g.DrawImage(Sprites.CobbleFloor, new Point(x * TileWidth, y * TileHeight));
					var obj = game.Map[currentPoint];
					if(obj is null) continue;
					
					switch (obj)
					{
						case Fireball _:
							g.DrawImage(Sprites.Fireball, new Point(x * TileWidth, y * TileHeight));
							break;
						
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
			game = new Game();
			OnLoad(new EventArgs());
		}

		private void DrawUI(Graphics g)
		{
			for (var i = 0; i < game.Player.Health; i++)
				g.DrawImage(Sprites.Hearth, new Point(i * 34, 0));
			for (var i = 0; i < game.Player.Keys; i++)
				g.DrawImage(Sprites.Key, new Point(i * 34, 34));
		}

		private void DrawFog(Graphics g)
		{
			var playerFov = game.Player.FieldOfView;
			var playerPos = game.PlayerPosition;
			for (var x = 0; x < game.Map.Width; x++)
			{
				for (var y = 0; y < game.Map.Height; y++)
				{
					if ((playerPos.X - x) * (playerPos.X - x) + (playerPos.Y - y) * (playerPos.Y - y)
					    > playerFov * playerFov  && !(game.Map[x, y] is Wall) && !(game.Map[x, y] is FakeWall))
					{
						g.DrawImage(Sprites.Fog, new Point(x * TileWidth, y * TileHeight));
					}
				}
			}
		}

		private void OnKeyPress(object sender, KeyPressEventArgs e)
		{
			switch (e.KeyChar.ToString().ToLower())
			{
				case "w":
					game.Player.Move(Direction.Up);
					break;
				
				case "s":
					game.Player.Move(Direction.Down);
					break;

				case "a":
					game.Player.Move(Direction.Left);
					break;

				case "d":
					game.Player.Move(Direction.Right);
					break;
			}
		}
	}
}