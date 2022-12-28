<Query Kind="Program" />

// Trick to this problem is that the input set contains directories containing no files. So deriving the 
// list of directories from the list of files will lead to under counting.

string[] _input = File.ReadAllLines($"""{Path.GetDirectoryName(Util.CurrentQueryPath)}\in.txt""");

void Main()
{
	_input.Dump();
	Console.WriteLine($"**** p1= {a()}");
}

HashSet<string> _dirs = new();

int a()
{
	//var input = Data.testa();
	var sizedFiles = CreateFileList(_input); // Dictionary of files and their sizes
	Dictionary<string, int> sizedDirs = new();
	
	var files = sizedFiles.Keys;
	foreach(var dir in _dirs)
	{
		var containedFiles = sizedFiles.Where(f => f.Key.StartsWith(dir));
		sizedDirs[dir] = containedFiles.Sum(f => f.Value);
	}

	var targetDirs = sizedDirs.Where(d => d.Value <= 100000);

	var sumTargetDirs = targetDirs.Select(d => d.Value).Sum();

	return sumTargetDirs;
}

Dictionary<string,int> CreateFileList(string[] input)
{
	Dictionary<string, int> files = new();

	// Calculate the list of files and sizes
	var dirState = new DirectoryState();

	foreach (var lineString in input)
	{
		if (lineString == "$ cd /" || lineString == "$ ls" || lineString.StartsWith("dir ")) // Ignore irrelevant commands 
			continue;

		var line = lineString.Split(" ");
		if (line is ["$", "cd", string toDir])
		{
			dirState.ChangeDirectory(toDir);
			_dirs.Add(dirState.CurrentDirectory());
		}
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



