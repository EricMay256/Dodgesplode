using System;
using System.ComponentModel;
using UnityEngine;

// public class IntRange : Range<int>
// {
//     public IntRange(int min, int max) : base(min, max) { }
//     public override int GetRandomValue()
//     {
//         return UnityEngine.Random.Range(Min, Max);
//     }
// }
// public class FloatRange : Range<float>
// {
//     public FloatRange(float min, float max) : base(min, max) { }
//     public override float GetRandomValue()
//     {
//         return UnityEngine.Random.Range(Min, Max);
//     }
// }
[Serializable]
public class Range<T> where T : IComparable<T>
{
    [field: SerializeField]
    public T Min { get; private set; }
    [field: SerializeField]
    public T Max { get; private set; }

    public Range(T min, T max)
    {
        if (min.CompareTo(max) > 0)
        {
            throw new ArgumentException("Min cannot be greater than Max.");
        }
        Min = min;
        Max = max;
    }

    public virtual T Clamp(T value)
    {
        if (value.CompareTo(Min) < 0) return Min;
        if (value.CompareTo(Max) > 0) return Max;
        return value;
    }

    public virtual T GetRandomValue()
    {
        
        try
        {
            if (typeof(T) == typeof(int))
            {
                return (T)(object)UnityEngine.Random.Range((int)(object)Min, (int)(object)Max);
            }
            else if (typeof(T) == typeof(float))
            {
                return (T)(object)UnityEngine.Random.Range((float)(object)Min, (float)(object)Max);
            }
            throw new NotSupportedException("GetRandomValue is not implemented for this type. Either override this function or the UnityEngine.Random function to process this type.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in GetRandomValue: {e.Message}");
            throw e;
        }

    }

    public bool Contains(T value)
    {
        return value.CompareTo(Min) >= 0 && value.CompareTo(Max) <= 0;
    }

    public override string ToString()
    {
        return $"Range({Min}, {Max})";
    }
}
