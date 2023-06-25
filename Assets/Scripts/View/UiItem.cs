using System;
using UnityEngine;
using UnityEngine.UI;

public class UiItem: MonoBehaviour
{
    [SerializeField] private Button button;
    private int index;
    private Action<int> action;
    
    public void Init(int id,Action<int> onClick)
    {
        index = id;
        action += onClick;
        button.onClick.AddListener(() =>
        {
            action?.Invoke(index);
        });
    }
}
