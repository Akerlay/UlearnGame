using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UlearnGame.Model;
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
			var model = new GameModel();
			Application.Run(new GameForm(model));
		}
	}
}
