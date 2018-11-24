using System.Windows.Forms;
using System.Drawing;
using UlearnGame.Model;


namespace UlearnGame.View
{
	class GameForm : Form
	{
		private GameModel model;
		public GameForm(GameModel model)
		{
			this.model = model;
		}
	}
}
