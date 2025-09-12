using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalArcher : PlayerController
{
    public override void NormalAtk()
    {
        if(target == null)
            return;

        Vector2 dir = (target.transform.position - transform.position).normalized;
        GameObject arrow = Manager.Resources.Instantiate("Projectile/Arrow", transform.position, Quaternion.identity);
        ProjectileController projectile = arrow.AddComponent<ProjectileController>();
        projectile.SetInfo(this, dir, 12, plaStatus.Damage);
    }
}
