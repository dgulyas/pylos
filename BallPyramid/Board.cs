using System;
using System.Collections.Generic;
using System.Linq;

namespace BallPyramid
{
	//Simulation of the Pylos ball stacking game.
	//Each level is a grid of balls. Each level rests on top of the one below it.
	//This forms a pyramid.

	public class Board
	{
		public List<State[,]> Levels = new List<State[,]>(4);

		public Board(int numLevels)
		{
			//level 0 has size numLevel x numLevel
			//the top level has size 1 x 1
			for (int i = 0; i < numLevels; i++)
			{
				Levels.Add(new State[numLevels-i,numLevels-i]);
			}

			ClearBoard();
		}

		public bool CanPlaceBall(int level, int x, int y)
		{
			//A ball can be placed in a spot if it and the 4 spots below are not empty
			if (Get(level, x, y) != State.Empty)
			{
				return false;
			}

			if (level == 0)
			{
				return true;
			}

			var levelBelow = level - 1;

			return Get(levelBelow, x, y) != State.Empty && Get(levelBelow,x + 1, y) != State.Empty &&
			       Get(levelBelow,x, y + 1) != State.Empty && Get(levelBelow,x + 1, y + 1) != State.Empty;
		}

		public void ClearBoard()
		{
			foreach (var level in Levels)
			{
				for (int i = 0; i < level.GetLength(0); i++)
				{
					for (int j = 0; j < level.GetLength(1); j++)
					{
						level[i, j] = State.Empty;
					}
				}
			}
		}

		public bool CanRemoveBall(int level, int x, int y)
		{
			//a ball can be removed from a space if there's a ball there and there aren't any balls in the 4 spots above it.

			if (Get(level, x, y) == State.Empty) return false;

			if (IsValidSpace(level + 1, x, y) && Get(level+1, x, y) != State.Empty) return false;
			if (IsValidSpace(level + 1, x - 1, y) && Get(level+1, x - 1, y) != State.Empty) return false;
			if (IsValidSpace(level + 1, x, y - 1) && Get(level+1, x, y - 1) != State.Empty) return false;
			if (IsValidSpace(level + 1, x - 1, y - 1) && Get(level+1, x - 1, y - 1) != State.Empty) return false;

			return true;
		}

		public bool IsValidSpace(int level, int x, int y)
		{
			if (level > Levels.Count - 1) return false; //space is above top level
			if (level < 0) return false; //space is below bottom level
			if (x < 0 || y < 0) return false;
			//
			if (x + level > Levels.First().GetLength(0) - 1 || y + level > Levels.First().GetLength(0) - 1) return false;

			return true;
		}

		public State Get(int level, int x, int y)
		{
			return Levels[level][x, y];
		}

		public void Set(int level, int x, int y, State value)
		{
			Levels[level][x, y] = value;
		}

		public State GetWinner()
		{
			return Get(Levels.Count - 1, 0, 0);
		}

		public List<(int, int, int)> GetValidMoves()
		{
			var validMoves = new List<(int, int, int)>();
			for (int level = 0; level < Levels.Count; level++)
			{
				for (int i = 0; i < Levels[level].GetLength(0); i++)
				{
					for (int j = 0; j < Levels[level].GetLength(1); j++)
					{
						if (CanPlaceBall(level, i, j))
						{
							validMoves.Add((level, i, j));
						}
					}
				}
			}

			return validMoves;
		}
	}

	//the state of each place on the board
	public enum State { Empty, White, Black };
}
