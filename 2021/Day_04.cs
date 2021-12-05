using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace AoC2021;

public class Day04 : BaseDay
{
	private List<int> _numbers;
    private IList<Board> _boards;

    public Day04()
    {
	    List<IList<string>> parts = SplitInput(File.ReadLines(InputFilePath)).ToList();

	    _numbers = parts[0][0].Split(',').Select(s => int.Parse(s)).ToList();
	    _boards = parts.Skip(1).Select(l => new Board(l)).ToList();
    }

    class Board
    {
	    private int Dimension => 5;
		record struct Field(int Value, bool Selected);
		private Field[][] _fields;
		private IEnumerable<Field> AllFields => _fields.SelectMany(f => f);

		IEnumerable<Field> Row(int index)
		{
			for (int i = 0; i < Dimension; i++)
				yield return _fields[index][i];
		}

		IEnumerable<Field> Col(int index)
		{
			for (int i = 0; i < Dimension; i++)
				yield return _fields[i][index];
		}

		public Board(IList<string> input)
	    {
		   _fields = input.Select(l => l.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
			   .Select(s => new Field(int.Parse(s), false)).ToArray())
			   .ToArray();
	    }

	    public void DrawNumber(int value)
	    {
			for(int i = 0; i < Dimension; i++)
			for (int j = 0; j < Dimension; j++)
			{
				if (_fields[i][j].Value == value)
					_fields[i][j].Selected = true;
			}
	    }

	    public bool HasWon()
	    {
			for (int i = 0; i < Dimension; i++)
			    if (Row(i).All(f => f.Selected))
				    return true;

			for (int i = 0; i < Dimension; i++)
				if (Col(i).All(f => f.Selected))
					return true;

			return false;
	    }

	    public int Sum()
	    {
		    return _fields.SelectMany(f => f).Where(f => !f.Selected).Sum(f => f.Value);
	    }
    }

    IEnumerable<IList<string>> SplitInput(IEnumerable<string> input)
    {
	    List<string> temp = new List<string>();
	    foreach (var line in input)
	    {
		    if (!String.IsNullOrEmpty(line))
		    {
			    temp.Add(line);
		    }
		    else
		    {
				yield return temp;
				temp = new List<string>();
			}
	    }

	    if (temp.Count > 0)
		    yield return temp;
    }

    public override ValueTask<string> Solve_1()
    {
	    foreach (int number in _numbers)
	    {
		    foreach (var board in _boards)
		    {
				board.DrawNumber(number);
				if(board.HasWon())
					return new((board.Sum() * number).ToString());
			}
	    }

	    return new((-1).ToString());
	}

    
    public override ValueTask<string> Solve_2()
    {
		List<Board> boards = _boards.Where(b => !b.HasWon()).ToList();
		foreach (int number in _numbers)
	    {
		    boards = _boards.Where(b => !b.HasWon()).ToList();

		    foreach (var board in boards)
		    {
			    board.DrawNumber(number);
				if (boards.Count == 1 && board.HasWon())
			    {
				    return new((board.Sum() * number).ToString());
			    }
			}
	    }

	    return new((-1).ToString());
	}

}