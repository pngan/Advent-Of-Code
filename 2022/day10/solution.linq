<Query Kind="Program" />

string[] _input = File.ReadAllLines($"""{Path.GetDirectoryName(Util.CurrentQueryPath)}\in.txt""");

void Main()
{
	int cycle = 1;
	int registerX = 1;
	int signal = 0;
	int crtPos = 0;

	foreach (var rawLine in _input)
	{
		var op = rawLine.Split(" ");
		var addxIncrement = 0;
		for (int opCycle = 0; opCycle < opCycles(op[0]); opCycle++)
		{
			if (cycle%40 == 20)
				signal+=cycle*registerX;
			
			if (crtPos%40 == 0)
				Console.WriteLine();
				
			if (crtPos == registerX-1 || crtPos == registerX || crtPos == registerX+1)
				Console.Write("#");
			else
				Console.Write(".");
			
			if (op[0] == "addx")
			{
				addxIncrement = int.Parse(op[1]);
			}
			cycle++;
			crtPos = (cycle-1)%40;
		}
		
		if (op[0] == "addx")
		{
			registerX += addxIncrement;
		}
	}
	Console.WriteLine();
	Console.WriteLine($"Part 1 = {signal}");
}


static int opCycles(string op) => op switch
{
	"addx" => 2,
	"noop" => 1,
	_ => -1
};

