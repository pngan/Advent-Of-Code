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



[Fact]
void partb()
{
	string[] data = Data.test1.Split("\r\n");

	var grouped = data.Select((x, i) => new { x, i })
					.GroupBy(y => y.i / 3)
					.Select(g => g.ToList());



	int score = 0;
	foreach (var triple in grouped)
	{
		var first = triple[0].x.Distinct().ToArray();
		var second = triple[1].x.Distinct().ToArray();
		var third = triple[2].x.Distinct().ToArray();
		List<char> list = new List<char>();
		list.AddRange(first);
		list.AddRange(second);
		list.AddRange(third);
		var values = list.GroupBy(x => x);
		foreach (var value in values)
		{
			if (value.Count() == 3)
			{
				var common = value.First();
				if (common >= 'a')
					score += (common - 'a' + 1);
				else
					score += (common - 'A' + 27);
				break;
			}
		}
	}

	score.Dump("b");
}
[Fact]
void partb2()
{
	string[] data = Data.test1.Split("\r\n");

	var grouped = data.Select((x, i) => new { x, i })
					.GroupBy(y => y.i / 3)
					.Select(g => g.ToList());



	int score = 0;
	foreach (var triple in grouped)
	{
		var list = triple[0].x.Distinct();
		var second = triple[1].x.Distinct();
		var third = triple[2].x.Distinct();
		
		list = list.Concat(second);
		list = list.Concat(third);
		
		var values = list.GroupBy(x => x);
		foreach (var value in values)
		{
			if (value.Count() == 3)
			{
				var common = value.First();
				if (common >= 'a')
					score += (common - 'a' + 1);
				else
					score += (common - 'A' + 27);
				break;
			}
		}
	}

	score.Dump("b-2");
}

[Fact]
void partb3()
{
	string[] data = Data.test1.Split("\r\n");

	int score = 0;
	for (int i = 0; i < data.Length; i += 3)
	{
		var common = data[i].Intersect(data[i + 1].Intersect(data[i + 2])).First();

		if (common >= 'a')
			score += (common - 'a' + 1);
		else
			score += (common - 'A' + 27);
	}

	score.Dump("b-3");
}


[Fact]
void parta()
{
	string[] data = Data.example1.Split("\r\n");
	int score = 0;
	foreach (var line in data)
	{
		var half = line.Length / 2;
		var first = line.Take(half).Distinct();
		var last = line.TakeLast(half).Distinct();
		List<char> list = new List<char>();
		list.AddRange(first);
		list.AddRange(last);
		var combined = list.ToArray();
		var common = combined.Where(f => Array.IndexOf(combined, f) != Array.LastIndexOf(combined, f)).First();
		if (common >= 'a')
			score += (common - 'a' + 1);
		else
			score += (common - 'A' + 27);
	}
	score.Dump("a");
}

[Fact]
void parta2()
{
	string[] data = Data.example1.Split("\r\n");
	int score = 0;
	foreach (var line in data)
	{
		var half = line.Length / 2;
		var first = line[..half];
		var last = line[half..];
		foreach (var c in first)
		{
			if (last.Contains(c))
			{
				if (c >= 'a')
					score += (c - 'a' + 1);
				else
					score += (c - 'A' + 27);
				break;
			}
			
		}
	}
	score.Dump("a-2");
}


#endregion