<Query Kind="Program" />

string[] _input = File.ReadAllLines($"""{Path.GetDirectoryName(Util.CurrentQueryPath)}\in.txt""");
List<Monkey> _monkeys = new();
long _reductionModulo = 1;

//Part 1 = 78678
//Part 2 = 15333249714
void Main()
{
	Setup();
	Process(ReductionPart1, 20);
	Console.WriteLine("Part 1 = " + CalculateResult());

	Setup();
	Process(ReductionPart2, 10000);
	Console.WriteLine("Part 2 = " + CalculateResult());
}

long CalculateResult()
{
	var counts = _monkeys.Select(m => m.InspectionCount).Order().ToArray();
	return (long)counts[^1]*(long)counts[^2];
}

public long ReductionPart1(long worry) => worry / 3;

public long ReductionPart2(long worry)
{
	return worry % _reductionModulo;
}

public void Process(Func<long, long> ReductionFunc, int numberRounds)
{
	for( int rounds = 0; rounds < numberRounds; rounds++)
	{
		foreach(var monkey in _monkeys)
		{
			while( monkey.Worry.Count > 0)
			{
				long worry = monkey.Worry.Dequeue();
				monkey.InspectionCount++;
				var temp = monkey.NewWorry(worry);
				var newWorry = ReductionFunc(temp);
				var monkeyToEnqueue = (newWorry % monkey.Factor) == 0 ? monkey.MonkeyTrue : monkey.MonkeyFalse;
				_monkeys[monkeyToEnqueue].Worry.Enqueue(newWorry);
			}
		}
	}
}


public void Setup()
{
	_monkeys = new();
	_reductionModulo = 1;
	var inputMonkeys = _input.Chunk(7);
	foreach (var inputMonkey in inputMonkeys)
	{
		var worries = GetIntsFromLine(inputMonkey[1]);
		var temp =  inputMonkey[2].Split(" ");
		string[] operation = inputMonkey[2].Split(" ").TakeLast(2).ToArray();
		int testFactor = int.Parse(inputMonkey[3].Split(" ").Last());
		int monkeyTrue = int.Parse(inputMonkey[4].Split(" ").Last());
		int monkeyFalse = int.Parse(inputMonkey[5].Split(" ").Last());

		_monkeys.Add(new Monkey(worries, operation, testFactor, monkeyTrue, monkeyFalse));
		
		_reductionModulo *= testFactor;
	}
}

public class Monkey
{
	public Queue<long> Worry { get; private set; }
	public Func<long, long> NewWorry {get; private set;}
	public int Factor { get; private set; }
	public int MonkeyTrue { get; private set; }
	public int MonkeyFalse { get; private set; }
	public int InspectionCount { get; set; } = 0;

	public Monkey(long[] worry, string[] operation, int testFactor, int monkeyTrue, int monkeyFalse)
	{
		Worry = new Queue<long>(worry);
		Factor = testFactor;
		MonkeyTrue = monkeyTrue;
		MonkeyFalse = monkeyFalse;
		NewWorry = operation switch
		{
			["+", "old"] => (worry => checked(worry + worry)),
			["+", string str] => (worry => checked(worry + int.Parse(str))),
			["*", "old"] => (worry => checked(worry * worry)),
			["*", string str] => (worry => checked(worry * int.Parse(str))),
			_ => _ => -1
		};
	}
}


long[] GetIntsFromLine(string inputString) // ! For negative numbers - Use (\-*\d+) instead
{
	Regex rg = new(@"(\d+)");
	var matched = rg.Matches(inputString);
	return matched.Cast<Match>().Select(m => Convert.ToInt64(m.Value)).ToArray();
}
