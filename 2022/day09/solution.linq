<Query Kind="Program">
  <Namespace>System.Numerics</Namespace>
</Query>

string[] _input = File.ReadAllLines($"""{Path.GetDirectoryName(Util.CurrentQueryPath)}\in.txt""");

void Main()
{
	var pt = new Point(3, 4);
	var tr = new Translation(-1, 1);
	pt.Dump();
	tr.Dump();
	(pt+tr).Dump();
}


public record Translation2<T>(T XOffset, T YOffset) where T : IAdditionOperators<T, T, T>;

public record Point2<T>(T X, T Y) where T : IAdditionOperators<T, T, T>
{
	public static Point2<T> operator +(Point2<T> left, Translation2<T> right) =>
	  left with { X = left.X + right.XOffset, Y = left.Y + right.YOffset };
}

public record Translation(int deltaRow, int deltaCol);
public record Point(int Row, int Col)
{
	public static Point operator +(Point point, Translation translation) =>
  		point with { Row = point.Row + translation.deltaRow, Col = point.Col + translation.deltaCol };
}


//public record Translation<T>(T XOffset, T YOffset) where T : IAdditionOperators<T, T, T>;
//
//public record Point<T>(T X, T Y) where T : IAdditionOperators<T, T, T>
//{
//	public static Point<T> operator +(Point<T> left, Translation<T> right) =>
//	  left with { X = left.X + right.XOffset, Y = left.Y + right.YOffset };
//}