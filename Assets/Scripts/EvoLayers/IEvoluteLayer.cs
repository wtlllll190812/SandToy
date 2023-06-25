using UnityEngine;

public interface IEvoluteLayer
{
    public bool IsReady();

    public void Init(MainMap map);

    public void Execute(int seed);
}