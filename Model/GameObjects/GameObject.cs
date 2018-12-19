using System;
using System.Collections.Generic;
using Point = UlearnGame.DataStructures.Point;

namespace UlearnGame.Model.GameObjects
{
	public abstract class GameObject
	{
		public virtual void Update(){}
		public Point Position;
		public Point Destination;

		public virtual bool Interact(GameObject other)
		{
			return false;
		}
		public readonly HashSet<Type> CanNotMoveTrough = new HashSet<Type>();

		protected GameObject(Point position)
		{
			Position = position;
			Destination = position;
		}

		public void AcceptMove() => Position = Destination;
	}
}