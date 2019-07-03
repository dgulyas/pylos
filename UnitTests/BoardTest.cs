using System;

using BallPyramid;
using NUnit.Framework;

namespace UnitTests
{

	public class BoardTest
	{

		[TestCase(0, 0, 0, true)]
		[TestCase(-1, 0, 0, false)]
		[TestCase(0, -1, 0, false)]
		[TestCase(0, 0, -1, false)]
		[TestCase(3, 0, 0, true)]
		[TestCase(3, 1, 0, false)]
		[TestCase(3, -1, 0, false)]
		[TestCase(2, 1, 1, true)]
		[TestCase(1, 2, 2, true)]
		[TestCase(1, 3, 2, false)]
		[TestCase(1, 2, 3, false)]
		public void TestIsValidSpace(int level, int x, int y, bool inPyramid)
		{
			var board = new Board(4);
			var loc = new Location{Level = level, X = x, Y = y};
			Assert.AreEqual(board.IsValidSpace(loc), inPyramid);
		}

		[Test]
		public void TestCanRemoveBallOnEdgeCovered()
		{
			var board = new Board(4);

			var loc = new Location {Level = 1, X = 2, Y = 1};
			var locationAboveLoc = new Location {Level = 2, X = 1, Y = 1};

			board.Set(loc, State.Black);
			board.Set(locationAboveLoc, State.Black);

			Assert.AreEqual(board.CanRemoveBall(loc), false);
		}

		[Test]
		public void TestCanRemoveBallOnEdgeUncovered()
		{
			var board = new Board(4);
			var loc = new Location { Level = 1, X = 2, Y = 1 };
			board.Set(loc, State.Black);

			Assert.AreEqual(board.CanRemoveBall(loc), true);
		}

		//wow, writing these is really boring


	}
}
