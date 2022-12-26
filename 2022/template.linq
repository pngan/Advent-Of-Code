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
	var input = Data.examplea();

	var files = CreateFileList(input);

	
	return input.Count();
}

Dictionary<string,int> CreateFileList(string[] input)
{
	Dictionary<string, int> files = new();

	// Calculate the list of files and sizes



	var dirState = new DirectoryState();

	foreach (var lineString in input)
	{
		if (lineString == "$ cd /" || lineString == "$ ls" || lineString.StartsWith("dir "))
			continue;

		var line = lineString.Split(" ");
		if (line is ["$", "cd", string toDir])
			dirState.ChangeDirectory(toDir);
		else if (line is [string sizeString, string filename])
		{
			int size = int.Parse(sizeString);
			string path = $"{dirState.CurrentDirectory()}${filename}";
			files[path] = size;
		}
	}
	
	return files;
}


internal class DirectoryState
{
	private List<string> _currentDir = new();
	
	internal void ChangeDirectory(string toDir)
	{
		if (toDir == "..")
			_currentDir.RemoveAt(_currentDir.Count - 1);
		else
			_currentDir.Add(toDir);
	}

	internal string CurrentDirectory() => '/'+string.Join('/', _currentDir);
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





int[] GetIntsFromLine(string inputString) // ! For negative numbers - Use (\-*\d+) instead
{
	Regex rg = new(@"(\d+)");
	var matched = rg.Matches(inputString);
	return matched.Cast<Match>().Select(m => Convert.ToInt32(m.Value)).ToArray();
}


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
@"REPLACE_ME"
	;

	public static string[] examplea() => ea.Split("\r\n");
	public const string ea =
@"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k"
	;
}







