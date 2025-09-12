using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager
{
    public bool RollBackPercent(float percent)
    {
        if(percent >= 100) return true;
        if(percent <= 0) return false;

        return Random.value < (percent * 0.01f); // 50 -> 0.5
    }
}
