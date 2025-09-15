using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Stage_2 : Stage_Base
{
    public GameObject door;
    protected override void Complete()
    {
        Destroy(door);
    }
}
