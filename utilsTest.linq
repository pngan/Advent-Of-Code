<Query Kind="Program">
  <NuGetReference>NUnitLite</NuGetReference>
  <Namespace>NUnit.Framework</Namespace>
  <Namespace>NUnitLite</Namespace>
  <IncludeUncapsulator>false</IncludeUncapsulator>
</Query>

#load "utils.linq"

void Main()
{
	  new AutoRun().Execute(new[]{"-noheader", @"-work=E:\Users\Phillip\SynologyDrive\Linqpad\queries\Advent-Of-Code"});
}

[Test]
public void Pos_ShouldConstruct(
	[Values(-1, 0, 1)] int row,
	[Values(-1, 0, 1)] int col)
{
	Pos a = new Pos(row, col);
	Assert.IsNotNull(a);
	Assert.AreEqual(row, a.row);
	Assert.AreEqual(col, a.col);
}


[Test]
public void Pos_ShouldOperate()
{
	Pos a = new Pos(-3, 4);
	Pos b = new Pos(1, -2);
	Assert.AreEqual(new Pos(-3, 4), +a);
	Assert.AreEqual(new Pos(3, -4), -a);
	Assert.AreEqual(new Pos(-2, 2), a+b);
	Assert.AreEqual(new Pos(-4, 6), a-b);
	Assert.AreEqual(new Pos(-2, 5), a+1);
	Assert.AreEqual(new Pos(-4, 3), a-1);
	Assert.AreEqual(new Pos(3, 4), Pos.Abs(a));
	Assert.AreEqual(new Pos(1, 2), Pos.Abs(b));
	Assert.AreEqual(7, Pos.Length(a));
	Assert.AreEqual(10, Pos.Dist(a, b));

	Assert.AreEqual(new Pos(-4, 4), a.North());
	Assert.AreEqual(new Pos(0, -2), b.North());
	Assert.AreEqual(new Pos(-2, 4), a.South());
	Assert.AreEqual(new Pos(2, -2), b.South());
	Assert.AreEqual(new Pos(-3, 5), a.East());
	Assert.AreEqual(new Pos(1, -1), b.East());
	Assert.AreEqual(new Pos(-3, 3), a.West());
	Assert.AreEqual(new Pos(1, -3), b.West());
}

[Test]
public void Pixel_ShouldConstruct()
{
	Pos a = new Pos(-3, 4);
	Pixel p = new Pixel(-1, a);
	Assert.AreEqual(-1, p.Value);
	Assert.AreEqual(a, p.Position);
	Assert.IsTrue(p == p);
}

[Test]
public void TestImage()
{
	int numRows = 5;
	int numCols = 7;
	var values = Enumerable.Range(0, numRows*numCols).ToArray();
	var image = new Image2d(numRows, numCols, values);
	Assert.AreEqual(numRows, image.numRows);
	Assert.AreEqual(numCols, image.numCols);
	var imvalues =  image.grid.Cast<int>().ToArray();
	Assert.AreEqual(values.ToArray(), imvalues);
	
	Assert.AreEqual(0, image.Value(new Pos(0,0)).Value);
	Assert.AreEqual(1, image.Value(new Pos(0,1)).Value);
	Assert.AreEqual(7, image.Value(new Pos(1, 0)).Value);
	Assert.AreEqual(34, image.Value(new Pos(4, 6)).Value);
 	Assert.Throws<ArgumentOutOfRangeException>(() => image.Value(new Pos(5, 6)));
 	Assert.Throws<ArgumentOutOfRangeException>(() => image.Value(new Pos(4, 7)));
 	Assert.Throws<ArgumentOutOfRangeException>(() => image.Value(new Pos(4, -1)));

	var pixel = image.Value(new Pos(0, 0));
	Assert.Throws<ArgumentOutOfRangeException>(() => image.North(pixel));
	Assert.Throws<ArgumentOutOfRangeException>(() => image.NorthWest(pixel));
	Assert.Throws<ArgumentOutOfRangeException>(() => image.NorthEast(pixel));
	Assert.Throws<ArgumentOutOfRangeException>(() => image.West(pixel));
	Assert.Throws<ArgumentOutOfRangeException>(() => image.SouthWest(pixel));
	Assert.AreEqual(0, pixel.Value);
	Assert.AreEqual(1, image.East(pixel).Value);
	Assert.AreEqual(7, image.South(pixel).Value);
	Assert.AreEqual(8, image.SouthEast(pixel).Value);

	var neighbours = image.AllNeighbours(pixel);
	Assert.AreEqual(3, neighbours.Count());
	Assert.Contains(image.East(pixel), neighbours);
	Assert.Contains(image.South(pixel), neighbours);
	Assert.Contains(image.SouthEast(pixel), neighbours);
	
	neighbours = image.CardinalNeighbours(pixel);
	Assert.AreEqual(2, neighbours.Count());
	Assert.Contains(image.East(pixel), neighbours);
	Assert.Contains(image.South(pixel), neighbours);
}
