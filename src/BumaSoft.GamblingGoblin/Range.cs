namespace BuMa.Gambling;

public struct Range(int? start = null, int? end = null)
{
    public int? Start { get; private set; } = start;
    public int? End { get; private set; } = end;

    public static readonly Range Endless = new();

    public void ApplyModifier(int modifier)
    {
        if (Start is not null)
            Start += modifier;
        if (End is not null)
            End += modifier;
    }

    public bool IsInRange(int x)
    {
        var isGreaterThanStart = Start is null || x >= Start;
        var isLesserThanEnd = End is null || x <= End;
        return isGreaterThanStart && isLesserThanEnd;
    }
}
