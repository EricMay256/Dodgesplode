using UnityEngine;
using UBear;
using NUnit.Framework;

namespace UBear.Tests
{
public class TestRange
{
    private const int RANDOM_TESTS_ITERATIONS = 1000;
    [Test]
    public void TestIntRange()
    {
        var intRange = new Range<int>(1, 3);
        Assert.AreEqual(1, intRange.Min);
        Assert.AreEqual(3, intRange.Max);
        Assert.AreEqual(1, intRange.Clamp(0));
        Assert.AreEqual(3, intRange.Clamp(4));
        Assert.AreEqual(2, intRange.Clamp(2));
        int randomValue;
        for(int i = 0; i < RANDOM_TESTS_ITERATIONS; i++)
        {
            randomValue = intRange.GetRandomValue();
            Assert.IsTrue(randomValue >= intRange.Min && randomValue < intRange.Max);
        }
    }
    [Test]
    public void TestFloatRange()
    {
        var floatRange = new Range<float>(1.5f, 3.5f);
        Assert.AreEqual(1.5f, floatRange.Min);
        Assert.AreEqual(3.5f, floatRange.Max);
        Assert.AreEqual(1.5f, floatRange.Clamp(0f));
        Assert.AreEqual(3.5f, floatRange.Clamp(4f));
        Assert.AreEqual(2.5f, floatRange.Clamp(2.5f));
        float randomValue;
        for(int i = 0; i < RANDOM_TESTS_ITERATIONS; i++)
        {
            randomValue = floatRange.GetRandomValue();
            Assert.IsTrue(randomValue >= floatRange.Min && randomValue < floatRange.Max);
        }
    }
}
}