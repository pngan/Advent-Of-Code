<Query Kind="Program" />

string[] _input = File.ReadAllLines($"""{Path.GetDirectoryName(Util.CurrentQueryPath)}\ex.txt""");
List<Monkey> _monkeys = new();

void Main()
{
	Setup();
	Process(ReductionPart1, 20);
}

public int ReductionPart1(int worry) => worry/3;

public void Process(Func<int,int> ReductionFunc, int numberRounds)
{
	DumpContainer dc = new DumpContainer();
	dc.Dump();
	for( int rounds = 0; rounds < numberRounds; rounds++)
	{
		foreach(var monkey in _monkeys)
		{
			foreach (var worry in monkey.Worry)
			{
				monkey.InspectionCount++;
				var newWorry = ReductionFunc(monkey.NewWorry(worry));
				var monkeyToEnqueue = (newWorry % monkey.Factor) == 0 ? monkey.MonkeyTrue : monkey.MonkeyFalse;
				_monkeys[monkeyToEnqueue].Worry.Enqueue(newWorry);
			}
			//dc.Content = rounds;

		}
	}
	_monkeys.Dump();
}


public void Setup()
{
	var inputMonkeys = _input.Chunk(7);
	foreach (var inputMonkey in inputMonkeys)
	{
		var worries = GetIntsFromLine(inputMonkey[1]);
		string[] operation = inputMonkey[2].Split(" ")[^2..^1];
		int testFactor = int.Parse(inputMonkey[3].Split(" ").Last());
		int monkeyTrue = int.Parse(inputMonkey[4].Split(" ").Last());
		int monkeyFalse = int.Parse(inputMonkey[5].Split(" ").Last());

		_monkeys.Add(new Monkey(worries, operation, testFactor, monkeyTrue, monkeyFalse));
	}

	//_monkeys.Dump();
}

public class Monkey
{
	public Queue<int> Worry { get; private set; }
	public Func<int, int> NewWorry {get; private set;}
	public int Factor { get; private set; }
	public int MonkeyTrue { get; private set; }
	public int MonkeyFalse { get; private set; }
	public int InspectionCount {get; set;} = 0;
	
	public Monkey(int[] worry, string[] operation , int testFactor, int monkeyTrue, int monkeyFalse)
	{
		Worry = new Queue<int>(worry);
		Factor = testFactor;
		MonkeyTrue = monkeyTrue;
		MonkeyFalse = monkeyFalse;
		NewWorry = operation switch
		{
			["+", "old"]      => (worry => worry + worry),
			["+", string str] => (worry => worry + int.Parse(str)),
			["*", "old"]      => (worry => worry * worry),
			["*", string str] => (worry => worry * int.Parse(str)),
			_                 => _ => -1
		};
	}
}


int[] GetIntsFromLine(string inputString) // ! For negative numbers - Use (\-*\d+) instead
{
	Regex rg = new(@"(\d+)");
	var matched = rg.Matches(inputString);
	return matched.Cast<Match>().Select(m => Convert.ToInt32(m.Value)).ToArray();
}
