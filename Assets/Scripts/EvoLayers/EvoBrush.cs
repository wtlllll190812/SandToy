using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EvoBrush : EvoluteLayer
{
    [SerializeField] private Camera cam;
    [SerializeField] private InputActionAsset input;
    private InputAction paint;
    private Collider2D col;
    private Species specie = Species.Sand;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        paint = input.FindAction("Paint");
    }

    public void OnPaint(InputAction.CallbackContext context)
    {
        var pos = paint.ReadValue<Vector2>();
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(pos), Vector2.zero);
        if (hit.collider == col)
        {
            Debug.Log("Target" + hit.point);
        }
    }
}