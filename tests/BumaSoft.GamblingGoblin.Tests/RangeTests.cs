namespace BumaSoft.GamblingGoblin.Tests;

public class RangeTests
{
    [Test]
    public void CreateNormalRange()
    {
        var range = new Range(1, 10);
        Assert.Pass();
    }

    [Test]
    public void CreateEndlessRange()
    {
        var range = Range.Endless;
        Assert.Pass();
    }

    [Test]
    public void ModifyRange()
    {
        var range = new Range(1, 10);
        range.ApplyModifier(10);
        Assert.That(range.Start == 11);
        Assert.That(range.End == 20);
    }

    [Test]
    public void ModifyEndlessRange()
    {
        var range = Range.Endless;
        range.ApplyModifier(10);
        Assert.Pass();
    }

    [Test]
    public void AssertNumberIsInRange()
    {
        var range = new Range(1, 10);
        for (var i = 1; i <= 10; i++)
            Assert.That(range.IsInRange(i));
    }

    [Test]
    public void AssertAnyNumberIsInEndlessRange()
    {
        var range = Range.Endless;
        Assert.Multiple(() =>
        {
            Assert.That(range.IsInRange(-100));
            Assert.That(range.IsInRange(0));
            Assert.That(range.IsInRange(100));
            Assert.That(range.IsInRange(int.MinValue));
            Assert.That(range.IsInRange(int.MaxValue));
        });
    }

    [Test]
    public void AssertNumberIsNotInRange()
    {
        var range = new Range(1, 10);
        Assert.Multiple(() =>
        {
            Assert.That(range.IsInRange(-10), Is.False);
            Assert.That(range.IsInRange(20), Is.False);
        });
    }
}
