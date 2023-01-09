<Query Kind="Program" />

string[] _input = File.ReadAllLines($"""{Path.GetDirectoryName(Util.CurrentQueryPath)}\ex.txt""");
int[,] _grid;
(int,int) _start;
(int,int) _end;

void Main()
{
	Setup();
	Solve();
}

void Setup()
{
	var rows = _input.Length;
	var cols = _input[0].Length;
	
	_grid = new int[rows,cols];
	
	for( int r = 0; r < rows; r++)
		for( int c = 0; c < cols; c++)
		{
			var inputChar = _input[r][c];
			if (inputChar == 'S')  // Note starting position
			{
				inputChar = 'a';
				_start = (r,c);
			}
			else if (inputChar == 'E') // Note end position
			{
				inputChar = 'z';
				_end = (r, c);
			}

			_grid[r,c] = inputChar - 'a';
		}
}

void Solve()
{
	var queue = new Queue<(int,int)>();
	var explored = new List<(int, int)>();

	var prev = new Dictionary<(int, int), (int, int)>();

	// Initialize queue with the start position
	prev.Clear();
	queue.Enqueue(_start);
	explored.Add(_start);

	int steps = 0;

	while (queue.Count() != 0)
	{
		//queue.Dump();
		steps++;
		var current = queue.Dequeue();
		
		Console.WriteLine($"Current = ({current.Item1}, {current.Item2})");

		if (current == _end) // Exit when end is found
		{
			Console.WriteLine("*** found!");
			break;
		}
		
		var allNeighbours = GetNeighbours(current);
		var neighbours = allNeighbours.Except(explored);
		foreach (var neighbour in neighbours)
		{
			explored.Add(neighbour);
			queue.Enqueue(neighbour);
			prev[neighbour] = current;
		}
	}
	steps.Dump();
	//prev.Dump();


}

List<(int, int)> GetNeighbours((int, int) currentPos)
{
	var neighbours = new List<(int,int)>();
	var currentVal = _grid[currentPos.Item1, currentPos.Item2];
	
	for(int dr = -1; dr < 2; dr++)
	 	for(int dc = -1; dc < 2; dc++)
		{
			if (dr ==0 && dc == 0) continue;
			int nr = currentPos.Item1 + dr;
			int nc = currentPos.Item2 + dc;
			if (nr < 0 || nr >= _grid.GetLength(0) || nc < 0 || nc >= _grid.GetLength(1))
				continue;
			int neighbourVal = _grid[nr,nc];
			if (neighbourVal == currentVal || neighbourVal == (currentVal+1))
				neighbours.Add((nr, nc));
		}
	
	return neighbours;
}

