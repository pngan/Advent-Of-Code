<Query Kind="Program" />

string[] _input = File.ReadAllLines($"""{Path.GetDirectoryName(Util.CurrentQueryPath)}\in.txt""");
int[,] _matrix;
int[,] _visible;
int _nRows;
int _nCols;

void Main()
{
	Setup();

	Console.WriteLine($"**** p1= {a()}");
	Console.WriteLine($"**** p2= {b()}");
}

void Setup()
{
	_nRows = _input.Length;
	_nCols = _input.First().Length;

	_matrix = new int[_nRows, _nCols];
	_visible = new int[_nRows, _nCols];
	for (int r = 0; r < _nRows; r++)
	{
		var row = _input[r];
		for (int c = 0; c < _nCols; c++)
		{
			_matrix[r, c] = row[c] - '0';
		}
	}
}

int b()
{
	int ans = 0;
	for (int r = 1; r < _nRows - 1; r++)
	{
		for (int c = 1; c < _nCols - 1; c++)
		{
			int currVal = _matrix[r, c];
			int viewDistance = 0;
			// up
			viewDistance = CalculateViewDistance(currVal, _matrix.GetColumnTo(c, r).Reverse());
			// down
			viewDistance *= CalculateViewDistance(currVal, _matrix.GetColumnFrom(c, r));
			// left
			viewDistance *= CalculateViewDistance(currVal, _matrix.GetRowTo(r, c).Reverse());
			// right
			viewDistance *= CalculateViewDistance(currVal, _matrix.GetRowFrom(r, c));
			
			if (viewDistance > ans) 
				ans = viewDistance;
		}
	}

	return ans;
}

int CalculateViewDistance(int currentTree, IEnumerable<int> otherTrees)
{
	var index = otherTrees.ToList().FindIndex(t => t >= currentTree);
	return (index == -1) ? otherTrees.Count() : index+1;
}

int a()
{
	int ans = 0;
	for (int r = 1; r < _nRows - 1; r++)
	{
		for (int c = 1; c < _nCols - 1; c++)
		{
			int currVal = _matrix[r, c];
			if (_matrix.GetColumnTo(c, r).Count(x => x >= currVal) == 0) { ans++; continue; }
			if (_matrix.GetColumnFrom(c, r).Count(x => x >= currVal) == 0) { ans++; continue; }
			if (_matrix.GetRowTo(r, c).Count(x => x >= currVal) == 0) { ans++; continue; }
			if (_matrix.GetRowFrom(r, c).Count(x => x >= currVal) == 0) { ans++; continue; }
		}
	}

	int edgeTrees = 2 * (_nRows + _nCols) - 4; // Number of trees on edge of matrix
	ans += edgeTrees;
	return ans;
}

public static class ArrayAccessor
{
	// Get Column up to but not including row number
	public static int[] GetColumnTo(this int[,] matrix, int columnNumber, int toRowNumber)
	{
		return Enumerable.Range(0, toRowNumber)
				.Select(x => matrix[x, columnNumber])
				.ToArray();
	}
	
	// Get Column from but including row number
	public static int[] GetColumnFrom(this int[,] matrix, int columnNumber, int fromRowNumber)
	{
		return Enumerable.Range(fromRowNumber+1, matrix.GetLength(0)-fromRowNumber-1)
				.Select(x => matrix[x, columnNumber])
				.ToArray();
	}
	
	// Get Row up to but not including column number
	public static int[] GetRowTo(this int[,] matrix, int rowNumber, int toColNumber)
	{
		return Enumerable.Range(0, toColNumber)
				.Select(x => matrix[rowNumber, x])
				.ToArray();
	}
	
	// Get Row from but not include column number
	public static int[] GetRowFrom(this int[,] matrix, int rowNumber, int fromColNumber)
	{
		return Enumerable.Range(fromColNumber+1, matrix.GetLength(1)-fromColNumber-1)
				.Select(x => matrix[rowNumber, x])
				.ToArray();
	}
}