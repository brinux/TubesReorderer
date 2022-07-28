namespace brinux.tubesReorderer
{
	public class GameStatus
	{
		private int TubesLength;

		private List<Tube> Tubes = new List<Tube>();

		private static List<GameStatus> KnownStatuses = new List<GameStatus>();

		public GameStatus(int tubesLength, List<List<ColorsEnum>> initialStatus)
		{
			TubesLength = tubesLength;

			foreach (var tube in initialStatus)
			{
				Tubes.Add(new Tube(tubesLength, tube));
			}
		}

		public void Solve()
		{
			KnownStatuses.Clear();

			KnownStatuses.Add(this);

			var resolution = SolveStatus(1);

			if (resolution == null)
			{
				Console.WriteLine("It wasn't possible to solve the game.");
			}
			else
			{
				resolution.Reverse();

				int count = 1;

				foreach (var step in resolution)
				{
					Console.WriteLine();
					Console.WriteLine($"Step { count++ }");
					Console.WriteLine();
					step.Item2.PrintStatus();
					Console.WriteLine();
					step.Item1.Print();
				}
			}
		}

		private List<(Move, GameStatus)> SolveStatus(int step)
		{
			//Console.WriteLine($"Step { step }");

			var moves = CalculateMoves();

			if (moves.Count() == 0)
			{
				return null;
			}

			foreach (var move in moves)
			{
				var newStatus = new GameStatus(TubesLength, GetStatusSetup());

				/*Console.WriteLine();
				Console.WriteLine("Selected move:");
				move.Print();*/

				newStatus.ApplyMove(move);

				/*newStatus.PrintStatus();
				Console.WriteLine();*/

				if (newStatus.IsThisStatusKnown())
				{
					return null;
				}
				else
				{
					KnownStatuses.Add(newStatus);
				}

				if (newStatus.IsSolved())
				{
					return new List<(Move, GameStatus)> { (move, newStatus) };
				}
				else
				{
					var output = newStatus.SolveStatus(step + 1);

					if (output != null)
					{
						output.Add((move, this));

						return output;
					}
				}
			}

			return null;
		}

		private bool IsThisStatusKnown()
		{
			foreach (var knownStatus in KnownStatuses)
			{
				if (Equals(knownStatus))
				{
					return true;
				}
			}

			return false;
		}

		public bool Equals(GameStatus status)
		{
			var matchedTubes = new List<int>();

			foreach (var tube in Tubes)
			{
				var matchedTube = false;

				foreach (var statusTube in status.Tubes)
				{
					if (!matchedTubes.Contains(status.Tubes.IndexOf(statusTube)))
					{
						if (tube.Equals(statusTube))
						{
							matchedTubes.Add(status.Tubes.IndexOf(statusTube));

							matchedTube = true;

							break;
						}
					}
				}

				if (!matchedTube)
				{
					return false;
				}
			}

			return true;
		}

		private void ApplyMove(Move move)
		{
			for (var c = 0; c < move.Amount; c++)
			{
				Tubes[move.SourceTubeIndex].Pop();
				Tubes[move.DestinationTubeIndex].Push(move.Color);
			}
		}

		private bool IsSolved()
		{
			var solved = true;

			foreach (var tube in Tubes)
			{
				solved &= tube.IsSolved();
			}

			return solved;
		}

		public void PrintStatus()
		{
			foreach (var t in Tubes)
			{
				t.Print(Tubes.IndexOf(t));
			}
		}

		private List<Move> CalculateMoves()
		{
			var sourceMoves = new List<MoveOption>();
			var destinationMoves = new List<MoveOption>();

			var moves = new List<Move>();

			foreach (var t in Tubes)
			{
				if (t.Status != TubeStatusEnum.Solved && t.Status != TubeStatusEnum.Empty)
				{
					var sourceMove = t.CalculateSourceMove();

					if (sourceMove != null)
					{
						sourceMoves.Add(sourceMove);
					}
				}

				if (t.Status != TubeStatusEnum.Solved && t.Status != TubeStatusEnum.Full)
				{
					var destinationMove = t.CalculateDestinationMove();

					if (destinationMove != null)
					{
						destinationMoves.Add(destinationMove);
					}
				}
			}

			/*Console.WriteLine();
			Console.WriteLine("Source options:");
			foreach (var option in sourceMoves)
			{
				Console.WriteLine($"Tube: { Tubes.IndexOf(option.Tube) }, Color: { option.Color }, Amount: { option.Amount }");
			}*/

			/*Console.WriteLine();
			Console.WriteLine("Destination options:");
			foreach (var option in destinationMoves)
			{
				Console.WriteLine($"Tube: { Tubes.IndexOf(option.Tube) }, Color: { (option.WithColor ? option.Color : "*") }, Amount: { option.Amount }");
			}*/

			foreach (var sourceOption in sourceMoves)
			{
				foreach (var destinationOption in destinationMoves)
				{
					if (sourceOption.Tube != destinationOption.Tube && (
							!destinationOption.WithColor ||
							sourceOption.Color == destinationOption.Color) &&
							!(sourceOption.Tube.Status == TubeStatusEnum.SingleColor && destinationOption.Tube.Status == TubeStatusEnum.Empty))
					{
						moves.Add(new Move(
							Tubes.IndexOf(sourceOption.Tube),
							Tubes.IndexOf(destinationOption.Tube),
							Math.Min(sourceOption.Amount, destinationOption.Amount),
							sourceOption.Color));
					}
				}
			}

			/*Console.WriteLine();
			Console.WriteLine("Moves:");
			foreach (var move in moves)
			{
				move.Print();
			}*/

			return moves;
		}

		private List<List<ColorsEnum>> GetStatusSetup()
		{
			var status = new List<List<ColorsEnum>>();

			foreach (var tube in Tubes)
			{
				status.Add(tube.ExportStatus());
			}

			return status;
		}
	}
}
