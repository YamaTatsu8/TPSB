using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public static class MonoBehaviorExtentsion
{
    private static bool _isRunning = false;

    public static IEnumerator DelayMethod(this MonoBehaviour mono, float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    public static Coroutine Delay(this MonoBehaviour mono, float waitTime, Action action)
    {
        return mono.StartCoroutine(DelayMethod(mono, waitTime, action));
    }

    public static IEnumerator DelayMethodOnce(this MonoBehaviour mono, float waitTime, Action action)
    {
        if (_isRunning) yield break;

        _isRunning = true;
        yield return new WaitForSeconds(waitTime);
        action();

        _isRunning = false;
    }

    public static Coroutine DelayOnce(this MonoBehaviour mono, float waitTime, Action action)
    {
        return mono.StartCoroutine(DelayMethodOnce(mono, waitTime, action));
    }

    public static IEnumerator DelayMethodForSpecifiedTime(this MonoBehaviour mono, float waitTime, Action action)
    {
        float time = 0;

        // コルーチン進行中は数値を増加させる
        while (time < waitTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        action();        
    }

    public static Coroutine DelayForSpecifiedTime(this MonoBehaviour mono, float waitTime, Action action)
    {
        return mono.StartCoroutine(DelayMethodOnce(mono, waitTime, action));
    }
}

/*
    public static IEnumerator DelayMethod<T>(this MonoBehaviour mono, float waitTime, Action<T> action, T t)
    {
        yield return new WaitForSeconds(waitTime);
        action(t);
    }

    public static Coroutine Delay<T>(this MonoBehaviour mono, float waitTime, Action<T> action, T t)
    {
        return mono.StartCoroutine(DelayMethod(mono, waitTime, action, t));
    }
 */
