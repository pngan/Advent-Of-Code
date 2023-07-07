<Query Kind="Statements">
  <IncludeUncapsulator>false</IncludeUncapsulator>
</Query>

//-------------------------------------------------------------------
// Origin is top-left. Increasing row => down, Increasing col => right
public readonly struct Pos
{
	public readonly int row;
	public readonly int col;
	
	public Pos(int r, int c)
	{
		row = r;
		col = c;
	}
	
	public static Pos operator +(Pos a) => a;
	public static Pos operator -(Pos a) => new Pos(-a.row, -a.col);
	public static Pos operator +(Pos a, Pos b) => new Pos(a.row + b.row, a.col+b.col);
	public static Pos operator -(Pos a, Pos b) => new Pos(a.row - b.row, a.col - b.col);
	public static Pos operator +(Pos a, int x) => new Pos(a.row + x, a.col + x);
	public static Pos operator -(Pos a, int x) => new Pos(a.row - x, a.col - x);

	public static Pos Abs(Pos a) => new Pos(Math.Abs(a.row), Math.Abs(a.col));
	public static int Length(Pos a) => Math.Abs(a.row) + Math.Abs(a.col); // City Block length
	public static int Dist(Pos a, Pos b) => Length(a - b); // City Block Distance
	public override string ToString() => $"({row},{col})";
}

public static class PosOps
{
	public static Pos North(this Pos a) => new Pos(a.row - 1, a.col);
	public static Pos South(this Pos a) => new Pos(a.row + 1, a.col);
	public static Pos East(this Pos a) => new Pos(a.row, a.col + 1);
	public static Pos West(this Pos a) => new Pos(a.row, a.col - 1);
	public static Pos NorthWest(this Pos a) => new Pos(a.row - 1, a.col - 1);
	public static Pos NorthEast(this Pos a) => new Pos(a.row - 1, a.col + 1);
	public static Pos SouthWest(this Pos a) => new Pos(a.row + 1, a.col - 1);
	public static Pos SouthEast(this Pos a) => new Pos(a.row + 1, a.col + 1);
}

public readonly struct Pixel : IEquatable<Pixel>
{
	public readonly int Value;
	public readonly Pos Position;

	public Pixel(int v, int row, int col) : this(v, new Pos(row, col)) {}
	
	public Pixel(int v, Pos p)
	{
		Value = v;
		Position = p;
	}

	public static bool operator ==(Pixel obj1, Pixel obj2)
	{
		if (ReferenceEquals(obj1, obj2))
			return true;
		if (ReferenceEquals(obj1, null))
			return false;
		if (ReferenceEquals(obj2, null))
			return false;
		return obj1.Equals(obj2);
	}
	public static bool operator !=(Pixel obj1, Pixel obj2) => !(obj1 == obj2);
	public bool Equals(Pixel other)
	{
		if (ReferenceEquals(other, null))
			return false;
		if (ReferenceEquals(this, other))
			return true;
		return Value.Equals(other.Value)
			   && Position.Equals(other.Position);
	}
	public override string ToString() => $"[{Value} {Position}]";
}

// (0, 0) = (row, colo) => top left
// Increasing row => down
// Increasing col => right
public class Image2d
{
	public readonly int[,] grid;
	public readonly int numRows;
	public readonly int numCols;
	
	public Image2d(int nRows, int nCols, int[] values)
	{
		if (nRows < 0 || nCols < 0)
			throw new Exception("Image dimension(s) must not be negative.");

		if (values.Length != nRows * nCols)
			throw new Exception("Incorrect number of image values provided.");

		numRows = nRows;
		numCols  = nCols;
		
		grid = new int[nRows, nCols];
		int i = 0;
		for (int r = 0; r < nRows; r++)
			for (int c = 0; c < nCols; c++)
				grid[r, c] = values[i++];
	}
	
	public Image2d(int nRows, int nCols, int[,] values)
	{
		if (nRows < 0 || nCols < 0)
			throw new Exception("Image dimension(s) must not be negative.");

		if (values.Length != nRows * nCols)
			throw new Exception("Incorrect number of image values provided.");

		numRows = nRows;
		numCols = nCols;
		grid = values;
	}

	public Pixel Value(Pos a)
	{
		if (!InBounds(a))
			throw new ArgumentOutOfRangeException();
		return new Pixel(grid[a.row, a.col], a);
	}
	public bool InBounds(Pos a) => a.row >= 0 && a.row < numRows && a.col >= 0 && a.col < numCols;

	public Pixel North(Pixel a) => Value(a.Position.North());
	public Pixel NorthWest(Pixel a) => Value(a.Position.NorthWest());
	public Pixel NorthEast(Pixel a) => Value(a.Position.NorthEast());
	public Pixel South(Pixel a) => Value(a.Position.South());
	public Pixel SouthWest(Pixel a) => Value(a.Position.SouthWest());
	public Pixel SouthEast(Pixel a) => Value(a.Position.SouthEast());
	public Pixel West(Pixel a) => Value(a.Position.West());
	public Pixel East(Pixel a) => Value(a.Position.East());

	public List<Pixel> AllNeighbours(Pixel a)
	{
		List<Pixel> result = new();
		void AppendIfExists(Pos x) { if (InBounds(x)) result.Add(Value(x)); }
		
		AppendIfExists(a.Position.North());
		AppendIfExists(a.Position.NorthWest());
		AppendIfExists(a.Position.NorthEast());
		AppendIfExists(a.Position.South());
		AppendIfExists(a.Position.SouthWest());
		AppendIfExists(a.Position.SouthEast());
		AppendIfExists(a.Position.West());
		AppendIfExists(a.Position.East());

		return result;
	}
	
	public List<Pixel> CardinalNeighbours(Pixel a)
	{
		List<Pixel> result = new();
		void AppendIfExists(Pos x) { if (InBounds(x)) result.Add(Value(x)); }

		AppendIfExists(a.Position.East());
		AppendIfExists(a.Position.South());
		AppendIfExists(a.Position.West());
		AppendIfExists(a.Position.North());

		return result;
	}
}