namespace AoC2021;

public class Day03 : BaseDay
{
    private List<string> _lines;

    public Day03()
    {
	    _lines = File.ReadLines(InputFilePath).ToList();
    }

    public override ValueTask<string> Solve_1()
    {
	    int gamma = 0, epsilon = 0; 
		foreach (var i in Enumerable.Range(0, 12))
		{
			var ones = _lines.Count(l => l[i] == '1');
			var zeros = _lines.Count - ones;
			gamma = gamma << 1 | (ones >= zeros ? 1 : 0);
			epsilon = epsilon << 1 | (ones >= zeros ? 0 : 1);
		}

	    return new((gamma * epsilon).ToString());
    }

    
    public override ValueTask<string> Solve_2()
    {
	    var lines = _lines.ToList();
	    int index = 0;
	    do
	    {
		    var ones = lines.Count(l => l[index] == '1');
			char selector = ones >= (lines.Count - ones) ? '1' : '0';
		    lines = lines.Where(l => l[index] == selector).ToList();
		    index++;
	    } while (lines.Count > 1);
		var oxy = BinaryStrToInt(lines[0]);

		lines = _lines.ToList();
		index = 0;
		do
		{
			var ones = lines.Count(l => l[index] == '1');
			char selector = ones < (lines.Count - ones) ? '1' : '0';
			lines = lines.Where(l => l[index] == selector).ToList();
			index++; 
		} while (lines.Count > 1);
		var scrub = BinaryStrToInt(lines[0]);

		return new((oxy * scrub).ToString());
    }

    int BinaryStrToInt(string input)
    {
	    int result = 0;
	    foreach (var bit in input)
		    result = result << 1 | (bit == '1' ? 1 : 0);

	    return result;
    }
}