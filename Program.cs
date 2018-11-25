using System;

using System.Windows.Forms;
using UlearnGame.View;

namespace UlearnGame
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Application.Run(new GameView());
		}
	}
}
