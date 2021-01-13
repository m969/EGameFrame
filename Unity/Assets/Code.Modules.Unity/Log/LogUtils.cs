using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogUtils
{
    public static void Debug(string log)
    {
        UnityEngine.Debug.Log(log);
    }

    public static void Error(string log)
    {
        UnityEngine.Debug.LogError(log);
    }

    public static void LogException(System.Exception e)
    {
        UnityEngine.Debug.LogException(e);
    }
}
