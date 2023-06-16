using UnityEngine;

public class EvoBasicElements: EvoluteLayer
{
    [SerializeField]private int mode = 0;

    private int Mode
    {
        set
        {
            mode = value;
            if (mode > 8)
            {   
                mode = 0;
            }
        }
        get => mode;
    }
    
    public override void Execute(RenderTexture texture, MainMap map, int seed)
    {
        Mode++;
        computeShader.SetInt("mode", Random.Range(0, 9));
        base.Execute(texture, map, seed);
    }
}
