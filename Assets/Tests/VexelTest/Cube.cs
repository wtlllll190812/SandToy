using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class Cube : MonoBehaviour
{
    public int size=16;
    public GameObject cubePref;

    [Button("GenCube")]
    public void GenCube()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                Vector3 pos = new Vector3(x, 0, y);
                var gobj = Instantiate(cubePref);
                gobj.transform.position = pos;
                gobj.transform.parent = transform;
            }
        }
    }

    public bool IsBound(Vector3 pos)
    {
        bool res = false;
        res |= pos.x==0;
        res |= pos.x==size-1;
        res |= pos.y==0;
        res |= pos.y==size-1;
        res |= pos.z==0;
        res |= pos.z==size-1;
        return res;
    }
}
