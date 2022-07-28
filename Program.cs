namespace brinux.tubesReorderer
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var game = new GameStatus(4, new List<List<ColorsEnum>>()
			{
				// Game 238
				new List<ColorsEnum>() { ColorsEnum.VerdeScuro, ColorsEnum.Blu, ColorsEnum.Arancione, ColorsEnum.Blu },
				new List<ColorsEnum>() { ColorsEnum.Rosa, ColorsEnum.Rosa, ColorsEnum.Bianco, ColorsEnum.Bianco },
				new List<ColorsEnum>() { ColorsEnum.Giallo, ColorsEnum.Azzurro, ColorsEnum.Marrone, ColorsEnum.Blu },
				new List<ColorsEnum>() { ColorsEnum.VerdeScuro, ColorsEnum.Rosso, ColorsEnum.VerdeChiaro, ColorsEnum.Giallo },
				new List<ColorsEnum>() { ColorsEnum.Rosa, ColorsEnum.Marrone, ColorsEnum.Rosso, ColorsEnum.Rosso },
				new List<ColorsEnum>() { ColorsEnum.Azzurro, ColorsEnum.Viola, ColorsEnum.Giallo, ColorsEnum.Blu },
				new List<ColorsEnum>() { ColorsEnum.Marrone, ColorsEnum.VerdeChiaro, ColorsEnum.VerdeChiaro, ColorsEnum.Fucsia },
				new List<ColorsEnum>() { ColorsEnum.Fucsia, ColorsEnum.Arancione, ColorsEnum.Marrone, ColorsEnum.Viola },
				new List<ColorsEnum>() { ColorsEnum.VerdeScuro, ColorsEnum.Bianco, ColorsEnum.Arancione, ColorsEnum.Viola },
				new List<ColorsEnum>() { ColorsEnum.Fucsia, ColorsEnum.Azzurro, ColorsEnum.Fucsia, ColorsEnum.Rosa },
				new List<ColorsEnum>() { ColorsEnum.Bianco, ColorsEnum.Giallo, ColorsEnum.VerdeChiaro, ColorsEnum.Rosso },
				new List<ColorsEnum>() { ColorsEnum.Azzurro, ColorsEnum.VerdeScuro, ColorsEnum.Viola, ColorsEnum.Arancione },
				new List<ColorsEnum>() { },
				new List<ColorsEnum>() { }
			});

			game.PrintStatus();
			Console.WriteLine();

			game.Solve();
		}
	}
}
