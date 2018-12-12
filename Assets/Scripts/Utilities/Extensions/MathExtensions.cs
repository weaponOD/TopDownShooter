using System;
using System.Threading;
using UnityEngine;

public static class ThreadSafeRandom
{
    [ThreadStatic] private static System.Random Local;

    public static System.Random ThisThreadsRandom
    {
        get { return Local ?? (Local = new System.Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
    }
}

public static class FloatExtensions
{
    public static float SqrDistanceTo(this Vector3 source, Vector3 destination)
    {
        return source.DirectionTo(destination).sqrMagnitude;
    }

    public static float Remap(this float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }
}

public static class Vector3Extensions
{
    public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
    }

    public static Vector3 Flat(this Vector3 original)
    {
        return new Vector3(original.x, 0, original.y);
    }

    public static Vector3 DirectionTo(this Vector3 source, Vector3 destination, bool normalize = false)
    {
        return normalize ? Vector3.Normalize(destination - source) : destination - source;
    }

    public static Vector3 DirectionTo(this Transform source, Transform destination, bool normalize = false)
    {
        return source.position.DirectionTo(destination.position, normalize);
    }
}