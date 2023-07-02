using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UiItem : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private LocalizeStringEvent mText;
    [SerializeField] private Image iconImage;
    [SerializeField] private UnityEvent open;
    [SerializeField] private UnityEvent close;
    [SerializeField] private int index;

    private bool isOpen;
    private Action<int> action;

    public void Init(int id, Action<int> onClick, Sprite icon, LocalizedString text)
    {
        Init(id, onClick, icon);
        mText.StringReference = text;
    }

    public void Init(int id, Action<int> onClick, Sprite icon = null)
    {
        index = id;
        action += onClick;
        iconImage.sprite = icon;
        button.onClick.AddListener(() =>
        {
            action?.Invoke(index);
            isOpen = !isOpen;
            if (isOpen)
                open?.Invoke();
            else
                close?.Invoke();
        });
    }
}
