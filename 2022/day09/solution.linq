<Query Kind="Program">
  <Namespace>System.Numerics</Namespace>
</Query>

string[] _input = File.ReadAllLines($"""{Path.GetDirectoryName(Util.CurrentQueryPath)}\ex.txt""");
HashSet<Point> tailLocations = new();
RopeSegment rope = new();

void Main()
{
	Setup();
	Part1();
}

void Part1()
{
	foreach (var rawLine in _input)
	{
		rawLine.Dump();
		var line = rawLine.Split(" ");
		if (line is not [string strDirection, string strCount])
			throw new Exception($"Cannot parse line {rawLine}");
		var count = int.Parse(strCount);
		var translation = GetTranslation(strDirection);
		for(int i = 0; i < count; i++)
		{
			i.Dump();
			rope.Translate(translation);
			rope.Dump();
			tailLocations.Add(rope.Tail);
			tailLocations.Dump();
		}
	}
	
}

void Setup()
{
	tailLocations.Add(rope.Tail);	// Record starting tail position
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

public class RopeSegment
{
	public Point Head { get; private set; } = new(0,0);
	public Point Tail { get; private set; } = new(0,0);
	
	// Translate head and return tail translation
	public Translation Translate(Translation headTranslation)
	{
		var originalTail = Tail;
		Head += headTranslation;
		if (Length() > 1)
			Tail = Head - headTranslation; // Tail follows head in direction of head travel
		var tailTranslation = Tail - originalTail;
		return tailTranslation;
	}

	private int Length()
	{
		var trans = Head - Tail;
		return Math.Max( trans.deltaRow, trans.deltaCol);
	}
}

static Translation GetTranslation(string direction) => direction switch
{
	"R" => new Translation( 0,  1),
	"L" => new Translation( 0, -1),
	"U" => new Translation( 1,  0),
	"D" => new Translation(-1,  0),
};
