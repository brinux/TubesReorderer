namespace brinux.tubesReorderer
{
	public class Tube
	{
		public TubeStatusEnum Status { get; private set; } = TubeStatusEnum.Empty;

		private int Capacity;

		private Stack<ColorsEnum> Content;

		public Tube(int tubeLength, List<ColorsEnum> initialStatus)
		{
			Capacity = tubeLength;
			Content = new Stack<ColorsEnum>(tubeLength);

			if (initialStatus.Count() > tubeLength)
			{
				throw new ArgumentException($"Each tube can contain at most { tubeLength } elements.");
			}

			foreach (var color in initialStatus)
			{
				Content.Push(color);
			}

			UpdateStatus();
		}

		public void Push(ColorsEnum color)
		{
			Content.Push(color);

			UpdateStatus();
		}

		public void Pop()
		{
			Content.Pop();

			UpdateStatus();
		}

		public bool IsSolved()
		{
			return Status == TubeStatusEnum.Empty || Status == TubeStatusEnum.Solved;
		}

		public bool Equals(Tube tube)
		{
			if (Content.Count() != tube.Content.Count())
			{
				return false;
			}

			for (int i = 0; i < Content.Count(); i++)
			{
				if (Content.ElementAt(i) != tube.Content.ElementAt(i))
				{
					return false;
				}
			}

			return true;
		}

		public void Print(int tubeIndex)
		{
			Console.Write($"Tube { tubeIndex }: ");

			var elements = Content.ToList();
			elements.Reverse();

			foreach (var color in elements)
			{
				var initialColor = Console.ForegroundColor;

				switch (color)
				{
					case ColorsEnum.Arancione:
						Console.ForegroundColor = ConsoleColor.DarkYellow;
						break;
					case ColorsEnum.Azzurro:
						Console.ForegroundColor = ConsoleColor.Cyan;
						break;
					case ColorsEnum.Bianco:
						Console.ForegroundColor = ConsoleColor.White;
						break;
					case ColorsEnum.Blu:
						Console.ForegroundColor = ConsoleColor.Blue;
						break;
					case ColorsEnum.Fucsia:
						Console.ForegroundColor = ConsoleColor.Magenta;
						break;
					case ColorsEnum.Giallo:
						Console.ForegroundColor = ConsoleColor.Yellow;
						break;
					case ColorsEnum.Rosa:
						Console.ForegroundColor = ConsoleColor.Gray;
						break;
					case ColorsEnum.Rosso:
						Console.ForegroundColor = ConsoleColor.Red;
						break;
					case ColorsEnum.Marrone:
						Console.ForegroundColor = ConsoleColor.DarkRed;
						break;
					case ColorsEnum.VerdeChiaro:
						Console.ForegroundColor = ConsoleColor.Green;
						break;
					case ColorsEnum.VerdeScuro:
						Console.ForegroundColor = ConsoleColor.DarkGreen;
						break;
					case ColorsEnum.Viola:
						Console.ForegroundColor = ConsoleColor.DarkMagenta;
						break;
				}

				Console.Write($"███");

				Console.ForegroundColor = initialColor;
			}

			Console.WriteLine();
		}

		public MoveOption CalculateSourceMove()
		{
			if (Content.Count() == 0)
			{
				return null;
			}

			var color = Content.First();

			var amount = 0;

			foreach (var c in Content.ToList())
			{
				if (c == color)
				{
					amount++;
				}
				else
				{
					break;
				}
			}

			return new MoveOption(this, amount, color);
		}

		public MoveOption CalculateDestinationMove()
		{
			if (Content.Count() == Capacity)
			{
				return null;
			}

			return Content.Count() == 0 ?
				new MoveOption(this, Capacity) :
				new MoveOption(this, Capacity - Content.Count(), Content.First());
		}

		public List<ColorsEnum> ExportStatus()
		{
			var tubeStatus = Content.ToList();
			tubeStatus.Reverse();

			return tubeStatus;
		}

		private void UpdateStatus()
		{
			if (Content.Count == 0)
			{
				Status = TubeStatusEnum.Empty;
			}
			else if (Content.Count() < Capacity)
			{
				Status = TubeStatusEnum.Partial;

				var referenceColor = Content.First();

				if (Content.All(c => c == referenceColor))
				{
					Status = TubeStatusEnum.SingleColor;
				}
			}
			else
			{
				Status = TubeStatusEnum.Full;

				var referenceColor = Content.First();

				if (Content.All(c => c == referenceColor))
				{
					Status = TubeStatusEnum.Solved;
				}
			}
		}
	}
}