namespace AoC2021;

public class Day02 : BaseDay
{

    record struct Navigation(string Operation, int Value);

    private List<Navigation> _lines;

    public Day02()
    {
        _lines = File.ReadLines(InputFilePath)
            .Select(l => l.Split(" "))
            .Select(a => new Navigation(a[0], int.Parse(a[1])))
            .ToList();
    }


    public override ValueTask<string> Solve_1()
    {
        int x = 0; int y = 0;
        int aim = 0;
        foreach (var op in _lines)
        {
            switch (op.Operation)
            {
                case "down":
                    y += op.Value;
                    break;
                case "up":
                    y -= op.Value;
                    break;
                case "forward":
                    x += op.Value;
                    break;
            }
        }

        return new((x*y).ToString());
    }

    
    public override ValueTask<string> Solve_2()
    {
        int x = 0; int y = 0;
        int aim = 0;
        foreach (var op in _lines)
        {
            switch (op.Operation)
            {
                case "down":
                    aim += op.Value;
                    break;
                case "up":
                    aim -= op.Value;
                    break;
                case "forward":
                    x += op.Value;
                    y += op.Value * aim;
                    break;
            }
        }

        return new((x * y).ToString());
    }

}