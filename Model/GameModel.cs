using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlearnGame.Model
{
	class GameModel
	{
		public GameObject[,] Map;
		public event Action StateChanged;

		public GameModel()
		{
			Map = Maps.Test;
			var a = 0;
		}

		public void Start()
		{
			throw new NotImplementedException();
		}
	}
}
