using Unity.VisualScripting;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    private static Debugger Instance;
    public static bool IsDebug;
    public bool isDebug;

    private void Awake()
    {
#if UNITY_EDITOR
        IsDebug = isDebug;
#else
        IsDebug = false;
#endif

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}