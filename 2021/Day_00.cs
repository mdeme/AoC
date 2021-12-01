namespace AoC2021;

public class Day00 : BaseDay
{
    private List<int> _lines;

    public Day00()
    {
        _lines = File.ReadLines(InputFilePath).Select(l => int.Parse(l)).ToList();
    }


    public override ValueTask<string> Solve_1()
    {
        var counter =0;

        return new(counter.ToString());
    }

    
    public override ValueTask<string> Solve_2()
    {
        var counter = 0;

        return new(counter.ToString());
    }

}