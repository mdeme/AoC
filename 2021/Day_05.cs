using System.Drawing;
namespace AoC2021;

public class Day05 : BaseDay
{
	record struct VentLine(Point Start, Point End);

    private readonly List<VentLine> _vents = new();
    private IEnumerable<Point> AllPoints => _vents.Select(v => v.Start)
										    .Concat(_vents.Select(v => v.End))
										    .ToList();

    public Day05()
    {
        var lines = File.ReadLines(InputFilePath).ToList();

        foreach (var line in lines)
        {
	        var parts = line.Split(" -> ");
	        var p1 = parts[0].Split(",");
            var v1 = new Point(int.Parse(p1[0]), int.Parse(p1[1]));
            var p2 = parts[1].Split(",");
            var v2 = new Point(int.Parse(p2[0]), int.Parse(p2[1]));
            _vents.Add(new VentLine(v1, v2));
        }
    }

    public override ValueTask<string> Solve_1()
    {
	    var field = new int[AllPoints.Max(p => p.X)+1, AllPoints.Max(v => v.Y)+1];

        foreach (var vent in _vents)
	        DrawLineSimple(field, vent.Start, vent.End);

        var counter = field.Cast<int>().Count(i => i > 1);
        return new(counter.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
	    var field = new int[AllPoints.Max(p => p.X) + 1, AllPoints.Max(v => v.Y) + 1];

	    foreach (var vent in _vents)
		    DrawLineExtended(field, vent.Start.X, vent.Start.Y, vent.End.X, vent.End.Y);

	    var counter = field.Cast<int>().Count(i => i > 1);
	    return new(counter.ToString());
	}

    private void DrawLineSimple(int[,] field, Point start, Point end)
    {
	    if (start.X == end.X)
	    {
		    for (int i = Math.Min(start.Y, end.Y); i <= Math.Max(start.Y, end.Y); i++)
			    field[start.X, i] += 1;
	    }
	    else if (start.Y == end.Y)

	    {
		    for (int i = Math.Min(start.X, end.X); i <= Math.Max(start.X, end.X); i++)
			    field[i, start.Y] += 1;
	    }
    }

    public void DrawLineExtended(int[,] field, int x, int y, int x2, int y2)
    {
	    int w = x2 - x;
	    int h = y2 - y;
	    int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
	    if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
	    if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
	    if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
	    int longest = Math.Abs(w);
	    int shortest = Math.Abs(h);
	    if (!(longest > shortest))
	    {
		    longest = Math.Abs(h);
		    shortest = Math.Abs(w);
		    if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
		    dx2 = 0;
	    }
	    int numerator = longest >> 1;
	    for (int i = 0; i <= longest; i++)
	    {
		    field[x,y]+=1;
		    numerator += shortest;
		    if (!(numerator < longest))
		    {
			    numerator -= longest;
			    x += dx1;
			    y += dy1;
		    }
		    else
		    {
			    x += dx2;
			    y += dy2;
		    }
	    }
    }
}