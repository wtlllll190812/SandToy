using System;
using UnityEngine;

public class Debugger: MonoBehaviour
{
    private static Debugger Instance;
    public static bool IsDebug;

    private void Awake()
    {
        #if UNITY_EDITOR
            IsDebug = true;
        #endif
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
