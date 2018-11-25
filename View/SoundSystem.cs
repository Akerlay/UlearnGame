using System.Media;


namespace UlearnGame.View
{
	public static class SoundSystem
	{
		private static SoundPlayer key = new System.Media.SoundPlayer(@"Sounds\key.wav");
		private static SoundPlayer chest = new System.Media.SoundPlayer(@"Sounds\chest.wav");
		private static SoundPlayer damage = new System.Media.SoundPlayer(@"Sounds\classic_hurt.wav");

		public static void Damage() => damage.Play();
		public static void Item() => key.Play();
		public static void Chest() => chest.Play();
	}
}