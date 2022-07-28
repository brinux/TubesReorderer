namespace brinux.tubesReorderer
{
	public class Move
	{
		public int SourceTubeIndex { get; }
		public int DestinationTubeIndex { get; }
		public int Amount { get;  }
		public ColorsEnum Color { get; }

		public Move(int sourceTubeIndex, int destinationTubeIndex, int amount, ColorsEnum color)
		{
			SourceTubeIndex = sourceTubeIndex;
			DestinationTubeIndex = destinationTubeIndex;
			Amount = amount;
			Color = color;
		}

		public void Print()
		{
			Console.WriteLine($"Source: { SourceTubeIndex }, Destination: { DestinationTubeIndex }, Color: { Color }, Amount: { Amount }");
		}
	}
}
