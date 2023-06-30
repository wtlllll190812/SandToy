using UnityEngine;
using UnityEngine.InputSystem;

public class EvoBrush : MonoEvoLayer
{
    [SerializeField] private int ppu = 100;
    [SerializeField] private InputActionAsset inputSetting;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private Camera cam;

    private Species currentSpecie;
    private InputAction paint;
    private bool pressed;
    private bool clear;
    private int kernelClear;
    private int brushSize;

    public int BrushSize
    {
        get => brushSize * mainMap.BasicTexture.width / 256;
        set => brushSize = value;
    }

    private void Awake()
    {
        LeftSidePanel.RegisterOnBrushSizeChange(size => BrushSize = size);
        LeftSidePanel.RegisterOnBrushTypeChange(species => currentSpecie = (Species) species);
    }

    public override void Init(MainMap map)
    {
        base.Init(map);
        //Action
        paint = inputSetting.FindActionMap("Player").FindAction("Paint");
        inputSetting.FindActionMap("Player").FindAction("StartPaint").performed += OnPaint;
        inputSetting.FindActionMap("Player").FindAction("Clear").performed += OnClear;
        //Shader
        kernelClear = computeShader.FindKernel("Clear");
        computeShader.SetInt("texSize", mainMap.BasicTexture.width);
        computeShader.SetTexture(kernelClear, "Result", mainMap.BasicTexture);
        computeShader.SetTexture(kernelClear, "Environment", mainMap.EnvironmentTexture);
        computeShader.Dispatch(kernelClear, mainMap.BasicTexture.width / 8, mainMap.BasicTexture.height / 8, 1);
        //Other
        // col.size = new Vector2((float) mainMap.BasicTexture.width / ppu, (float) mainMap.BasicTexture.height / ppu);
    }

    public override void Execute(int seed)
    {
        if (clear)
        {
            computeShader.Dispatch(kernelClear, mainMap.BasicTexture.width / 8, mainMap.BasicTexture.height / 8, 1);
            clear = false;
        }

        if (!pressed) return;

        var pixelId = GetPixelID(mainMap.BasicTexture.width, mainMap.BasicTexture.height);
        Debug.Log(pixelId);
        computeShader.SetInt("kind", (int) currentSpecie);
        computeShader.SetInt("size", BrushSize);
        computeShader.SetInts("pos", pixelId.x, pixelId.y);
        base.Execute(seed);
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
        var pixelId = new Vector2Int(width / 2 + (int) (hitPoint.x * ppu) * width / 256,
            height / 2 + (int) (hitPoint.y * ppu) * height / 256);
        return pixelId;
    }
}