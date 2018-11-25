using System;
using System.Drawing;
using System.Windows.Forms;
using UlearnGame.Model;

namespace UlearnGame.View
{
	public partial class GameView : Form
	{
		private readonly GameModel model;
		private const int viewWidth = 30;
		private const int viewHeight = 30;

		private const int tileHeight = 32;
		private const int tileWidth = 32;
		
		public GameView()
		{
			model = new GameModel();
			InitializeComponent();
		}
		
		private void OnLoad(object sender, EventArgs e)
		{
			model.StateChanged += OnStateChanged;
			
			pictureBox1.Width = model.Map.Width * tileWidth;
			pictureBox1.Height = model.Map.Height * tileHeight;
			Width = model.Map.Width * (tileWidth + 1);
			Height = model.Map.Height * (tileHeight + 4);
		}
		
		private void OnPaint(object sender, PaintEventArgs e)
		{
			for (var x = 0; x < model.Map.Width; x++)
			{
				for (var y = 0; y < model.Map.Height; y++)
				{
					var obj = model.Map[x, y];
					switch (obj)
					{
						case Wall _:
							e.Graphics.DrawImage(Sprites.Floor, new Point(x * tileWidth, y * tileHeight));
							break;
						
						case Player _:
							e.Graphics.DrawImage(Sprites.Player, new Point(x * tileWidth, y * tileHeight));
							break;
						
						case Cobold _:
							e.Graphics.DrawImage(Sprites.Cobald, new Point(x * tileWidth, y * tileHeight));
							break;
					}
				}
			}
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

		private void OnStateChanged()
		{
			pictureBox1.Invalidate();
		}
	}
}