using System.Collections;
using System;
using UnityEngine;


public static class MonoBehaviourExtensions
{
    public static void Invoke(this MonoBehaviour behaviour, Action function, float delay)
    {
        behaviour.StartCoroutine(ExecuteAfterTime(function, delay));
    }

    public static void InvokeRepeating(this MonoBehaviour behaviour, Action function, float delay, float repeatDelay)
    {
        behaviour.StartCoroutine(ExecuteAfterTimeRepeating(behaviour, function, delay, repeatDelay));
    }

    private static IEnumerator ExecuteAfterTime(Action function, float delay)
    {
        yield return new WaitForSeconds(delay);
        function();
    }

    private static IEnumerator ExecuteAfterTimeRepeating(MonoBehaviour behaviour, Action function, float delay, float repeatDelay)
    {
        yield return new WaitForSeconds(delay);

        function();

        yield return new WaitForSeconds(repeatDelay);

        behaviour.StartCoroutine(ExecuteAfterTimeRepeating(behaviour, function, delay, repeatDelay));
    }
}
