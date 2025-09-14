using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandWarrior : PlayerController
{
    public override void NormalAtk()
    {
        target.OnDamage(this, plaStatus.Damage);
    }
}
