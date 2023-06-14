using UnityEngine;
using UnityEngine.InputSystem;

public class EvoBrush : EvoluteLayer
{
    [SerializeField] private Camera cam;
    [SerializeField] private InputActionAsset input;
    [SerializeField] private Vector2 brushOffset;
    [SerializeField] private Species specie = Species.Sand;
    private InputAction paint;
    private InputAction startPaint;
    private Collider2D col;
    [SerializeField]private bool pressed;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        paint = input.FindActionMap("Player").FindAction("Paint");
        startPaint = input.FindActionMap("Player").FindAction("StartPaint");
        startPaint.performed += OnPaint;
    }

    public void OnPaint(InputAction.CallbackContext context)
    {
        pressed = !pressed;
    }

    public override void Execute(RenderTexture texture, MainMap map, int seed)
    {
        if (!pressed) return;

        var pixelId = GetPixelID(texture.texelSize, texture.width, texture.height);
        Debug.Log(pixelId + "pixelId" + specie);
        computeShader.SetInt("kind", (int) specie);
        computeShader.SetInts("pos", pixelId.x, pixelId.y);
        base.Execute(texture, map, seed);
    }

    /// <summary>
    /// 获取点击点
    /// </summary>
    private Vector2 GetHitPoint()
    {
        Vector3 pos = paint.ReadValue<Vector2>();
        pos.z = 0;
        var hit = Physics2D.Raycast(cam.ScreenToWorldPoint(pos), Vector2.zero);
        return hit.collider == col ? hit.point : Vector2.zero;
    }

    /// <summary>
    /// 求像素坐标
    /// </summary>
    private Vector2Int GetPixelID(Vector2 texelSize, int width, int height)
    {
        var point = (brushOffset + GetHitPoint()) / 2;
        var pixelId = new Vector2Int(width / 2 + (int) (point.x / texelSize.x),
            height / 2 + (int) (point.y / texelSize.y));
        return pixelId;
    }
}