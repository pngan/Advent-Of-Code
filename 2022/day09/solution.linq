<Query Kind="Program">
  <Namespace>System.Numerics</Namespace>
</Query>

string[] _input = File.ReadAllLines($"""{Path.GetDirectoryName(Util.CurrentQueryPath)}\ex.txt""");
HashSet<Point> tailLocations1 = new();
HashSet<Point> tailLocations2 = new();
RopeSegment rope1 = new();
RopeSegment[] rope2 = new RopeSegment[] { new(), new(), new(), new(), new(), new(), new(), new(), new() };
RopeSegment[] rope3 = new RopeSegment[] { new(0,1,0,1), new(1,1,0,1), new(2,1,1,1)};


void Main()
{
	Setup();
//	Part1();
	Part2();
	//PrintRope(rope2);
}

// 6011
void Part2()
{
	foreach (var rawLine in _input)
	{
		rawLine.Dump();
		var line = rawLine.Split(" ");
		if (line is not [string strDirection, string strCount])
			throw new Exception($"Cannot parse line {rawLine}");
		var count = int.Parse(strCount);
		for (int i = 0; i < count; i++)
		{
			var translation = GetTranslation(strDirection);
			RopeSegment rope = null;
			for (int r = 0; r < rope2.Length; r++)
			{
				rope= rope2[r];
				//(i,r).Dump();
				translation = rope.Translate(translation);
				//rope.Dump();
			}
			tailLocations2.Add(rope.Tail);
			//tailLocations.Dump();
		}
		PrintRope(rope2);
	}
	//tailLocations2.Count().Dump();
}

void PrintRope(RopeSegment[] ropes)
{
	int rMax = 4;
	int rMin = 0;
	int cMax = 5;
	int cMin = 0;

	for (int r = rMax; r >= rMin; r--)
	{
		Console.Write($"{r:000;-00} ");
		for (int c = cMin; c <= cMax; c++)
		{
			bool found = false;
			for (int i = 0 ;i < ropes.Length-1; i++)
			{
				var rope = ropes[i];
				if (rope.Head.Row == r && rope.Head.Col == c)
				{
					Console.Write($"{i:00} ");
					found = true;
					break;
				}
			}
			if (!found)
				if (r == 0 && c == 0)
					Console.Write("s  ");
				else
				    Console.Write(".  ");
		}
		Console.WriteLine();
	}
	Console.WriteLine();
	for (int r = rMax; r >= rMin; r--)
	{
		Console.Write($"{r:000;-00} ");
		for (int c = cMin; c <= cMax; c++)
		{
			bool found = false;
			for (int i = 0; i < ropes.Length; i++)
			{
				var rope = ropes[i];
				if (rope.Tail.Row == r && rope.Tail.Col == c)
				{
					Console.Write($"{i:00} ");
					found = true;
					break;
				}
			}
			if (!found)
				if (r == 0 && c == 0)
					Console.Write("s  ");
				else
					Console.Write(".  ");
		}
		Console.WriteLine();
	}
}
// 6011
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
		for (int i = 0; i < count; i++)
		{
			//i.Dump();
			rope1.Translate(translation);
			//rope.Dump();
			tailLocations1.Add(rope1.Tail);
			//tailLocations.Dump();
		}
	}
	tailLocations1.Count().Dump();
}

void Setup()
{
	tailLocations1.Add(rope1.Tail);	// Record starting tail position
	tailLocations2.Add(
	rope2[8].Tail);
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
	
	public RopeSegment(int headRow = 0, int headCol = 0, int tailRow = 0, int tailCol = 0)
	{
		Head = new Point(headRow, headCol);
		Tail = new Point(tailRow, tailCol);
	}
	
	// Translate head and return tail translation
	public Translation Translate(Translation headTranslation)
	{
		var originalTail = Tail;
		Head += headTranslation;
		if (Length() > 1)
			Tail = Head - headTranslation; // Tail follows head in direction of head travel
		var tailTranslation = Tail - originalTail;
		tailTranslation.Dump();
		return tailTranslation;
	}

	private int Length()
	{
		var trans = Head - Tail;
		return Math.Max(Math.Abs(trans.deltaRow), Math.Abs(trans.deltaCol));
	}
}

static Translation GetTranslation(string direction) => direction switch
{
	"R" => new Translation( 0,  1),
	"L" => new Translation( 0, -1),
	"U" => new Translation( 1,  0),
	"D" => new Translation(-1,  0),
};
