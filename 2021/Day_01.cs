namespace AoC2021;

public class Day01 : BaseDay
{
    private List<int> _lines;

    public Day01()
    {
        _lines = File.ReadLines(InputFilePath).Select(l => int.Parse(l)).ToList();
    }


    public override ValueTask<string> Solve_1()
    {
        var counter =0;
        for (var i = 1; i < _lines.Count; i++)
        {
            if (_lines[i] > _lines[i-1])
                counter++;
        }

        return new(counter.ToString());
    }

    
    public override ValueTask<string> Solve_2()
    {
        var counter = 0;
        for (var i = 3; i < _lines.Count; i++)
        {
            if ((_lines[i] > _lines[i - 3]))
                counter++;
        }

        return new(counter.ToString());
    }

}