namespace BallPyramid
{
	public class Location
	{
		public int X;
		public int Y;
		public int Level;

		//If a bunch of these are chained together a lot of inbetween clones are going to be created. This isn't the best/fastest.
		public Location ModX(int modification)
		{
			var clone = Clone();
			clone.X += modification;
			return clone;
		}

		public Location ModY(int modification)
		{
			var clone = Clone();
			clone.Y += modification;
			return clone;
		}

		public Location ModLevel(int modification)
		{
			var clone = Clone();
			clone.Level += modification;
			return clone;
		}

		public Location Clone()
		{
			return new Location{Level = Level, X = X, Y = Y};
		}

		public override string ToString()
		{
			return $"Level:{Level} X:{X} Y:{Y}";
		}
	}
}
