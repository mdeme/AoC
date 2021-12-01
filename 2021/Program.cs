global using AoCHelper;


if (args.Length == 0)
{
    ConsoleKeyInfo key;
    do
    {
        Solver.SolveLast();
        key = Console.ReadKey();
    }
    while ((key.Modifiers & ConsoleModifiers.Control) == 0 || key.KeyChar != 'c');

}
else if (args.Length == 1 && args[0].Contains("all", System.StringComparison.CurrentCultureIgnoreCase))
{
    Solver.SolveAll();
}
else
{
    var indexes = args.Select(arg => uint.TryParse(arg, out var index) ? index : uint.MaxValue);

    Solver.Solve(indexes.Where(i => i < uint.MaxValue));
}
