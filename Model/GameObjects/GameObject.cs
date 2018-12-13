namespace UlearnGame.Model.GameObjects
{
	public abstract class GameObject
	{
		public virtual void Update(){}
		public virtual void Interact(GameObject other){}
	}
}