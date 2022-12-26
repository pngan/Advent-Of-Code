<Query Kind="Program">
  <Namespace>Xunit</Namespace>
</Query>

#load "xunit"
#load "data.linq"

void Main()
{
	//RunTests();  // Call RunTests() or press Alt+Shift+T to initiate testing.
}

#region private::Tests

[Fact] void example()
{
	string[] elfs = Data.example1.Split("\r\n\r\n");
	
	var largest = elfs
		.Select(e => e.Split("\r\n")
			.Select(v => Convert.ToInt32(v))
			.Sum())
		.Max();
	largest.Dump("example");
	
	Assert.Equal(24000, largest);
}


[Fact]
void test()
{
	string[] elfs = Data.test1.Split("\r\n\r\n");

	var largest = elfs
		.Select(e => e.Split("\r\n")
			.Select(v => Convert.ToInt32(v))
			.Sum())
		.Max();
	largest.Dump("test");
}



#endregion