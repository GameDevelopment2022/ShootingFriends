using System;
using System.Collections;
using UnityEngine;

public static class Utils
{
    public static IEnumerator PerformActionAfterCertainTime(float delay, Action callback)
    {
        yield return new WaitForSeconds(delay);
        callback();
    }
}