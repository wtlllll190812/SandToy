using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.U2D;

public class EvoBrush : EvoluteLayer
{
    [SerializeField] private int ppu = 100;
    [SerializeField] private Camera cam;
    [SerializeField] private InputActionAsset inputSetting;
    [SerializeField] private Species currentSpecie;
    [SerializeField] private Vector2 brushOffset;
    private InputAction paint;
    private InputAction startPaint;
    private Collider2D col;
    private bool pressed;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        paint = inputSetting.FindActionMap("Player").FindAction("Paint");
        startPaint = inputSetting.FindActionMap("Player").FindAction("StartPaint");
        startPaint.performed += OnPaint;
    }

    public void OnPaint(InputAction.CallbackContext context)
    {
        pressed = !pressed;
    }

    public override void Execute(RenderTexture texture, MainMap map, int seed)
    {
        if (!pressed) return;
        var pixelId = GetPixelID(texture.width, texture.height);
        computeShader.SetInt("kind", (int) currentSpecie);
        computeShader.SetInts("pos", pixelId.x, pixelId.y);
        base.Execute(texture, map, seed);
    }

    /// <summary>
    /// 获取点击点
    /// </summary>
    private bool GetHitPoint(out Vector2 hitPoint)
    {
        Vector3 pos = paint.ReadValue<Vector2>();
        pos.z = 0;
        var hit = Physics2D.Raycast(cam.ScreenToWorldPoint(pos), Vector2.zero);
        hitPoint = hit.collider == col ? hit.point : Vector2.zero;
        return hit.collider == col;
    }

    /// <summary>
    /// 求像素坐标
    /// </summary>
    private Vector2Int GetPixelID(int width, int height)
    {
        if (!GetHitPoint(out var hitPoint)) return new Vector2Int(-100, -100);
        var pixelId = new Vector2Int(width / 2 + (int) (hitPoint.x * ppu),
            height / 2 + (int) (hitPoint.y * ppu));
        return pixelId;

    }
}