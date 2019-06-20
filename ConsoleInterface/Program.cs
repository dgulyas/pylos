using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using BallPyramid;

namespace ConsoleInterface
{
	class Program
	{
		private static Board board;
		private static bool whitePlayerTurn;
		private static Dictionary<State, char> charRep = new Dictionary<State, char> { {State.Black,'b'}, { State.White, 'w' }, { State.Empty, '_' } };

		static void Main(string[] args)
		{
			Init();

			while (true)
			{
				PrintBoard();

				var activePlayer = "";
				activePlayer = whitePlayerTurn ? "white" : "black";
				whitePlayerTurn = !whitePlayerTurn;

				Console.WriteLine($"{activePlayer}'s turn. Enter move coordinates eg level,x,y:");
				var input = Console.ReadLine();




			}



		}

		public static void Init()
		{
			board = new Board(4);
			whitePlayerTurn = true;
		}

		public static void PrintBoard()
		{
			for (int levelIndex = 0; levelIndex < board.Levels.Count; levelIndex++)
			{
				var level = board.Levels[levelIndex];
				for (int i = 0; i < level.GetLength(0); i++)
				{
					for (int j = 0; j < level.GetLength(1); j++)
					{
						Console.Write(charRep[level[i,j]]);
					}
					Console.WriteLine();
				}
				Console.WriteLine();
			}
		}
	}
}
