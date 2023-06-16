using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class EvoBrush : EvoluteLayer
{
    [SerializeField] private int ppu = 100;
    [SerializeField] private Camera cam;
    [SerializeField] private InputActionAsset inputSetting;
    [SerializeField] private Vector2 brushOffset;
    [SerializeField] private int maxBrushSize;
    
    private int brushSize;
    private Species currentSpecie;
    private InputAction paint;
    private Collider2D col;
    private bool pressed;
    private bool clear;
    private int kernelClear;

    public override void Init(RenderTexture texture)
    {
        col = GetComponent<Collider2D>();
        paint = inputSetting.FindActionMap("Player").FindAction("Paint");
        inputSetting.FindActionMap("Player").FindAction("StartPaint").performed += OnPaint;
        inputSetting.FindActionMap("Player").FindAction("Clear").performed += OnClear;
        kernelClear = computeShader.FindKernel("Clear");
        computeShader.SetTexture(kernelClear, "Result", texture);
        
        BrushBinder.RegisterOnBrushSizeChange(size => brushSize = (int) (size * maxBrushSize));
        BrushBinder.RegisterOnBrushTypeChange(species => currentSpecie = (Species) species);
        base.Init(texture);
    }

    public override void Execute(RenderTexture texture, MainMap map, int seed)
    {
        if (clear)
        {
            computeShader.Dispatch(kernelClear, texture.width / 8, texture.height / 8, 1);
            clear = false;
        }
        if (!pressed) return;

        var pixelId = GetPixelID(texture.width, texture.height);
        computeShader.SetInt("kind", (int) currentSpecie);
        computeShader.SetInt("size", brushSize);
        computeShader.SetInts("pos", pixelId.x, pixelId.y);
        base.Execute(texture, map, seed);
    }

    /// <summary>
    /// 清屏
    /// </summary>
    private void OnClear(InputAction.CallbackContext context)
    {
        clear = true;
    }
    
    /// <summary>
    /// 开始绘画
    /// </summary>
    public void OnPaint(InputAction.CallbackContext context)
    {
        pressed = !pressed;
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