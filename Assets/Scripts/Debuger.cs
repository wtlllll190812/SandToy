using UnityEngine;

public class Debugger: MonoBehaviour
{
    private static Debugger Instance;
    public static bool IsDebug;

    private void Awake()
    {
        #if UNITY_EDITOR
            IsDebug = false;
        #else
            IsDebug = false;
        #endif
        
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
