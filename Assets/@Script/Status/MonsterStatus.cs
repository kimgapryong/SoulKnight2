using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : Status
{
    public float Amount { get; private set; }
    public override void SetData(CreatureData data)
    {
        base.SetData(data);
        Amount = data.Amount;
    }
}
