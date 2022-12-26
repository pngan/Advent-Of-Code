<Query Kind="Program" />


void Main()
{
	Console.WriteLine($"**** p1= {a()}");
	Console.WriteLine($"**** p2= {b()}");
}

int b()
{
	var input = Data.exampleb();
	return input.Count();
}


int a()
{
	var input = Data.testa();

	// Set up initial stacks
	// Find blank line index
	int blankLine = 0;
	while (blankLine < input.Count())
	{
		if (input[blankLine].Length == 0) break;
		blankLine++;
	}

	// Number of Stacks
	var stackCount = (int)(input[blankLine - 1].Length + 1) / 4;

	// Initialize stacks
	List<Stack<char>> stacks1 = new();
	List<Stack<char>> stacks2 = new();
	for (int i = 0; i < stackCount; i++)
	{
		stacks1.Add(new Stack<char>());
		stacks2.Add(new Stack<char>());
	}

	for (int i = 0; i < blankLine - 1; i++)
	{
		int cratePosition = 0;
		var line = input[i];
		for (int stack = 0; stack < stackCount; stack++)
		{
			if (stack == 0)
				cratePosition = 1; // The first crate value is second char in the line

			var crate = line[cratePosition];
			if (crate != ' ')
			{
				stacks1[stack].Push(crate);
				stacks2[stack].Push(crate);
			}
			cratePosition += 4;    // All subsequent crates are spaced 4 chars apart
		}
	}

	// Reverse all the stacks
	for (int i = 0; i < stacks1.Count(); i++)
	{
		stacks1[i] = new Stack<char>(stacks1[i]);
		stacks2[i] = new Stack<char>(stacks2[i]);
	}

	// Apply crate movement instructions
	for (int i = blankLine + 1; i < input.Length; i++)
	{
		var v = GetIntsFromLine(input[i]);
		int count = v[0];
		int from = v[1] - 1;
		int to = v[2] - 1;
		
		for (int j = 0; j < count; j++)
		{
			if (stacks1[from].Count() == 0) continue;
			char crate = stacks1[from].Pop();
			stacks1[to].Push(crate);
		}
		
		var tempStack = new Stack<char>();
		for (int j = 0; j < count; j++)
		{
			if (stacks2[from].Count() == 0) continue;
			char crate = stacks2[from].Pop();
			tempStack.Push(crate);
		}

		for (int j = 0; j < count; j++)
		{
			if (tempStack.Count() == 0) continue;
			char crate = tempStack.Pop();
			stacks2[to].Push(crate);
		}
	}

	// Calculate return string, by reading top of stacks
	StringBuilder sb1 = new();
	for (int s = 0; s < stackCount; s++)
	{
		var currentStack1 = stacks1[s];
		if (currentStack1.Count() != 0)
		{
			sb1.Append(currentStack1.Pop());
		}
	}
	sb1.ToString().Dump();


	StringBuilder sb2 = new();
	for (int s = 0; s < stackCount; s++)
	{
		var currentStack = stacks2[s];
		if (currentStack.Count() != 0)
		{
			sb2.Append(currentStack.Pop());
		}
	}
	sb2.ToString().Dump();
	return input.Count();
}

int[] GetIntsFromLine(string inputString) // ! For negative numbers - Use (\-*\d+) instead
{
	Regex rg = new(@"(\d+)");
	var matched = rg.Matches(inputString);
	return  matched.Cast<Match>().Select(m => Convert.ToInt32(m.Value)).ToArray();
}

/**** HINTS ****
data.Select(v => Convert.ToInt32(v)) // Convert to int
data.Split("\r\n\r\n");              // Split black lines
data.Select((x, i) => new { x, i })  // Project with index
data.GroupBy(x => x);                // Group by each unique
data.GroupBy(x => x).Select(g=> new {Key = g.Key.ToString(), Tally = g.Count()}).OrderByDescending(stat=>stat.Tally).ToList(); // Histogram


first.Intersect(second)               // Set AND
first.Except(second)                  // EXCLUDE
first.Union(second)                   // UNION (does not create duplicates)
first.Concat(second)                  // Concatenation (creates duplications) 
first.Distinct(second)                // UNIQUE

*/





public static class Data
{
	public static string[] testb() => tb.Split("\r\n");
	public const string tb =
@"REPLACE_ME"
	;

	public static string[] exampleb() => eb.Split("\r\n");
	public const string eb =
@"REPLACE_ME"
	;

	public static string[] testa() => ta.Split("\r\n");
	public const string ta =
@"        [Q] [B]         [H]        
    [F] [W] [D] [Q]     [S]        
    [D] [C] [N] [S] [G] [F]        
    [R] [D] [L] [C] [N] [Q]     [R]
[V] [W] [L] [M] [P] [S] [M]     [M]
[J] [B] [F] [P] [B] [B] [P] [F] [F]
[B] [V] [G] [J] [N] [D] [B] [L] [V]
[D] [P] [R] [W] [H] [R] [Z] [W] [S]
 1   2   3   4   5   6   7   8   9 

move 1 from 4 to 1
move 2 from 4 to 8
move 5 from 9 to 6
move 1 from 1 to 3
move 5 from 8 to 3
move 1 from 1 to 5
move 4 from 3 to 6
move 14 from 6 to 2
move 5 from 4 to 5
move 7 from 7 to 2
move 24 from 2 to 3
move 13 from 3 to 2
move 1 from 7 to 9
move 1 from 9 to 5
move 7 from 2 to 6
move 3 from 1 to 7
move 3 from 6 to 3
move 2 from 7 to 1
move 1 from 7 to 5
move 2 from 2 to 6
move 2 from 1 to 4
move 9 from 5 to 1
move 1 from 6 to 3
move 4 from 5 to 4
move 1 from 2 to 7
move 4 from 6 to 2
move 7 from 2 to 3
move 2 from 2 to 6
move 2 from 2 to 3
move 2 from 5 to 4
move 1 from 7 to 3
move 4 from 6 to 7
move 19 from 3 to 6
move 3 from 7 to 4
move 1 from 7 to 8
move 1 from 8 to 1
move 2 from 1 to 3
move 10 from 3 to 2
move 3 from 3 to 8
move 1 from 3 to 9
move 1 from 9 to 6
move 11 from 6 to 8
move 2 from 3 to 8
move 6 from 4 to 3
move 3 from 4 to 1
move 7 from 2 to 8
move 1 from 3 to 6
move 6 from 8 to 5
move 1 from 4 to 6
move 9 from 6 to 9
move 6 from 3 to 8
move 1 from 3 to 5
move 10 from 1 to 3
move 11 from 8 to 7
move 1 from 3 to 5
move 1 from 1 to 8
move 5 from 9 to 2
move 1 from 6 to 3
move 5 from 3 to 6
move 1 from 3 to 5
move 4 from 6 to 4
move 1 from 5 to 9
move 6 from 2 to 4
move 2 from 2 to 9
move 5 from 5 to 1
move 2 from 1 to 7
move 10 from 8 to 3
move 1 from 8 to 6
move 3 from 6 to 3
move 6 from 4 to 2
move 8 from 3 to 8
move 3 from 4 to 8
move 4 from 2 to 1
move 3 from 5 to 3
move 4 from 7 to 6
move 2 from 9 to 3
move 1 from 2 to 9
move 1 from 2 to 3
move 2 from 4 to 8
move 1 from 7 to 9
move 5 from 7 to 8
move 2 from 7 to 3
move 14 from 3 to 2
move 3 from 9 to 5
move 1 from 3 to 1
move 1 from 7 to 4
move 3 from 9 to 8
move 7 from 8 to 9
move 7 from 2 to 5
move 2 from 3 to 7
move 2 from 7 to 6
move 16 from 8 to 9
move 4 from 6 to 5
move 1 from 2 to 5
move 21 from 9 to 5
move 3 from 9 to 3
move 6 from 1 to 4
move 1 from 1 to 9
move 1 from 1 to 4
move 2 from 6 to 3
move 3 from 4 to 6
move 3 from 4 to 8
move 1 from 9 to 4
move 2 from 4 to 6
move 4 from 3 to 6
move 1 from 3 to 4
move 1 from 4 to 9
move 1 from 9 to 8
move 1 from 8 to 6
move 6 from 2 to 1
move 2 from 8 to 4
move 6 from 1 to 8
move 23 from 5 to 9
move 1 from 4 to 7
move 1 from 7 to 1
move 22 from 9 to 7
move 4 from 8 to 7
move 1 from 5 to 2
move 1 from 1 to 9
move 2 from 8 to 4
move 6 from 6 to 3
move 2 from 9 to 5
move 18 from 7 to 4
move 18 from 4 to 5
move 1 from 2 to 7
move 1 from 8 to 4
move 6 from 7 to 2
move 5 from 4 to 5
move 1 from 3 to 1
move 1 from 7 to 2
move 4 from 3 to 4
move 1 from 3 to 4
move 1 from 1 to 7
move 1 from 5 to 8
move 3 from 4 to 3
move 3 from 3 to 8
move 2 from 8 to 3
move 2 from 4 to 8
move 2 from 7 to 5
move 1 from 7 to 9
move 2 from 3 to 1
move 1 from 9 to 7
move 4 from 2 to 3
move 1 from 8 to 9
move 2 from 1 to 8
move 2 from 2 to 4
move 1 from 9 to 1
move 4 from 6 to 8
move 1 from 2 to 7
move 1 from 4 to 7
move 4 from 8 to 2
move 1 from 4 to 3
move 1 from 1 to 9
move 4 from 8 to 1
move 2 from 2 to 1
move 3 from 3 to 9
move 2 from 7 to 1
move 32 from 5 to 1
move 1 from 8 to 7
move 6 from 5 to 1
move 2 from 7 to 6
move 1 from 9 to 5
move 1 from 3 to 2
move 1 from 5 to 9
move 2 from 6 to 1
move 1 from 3 to 7
move 1 from 9 to 8
move 36 from 1 to 4
move 1 from 8 to 9
move 5 from 4 to 9
move 6 from 9 to 3
move 2 from 2 to 9
move 3 from 1 to 9
move 1 from 3 to 2
move 30 from 4 to 8
move 1 from 7 to 5
move 1 from 3 to 5
move 3 from 3 to 4
move 2 from 8 to 5
move 3 from 9 to 8
move 3 from 9 to 3
move 19 from 8 to 6
move 2 from 3 to 5
move 3 from 4 to 3
move 1 from 4 to 7
move 8 from 1 to 8
move 1 from 3 to 2
move 1 from 7 to 6
move 4 from 5 to 3
move 1 from 1 to 7
move 2 from 5 to 4
move 1 from 9 to 4
move 12 from 6 to 2
move 1 from 7 to 8
move 6 from 2 to 9
move 3 from 6 to 7
move 2 from 7 to 5
move 6 from 2 to 3
move 8 from 3 to 5
move 5 from 6 to 8
move 5 from 3 to 6
move 1 from 9 to 4
move 1 from 9 to 8
move 5 from 5 to 9
move 3 from 4 to 6
move 1 from 4 to 9
move 1 from 7 to 5
move 1 from 3 to 5
move 8 from 9 to 2
move 3 from 9 to 6
move 27 from 8 to 2
move 10 from 6 to 9
move 1 from 6 to 4
move 1 from 4 to 9
move 2 from 5 to 6
move 5 from 5 to 3
move 2 from 6 to 9
move 5 from 3 to 2
move 12 from 9 to 3
move 5 from 3 to 1
move 3 from 1 to 5
move 1 from 9 to 8
move 1 from 5 to 2
move 1 from 2 to 1
move 1 from 1 to 6
move 1 from 5 to 3
move 34 from 2 to 4
move 8 from 3 to 9
move 1 from 6 to 1
move 1 from 8 to 5
move 4 from 2 to 8
move 3 from 8 to 7
move 1 from 7 to 2
move 7 from 9 to 8
move 1 from 9 to 6
move 2 from 5 to 1
move 1 from 6 to 9
move 1 from 9 to 5
move 2 from 2 to 5
move 5 from 8 to 6
move 2 from 8 to 5
move 1 from 1 to 3
move 12 from 4 to 6
move 2 from 7 to 1
move 4 from 1 to 6
move 3 from 2 to 3
move 1 from 8 to 5
move 1 from 2 to 6
move 1 from 1 to 9
move 1 from 9 to 5
move 16 from 4 to 1
move 4 from 3 to 1
move 8 from 1 to 8
move 1 from 4 to 1
move 6 from 5 to 8
move 1 from 5 to 7
move 12 from 6 to 9
move 7 from 1 to 5
move 2 from 1 to 7
move 1 from 7 to 1
move 9 from 9 to 6
move 15 from 6 to 2
move 2 from 9 to 7
move 4 from 4 to 5
move 2 from 2 to 9
move 3 from 7 to 5
move 2 from 1 to 3
move 1 from 7 to 1
move 10 from 2 to 3
move 6 from 8 to 6
move 3 from 9 to 2
move 14 from 5 to 6
move 1 from 8 to 4
move 5 from 8 to 2
move 2 from 2 to 3
move 24 from 6 to 1
move 3 from 1 to 2
move 9 from 2 to 9
move 1 from 4 to 3
move 1 from 4 to 2
move 1 from 8 to 4
move 23 from 1 to 4
move 3 from 2 to 4
move 2 from 1 to 2
move 1 from 8 to 4
move 3 from 3 to 5
move 3 from 3 to 4
move 3 from 5 to 8
move 3 from 2 to 7
move 2 from 3 to 8
move 15 from 4 to 3
move 2 from 4 to 1
move 19 from 3 to 9
move 1 from 7 to 2
move 1 from 2 to 5
move 1 from 5 to 4
move 1 from 7 to 6
move 1 from 7 to 4
move 3 from 8 to 3
move 1 from 8 to 4
move 5 from 3 to 8
move 1 from 3 to 6
move 22 from 9 to 2
move 17 from 2 to 6
move 3 from 9 to 3
move 9 from 4 to 9
move 6 from 4 to 9
move 5 from 2 to 6
move 1 from 4 to 2
move 1 from 4 to 9
move 1 from 1 to 6
move 19 from 9 to 2
move 4 from 8 to 7
move 1 from 1 to 5
move 1 from 5 to 3
move 1 from 8 to 1
move 1 from 8 to 2
move 4 from 3 to 7
move 12 from 6 to 1
move 3 from 7 to 3
move 7 from 2 to 7
move 9 from 2 to 6
move 4 from 2 to 6
move 13 from 1 to 4
move 8 from 6 to 4
move 16 from 4 to 8
move 12 from 7 to 6
move 3 from 8 to 3
move 1 from 1 to 2
move 4 from 3 to 8
move 5 from 8 to 9
move 27 from 6 to 8
move 2 from 3 to 7
move 2 from 2 to 8
move 2 from 7 to 5
move 1 from 5 to 9
move 1 from 5 to 1
move 1 from 6 to 9
move 2 from 6 to 2
move 2 from 2 to 6
move 2 from 9 to 2
move 3 from 4 to 3
move 1 from 1 to 9
move 5 from 9 to 8
move 1 from 9 to 5
move 2 from 2 to 6
move 2 from 4 to 6
move 1 from 3 to 7
move 1 from 5 to 6
move 1 from 6 to 7
move 6 from 6 to 8
move 2 from 7 to 5
move 2 from 3 to 2
move 34 from 8 to 1
move 1 from 5 to 6
move 1 from 5 to 3
move 1 from 6 to 1
move 32 from 1 to 8
move 23 from 8 to 4
move 1 from 2 to 1
move 24 from 8 to 4
move 1 from 3 to 6
move 47 from 4 to 6
move 2 from 6 to 1
move 3 from 1 to 5
move 1 from 2 to 1
move 3 from 5 to 7
move 21 from 6 to 2
move 3 from 7 to 8
move 2 from 1 to 6
move 8 from 6 to 4
move 4 from 8 to 9
move 3 from 2 to 8
move 4 from 4 to 2
move 2 from 2 to 5
move 4 from 9 to 8
move 2 from 1 to 5
move 11 from 6 to 1
move 14 from 2 to 6
move 2 from 4 to 3
move 1 from 2 to 9
move 3 from 2 to 9
move 20 from 6 to 5
move 2 from 4 to 2
move 4 from 9 to 1
move 8 from 8 to 9
move 1 from 6 to 9
move 14 from 5 to 2
move 10 from 2 to 7
move 7 from 9 to 6
move 1 from 6 to 8
move 6 from 2 to 6
move 1 from 2 to 5
move 1 from 3 to 5
move 9 from 6 to 3
move 1 from 5 to 2
move 9 from 7 to 3
move 12 from 3 to 2
move 9 from 5 to 9
move 1 from 8 to 6
move 3 from 3 to 5
move 1 from 7 to 6
move 14 from 2 to 6
move 3 from 9 to 7
move 6 from 1 to 2
move 5 from 1 to 8
move 10 from 6 to 9
move 4 from 5 to 6
move 3 from 2 to 4
move 9 from 9 to 7
move 1 from 8 to 7
move 3 from 9 to 6
move 3 from 3 to 7
move 1 from 5 to 1
move 15 from 7 to 1
move 2 from 8 to 5
move 2 from 5 to 4
move 1 from 7 to 4
move 1 from 3 to 1
move 15 from 6 to 7
move 2 from 4 to 9
move 3 from 4 to 7
move 18 from 1 to 6
move 1 from 8 to 9
move 6 from 9 to 7
move 3 from 6 to 8
move 1 from 1 to 2
move 2 from 9 to 5
move 2 from 2 to 9
move 16 from 6 to 3
move 15 from 3 to 7
move 2 from 8 to 4
move 1 from 3 to 7
move 3 from 4 to 9
move 2 from 1 to 9
move 26 from 7 to 4
move 1 from 2 to 1
move 7 from 9 to 8
move 1 from 2 to 5
move 2 from 5 to 2
move 8 from 7 to 5
move 1 from 7 to 3
move 1 from 3 to 9
move 2 from 2 to 7
move 1 from 6 to 4
move 4 from 8 to 9
move 1 from 1 to 3
move 1 from 5 to 6
move 2 from 5 to 7
move 17 from 4 to 9
move 6 from 4 to 9
move 1 from 3 to 4
move 6 from 7 to 9
move 3 from 5 to 6
move 2 from 7 to 9
move 4 from 8 to 9
move 4 from 6 to 4
move 8 from 4 to 6
move 1 from 8 to 4
move 3 from 5 to 2
move 2 from 4 to 3
move 1 from 7 to 9
move 2 from 3 to 5
move 4 from 6 to 9
move 1 from 6 to 1
move 36 from 9 to 4
move 2 from 5 to 3
move 3 from 2 to 1
move 3 from 1 to 4
move 14 from 4 to 1
move 1 from 8 to 5
move 4 from 1 to 3
move 5 from 9 to 5
move 2 from 5 to 8
move 1 from 8 to 9
move 4 from 9 to 6
move 3 from 5 to 8
move 1 from 5 to 6
move 2 from 1 to 6
move 2 from 9 to 7
move 6 from 6 to 4
move 1 from 1 to 3
move 29 from 4 to 6
move 7 from 3 to 4
move 1 from 8 to 9
move 3 from 1 to 6
move 4 from 1 to 4
move 1 from 8 to 4
move 4 from 4 to 3
move 15 from 6 to 8
move 9 from 4 to 9
move 1 from 7 to 9
move 8 from 8 to 3
move 3 from 6 to 7
move 1 from 1 to 2
move 4 from 7 to 6
move 7 from 8 to 5
move 1 from 8 to 4
move 2 from 5 to 7
move 1 from 2 to 4
move 5 from 6 to 1
move 4 from 3 to 2"
	;

	public static string[] examplea() => ea.Split("\r\n");
	public const string ea =
@"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2"
	;
}
