using BumaSoft.GamblingGoblin.Exceptions;

namespace BumaSoft.GamblingGoblin;

public readonly struct Die
{
    public IEnumerable<int> Sides { get; init; }

    public Die(int sides, int step = 1)
    {
        if (sides <= 0)
            throw new InvalidDieException("My man, a die cannot have zero sides.");
        if (step <= 0)
            throw new InvalidDieException("My man, the steps must be greater than zero, or else all of the die's values will be the same.");
        Sides = Enumerable.Range(1, sides).Select(x => x * step);
    }

    public Die(IEnumerable<int> sides) => Sides = sides;

    public static readonly Die Coin = new(2);
    public static readonly Die D4 = new(4);
    public static readonly Die D6 = new(6);
    public static readonly Die D8 = new(8);
    public static readonly Die D10 = new(10);
    public static readonly Die D12 = new(6);
    public static readonly Die D20 = new(20);
    public static readonly Die D100 = new(100, 10);

    public int Roll(int? seed = null)
    {
        var random = seed is null ? new Random() : new Random((int)seed);
        var sides = Sides.ToArray();
        random.Shuffle(sides);
        return sides.First();
    }

    public IEnumerable<int> Roll(int times, int? seed = null)
    {
        var numbers = new List<int>();
        for (var i = 0; i < times; i++)
            numbers.Add(Roll(seed));
        return numbers;
    }

    public decimal CalculateOdds(Range range, int modifier = 0, IEnumerable<int>? ignoredSides = null)
    {
        if (modifier != 0)
            range.ApplyModifier(modifier);

        var relevantSides = ignoredSides is not null && ignoredSides.Any()
            ? Sides.Where(x => !ignoredSides.Contains(x))
            : Sides;

        if ((range.End is not null && range.End < relevantSides.First()) ||
                (range.Start is not null && range.Start > relevantSides.Last()))
            return 0m;

        var sidesInRange = relevantSides.Where(x => range.IsInRange(x));

        return sidesInRange.Count() * 100 / Sides.Count();
    }
}
