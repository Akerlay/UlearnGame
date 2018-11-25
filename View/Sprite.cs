using System.Drawing;

namespace UlearnGame.View
{
	public class Sprite
	{
		private Bitmap bitmap;

		public Sprite(string filepath)
		{
			try
			{
				bitmap = new Bitmap(filepath);
			}
			catch
			{
				bitmap = new Bitmap("Images/missing.png");
			}
		}

		public Sprite() => bitmap = new Bitmap("Images/missing.png");

		public static implicit operator Bitmap(Sprite sprite) => sprite.bitmap;

	}
}