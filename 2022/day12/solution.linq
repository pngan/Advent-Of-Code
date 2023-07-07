<Query Kind="Program" />

#load "..\..\Utils.linq" 
string[] _input = File.ReadAllLines($"""{Path.GetDirectoryName(Util.CurrentQueryPath)}\in.txt""");
Image2d _grid;
Pixel _start;
Pixel _end;

void Main()
{
	Setup();
	Solve();
}

void Setup()
{
	var rows = _input.Length;
	var cols = _input[0].Length;
	
	int[,] grid = new int[rows,cols];

	for (int r = 0; r < rows; r++)
	{
		for (int c = 0; c < cols; c++)
		{
			var inputChar = _input[r][c];
			if (inputChar == 'S')  // Note starting position
			{
				inputChar = 'a';
				_start = new Pixel(0, r, c);
			}
			else if (inputChar == 'E') // Note end position
			{
				inputChar = 'z';
				_end = new Pixel('z'-'a', r, c);
			}

			grid[r, c] = inputChar - 'a';
		}
	}
	_grid = new Image2d(rows, cols, grid);
}

void Solve()
{
	var queue = new Queue<(Pixel pixel, int steps)>();
	var visited = new List<Pixel>();

	var prev = new Dictionary<(int, int), (int, int)>();

	// Initialize queue with the start position
	prev.Clear();
	queue.Enqueue((_start, 0));
	visited.Add(_start);

	while (queue.Count() != 0)
	{
		var current = queue.Dequeue();

		Console.Write($"Current = ({current.pixel} {current.steps})");

		if (current.pixel == _end) // Exit when end is found
		{
			Console.WriteLine("\n*** found!");
			break;
		}
		
		var cardinalNeighbours = _grid.CardinalNeighbours(current.pixel);
		var currentValue = current.pixel.Value;
		// Candidate next pixel must be in cardinal directions, not previously visited, and a value the same or one more
		// than the current pixel.
		var neighbours = cardinalNeighbours
			.Except(visited).Where(n => n.Value == currentValue || n.Value == currentValue+1)
			.OrderBy(n=>n.Value);
		if (!neighbours.Any()) Console.Write(" x");
		foreach (var neighbour in neighbours)
		{
			visited.Add(neighbour);
			queue.Enqueue((neighbour, current.steps+1));
		}
		Console.WriteLine();
	}


}