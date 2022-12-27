<Query Kind="Program" />

string[] _input = File.ReadAllLines($"""{Path.GetDirectoryName(Util.CurrentQueryPath)}\in.txt""");

void Main()
{
	a();
    b();
}

void a()
{
	int ans = 0;
	foreach (var inputLine in _input)
	{
		ans += score(inputLine);
	}
	ans.Dump();
}

int score(string input) => input switch
		{
		    "A X" => 4, "A Y" => 8, "A Z" => 3,
		    "B X" => 1, "B Y" => 5, "B Z" => 9,
		    "C X" => 7, "C Y" => 2, "C Z" => 6
		};
		
string transform(string input) => input switch
		{
			"A X" => "A Z", "A Y" => "A X", "A Z" => "A Y",
			"B X" => "B X", "B Y" => "B Y", "B Z" => "B Z",
		    "C X" => "C Y", "C Y" => "C Z", "C Z" => "C X"
		};

void b()
{
	int ans = 0;
	foreach (var inputLine in _input)
	{
		ans += score(transform(inputLine));
	}
	ans.Dump();
}
