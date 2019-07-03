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

		public bool CanPlaceBall(Location loc)
		{
			//A ball can be placed in a spot if it and the 4 spots below are not empty
			if (Get(loc) != State.Empty)
			{
				return false;
			}

			if (loc.Level == 0)
			{
				return true;
			}

			var levelBelow = loc.Level - 1;

			return Get(loc.ModLevel(-1)) != State.Empty && Get(loc.ModLevel(-1).ModX(1)) != State.Empty &&
			       Get(loc.ModLevel(-1).ModY(1)) != State.Empty && Get(loc.ModLevel(-1).ModX(1).ModY(1)) != State.Empty;
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

		public bool CanRemoveBall(Location loc)
		{
			//a ball can be removed from a space if there's a ball there and there aren't any balls in the 4 spots above it.

			if (Get(loc) == State.Empty) return false;

			if (IsValidSpace(loc.ModLevel(1)) && Get(loc.ModLevel(1)) != State.Empty) return false;
			if (IsValidSpace(loc.ModLevel(1).ModX(-1)) && Get(loc.ModLevel(1).ModX(-1)) != State.Empty) return false;
			if (IsValidSpace(loc.ModLevel(1).ModY(-1)) && Get(loc.ModLevel(1).ModY(-1)) != State.Empty) return false;
			if (IsValidSpace(loc.ModLevel(1).ModX(-1).ModY(-1)) && Get(loc.ModLevel(1).ModX(-1).ModY(-1)) != State.Empty) return false;

			return true;
		}

		public bool IsValidSpace(Location l)
		{
			if (l.Level > Levels.Count - 1) return false; //space is above top level
			if (l.Level < 0) return false; //space is below bottom level
			if (l.X < 0 || l.Y < 0) return false;
			//
			if (l.X + l.Level > Levels.First().GetLength(0) - 1 || l.Y + l.Level > Levels.First().GetLength(0) - 1) return false;

			return true;
		}

		public State Get(Location l)
		{
			return Levels[l.Level][l.X, l.Y];
		}

		public void Set(Location l, State value)
		{
			Levels[l.Level][l.X, l.Y] = value;
		}

		public State GetWinner()
		{
			return Get(new Location{Level = Levels.Count - 1, X = 0, Y = 0});
		}

		public List<Location> GetValidMoves()
		{
			var validMoves = new List<Location>();
			for (int level = 0; level < Levels.Count; level++)
			{
				for (int i = 0; i < Levels[level].GetLength(0); i++)
				{
					for (int j = 0; j < Levels[level].GetLength(1); j++)
					{
						var loc = new Location {Level = level, X = i, Y = j};
						if (CanPlaceBall(loc))
						{
							validMoves.Add(loc);
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
