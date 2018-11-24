using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlearnGame
{
	public abstract class GameObject
	{
		public static GameObject Empty => null;
		public Point Position;
	}
}
