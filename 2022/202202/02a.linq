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
	var scores = new Dictionary<string, int>
	{
		{"A X", 4}, {"A Y", 8},  {"A Z", 3},
		{"B X", 1}, {"B Y", 5},  {"B Z", 9},
		{"C X", 7}, {"C Y", 2},  {"C Z", 6}
	};

	int score = 0;
	string[] games = Data.example1.Split("\r\n");
	score = games.Select(g => scores[g]).Sum();
	games.Dump();
	Assert.Equal(15, score);
}


[Fact]
void test()
{

}



#endregion