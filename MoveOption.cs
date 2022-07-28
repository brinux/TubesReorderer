namespace brinux.tubesReorderer
{
	public class MoveOption
	{
		public Tube Tube { get; }
		public int Amount { get; }
		public bool WithColor { get; } = true;
		public ColorsEnum Color { get; }

		public MoveOption(Tube tube, int amount, ColorsEnum color)
		{
			Tube = tube;
			Amount = amount;
			Color = color;
		}

		public MoveOption(Tube tube, int amount)
		{
			Tube = tube;
			Amount = amount;
			WithColor = false;
		}
	}
}
