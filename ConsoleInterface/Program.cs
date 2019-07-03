using System;
using System.Collections.Generic;
using BallPyramid;

namespace ConsoleInterface
{
	class Program
	{
		private static Board board;
		private static State activePlayer;
		private static Dictionary<State, char> charRep = new Dictionary<State, char> { {State.Black,'B'}, { State.White, 'W' }, { State.Empty, '-' } };
		//private static Dictionary<State, string> stringRep = new Dictionary<State, string> { { State.Black, "Black" }, { State.White, "White" }, { State.Empty, "Null" } };

		static void Main(string[] args)
		{
			Init();

			while (true)
			{
				PrintBoard();
				var validMoves = board.GetValidMoves();
				foreach (var validMove in validMoves)
				{
					Console.WriteLine(validMove);
				}

				var loc = GetUserInputLocation();

				board.Set(loc, activePlayer);
				SwitchActivePlayer();

				if (board.GetWinner() != State.Empty)
				{
					Console.WriteLine($"{board.GetWinner()} Wins");
					break;
				}
			}

		}

		private static void Init()
		{
			board = new Board(4);
			activePlayer = State.White;
		}

		private static void PrintBoard()
		{
			for (int levelIndex = 0; levelIndex < board.Levels.Count; levelIndex++)
			{
				Console.WriteLine($"Level {levelIndex}:");
				var level = board.Levels[levelIndex];

				Console.Write("  ");
				for (int x = 0; x < level.GetLength(0); x++)
				{
					Console.Write(x);
				}
				Console.WriteLine();

				for (int i = 0; i < level.GetLength(0); i++) //iterate over y dimension
				{
					Console.Write($"{i}:");
					for (int j = 0; j < level.GetLength(1); j++) //iterate over x dimension
					{
						Console.Write(charRep[level[j,i]]);
					}
					Console.WriteLine();
				}
				Console.WriteLine();
			}
		}

		private static void SwitchActivePlayer()
		{
			activePlayer = activePlayer == State.White ? State.Black : State.White;
		}

		private static Location GetUserInputLocation()
		{
			var receivedGoodInput = false;
			int level = 0;
			int x = 0;
			int y = 0;
			while (!receivedGoodInput)
			{
				level = -1;
				x = -1;
				y = -1;
				Console.Write($"{activePlayer.ToString().ToUpper()}'s turn. {Environment.NewLine}Enter move coordinates as level,x,y: ");
				var input = Console.ReadLine();
				if (input == null) continue;
				var tokens = input.Split(',');
				receivedGoodInput = int.TryParse(tokens[0], out level);
				receivedGoodInput = receivedGoodInput & int.TryParse(tokens[1], out x);
				receivedGoodInput = receivedGoodInput & int.TryParse(tokens[2], out y);
				if (!receivedGoodInput)
				{
					Console.WriteLine($"Could not parse input: '{input}'");
					continue;
				}

				var loc = new Location { Level = level, X = x, Y = y };
				if (!board.IsValidSpace(loc))
				{
					Console.WriteLine($"Move {input} is a coordinate outside the board.");
					receivedGoodInput = false;
					continue;
				}

				if (!board.CanPlaceBall(loc))
				{
					Console.WriteLine($"Cannot place a ball at {input}.");
					receivedGoodInput = false;
					continue;
				}
			}

			return new Location {Level = level, X = x, Y = y};
		}

	}
}
