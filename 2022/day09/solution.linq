<Query Kind="Program">
  <Namespace>System.Numerics</Namespace>
</Query>

string[] _input = File.ReadAllLines($"""{Path.GetDirectoryName(Util.CurrentQueryPath)}\in.txt""");
HashSet<Point> tailLocations = new();
Rope rope1 = new(2); // 2 knots
Rope rope2 = new(10);// 10 knots

void Main()
{
	Solve(rope1); // 6011
	tailLocations.Clear();
	Solve(rope2); // 2419
}

void Solve(Rope rope)
{
	tailLocations.Add(rope.Knots[rope.Knots.Length - 1]);

	foreach (var rawLine in _input)
	{
		var line = rawLine.Split(" ");
		if (line is not [string strDirection, string strCount])
			throw new Exception($"Cannot parse line {rawLine}");
		var count = int.Parse(strCount);
		var translation = GetTranslation(strDirection);
		for (int i = 0; i < count; i++)
		{
			rope.Translate(translation);
			tailLocations.Add(rope.Knots[rope.Knots.Length-1]);
		}
	}
	tailLocations.Count().Dump();
}

public record Translation(int deltaRow, int deltaCol);
public record Point(int Row, int Col)
{
	public static Point operator +(Point point, Translation translation) =>
  		point with { Row = point.Row + translation.deltaRow, Col = point.Col + translation.deltaCol };
	public static Point operator -(Point point, Translation translation) =>
  		point with { Row = point.Row - translation.deltaRow, Col = point.Col - translation.deltaCol };
	public static Translation operator -(Point left, Point right) => 
		new Translation(left.Row - right.Row, left.Col - right.Col);
}

public class Rope
{
	public Point[] Knots { get; private set; }
	
	public Rope(int knots)
	{
		Knots = new Point[knots];
		for(int i = 0; i < knots;i++)
			Knots[i] = new(0,0);
	}

	public void Translate(Translation translation)
	{
		var target = Knots[0] + translation + translation; // double translation to move head
		for (int k = 0; k < Knots.Length; k++)
		{
			var knot = Knots[k];
			int deltaRow = target.Row-knot.Row;
			int deltaCol = target.Col-knot.Col;
			if ((Math.Abs(deltaRow) > 1) || (Math.Abs(deltaCol) > 1)) // Only move if not adjacent
			   Knots[k] = new Point(knot.Row+Math.Sign(deltaRow), knot.Col+Math.Sign(deltaCol));
			target = Knots[k];
		}
	}
}

static Translation GetTranslation(string direction) => direction switch
{
	"R" => new Translation(0, 1),
	"L" => new Translation(0, -1),
	"U" => new Translation(1, 0),
	"D" => new Translation(-1, 0),
};
