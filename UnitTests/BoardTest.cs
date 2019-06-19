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
			Assert.AreEqual(board.IsValidSpace(level,x,y), inPyramid);
		}

		[Test]
		public void TestCanRemoveBallOnEdgeCovered()
		{
			var board = new Board(4);
			board.Set(1, 2, 1, State.Black);
			board.Set(2, 1, 1, State.Black);

			Assert.AreEqual(board.CanRemoveBall(1,2,1), false);
		}



	}
}
